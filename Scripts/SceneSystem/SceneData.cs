using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.CasualCore.Scripts.Runtime.SceneSystem
{
    [CreateAssetMenu(fileName = "New Scene Data", menuName = "CasualCore/Data Containers/Scene Data")]
    public class SceneData : ScriptableObject, ISerializationCallbackReceiver
    {
#if UNITY_EDITOR
#pragma warning disable 0649
        [SerializeField] private UnityEditor.SceneAsset scene;
#pragma warning restore 0649
#endif

        [SerializeField] private string sceneName = null;

        public string SceneName => sceneName;

        public void OnAfterDeserialize() { }

        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            string nameValue = scene != null ? scene.name : null;
            sceneName = nameValue;
#endif
        }

        public AsyncOperation LoadAsync(LoadSceneParameters parameters)
        {
            return SceneManager.LoadSceneAsync(sceneName, parameters);
        }
    }
}