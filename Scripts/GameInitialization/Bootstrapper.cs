using Assets.CasualCore.Scripts.Runtime.SceneSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper : MonoBehaviour
{
    private const string BOOTSTRAPPER_SCENE_NAME = "Bootstrapper";

    [SerializeField] SceneData _firstScene;

    private void Start()
    {
        Application.runInBackground = true;
        //await UnityServices.InitializeAsync();

        if (SceneManager.loadedSceneCount == 1)
            _firstScene.LoadAsync(new LoadSceneParameters(LoadSceneMode.Additive));
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        if (SceneManager.GetSceneByName(BOOTSTRAPPER_SCENE_NAME).isLoaded != true)
            SceneManager.LoadScene(BOOTSTRAPPER_SCENE_NAME);
    }
}
