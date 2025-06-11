using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.CasualCore.Scripts.Runtime.SceneSystem
{
    [CreateAssetMenu(fileName = "New Scene Data", menuName = "CasualCore/Data Containers/Scene Data")]
    public class SceneData : ScriptableObject, ISerializationCallbackReceiver
    {
#if UNITY_EDITOR
#pragma warning disable 0649
        [SerializeField] private UnityEditor.SceneAsset _scene;
#pragma warning restore 0649
#endif

        [SerializeField] private string _sceneName = null;

        public string SceneName => _sceneName;

        public void OnAfterDeserialize() { }

        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            string nameValue = _scene != null ? _scene.name : null;
            _sceneName = nameValue;
#endif
        }

        public void SetScene(UnityEditor.SceneAsset scene)
        {
            _scene = scene;
        }

        public AsyncOperation LoadAsync(LoadSceneParameters parameters)
        {
            return SceneManager.LoadSceneAsync(_sceneName, parameters);
        }
    }
}