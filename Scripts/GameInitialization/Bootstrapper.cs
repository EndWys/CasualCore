using Assets.CasualCore.Scripts.Runtime.SceneSystem;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper : MonoBehaviour
{
    private const string BOOTSTRAPPER_SCENE_NAME = "Bootstrapper";

    public static bool IsBootstrapperLoaded = false;

    [SerializeField] SceneData _firstScene;

    private void Start()
    {
        Application.runInBackground = true;
        //await UnityServices.InitializeAsync();

        IsBootstrapperLoaded = true;

        if (SceneManager.loadedSceneCount == 1)
            _firstScene.LoadAsync(new LoadSceneParameters(LoadSceneMode.Additive));
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        IsBootstrapperLoaded = false;

        var firstSceneInBuild = EditorBuildSettings.scenes.FirstOrDefault();

        if (firstSceneInBuild == default || !firstSceneInBuild.path.Contains(BOOTSTRAPPER_SCENE_NAME))
        {
            IsBootstrapperLoaded = true;
            return;
        }

        if (SceneManager.GetSceneByName(BOOTSTRAPPER_SCENE_NAME).isLoaded != true)
            SceneManager.LoadScene(BOOTSTRAPPER_SCENE_NAME);
    }
}
