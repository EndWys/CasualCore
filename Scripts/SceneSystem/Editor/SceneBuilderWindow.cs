using UnityEditor;
using UnityEngine;

namespace Assets.CasualCore.Scripts.SceneSystem.Editor
{
    public class SceneBuilderWindow : EditorWindow
    {
        private DefaultSceneBuilder _sceneBuilder = new DefaultSceneBuilder();

        private string _sceneName = "NewGameScene";

        [MenuItem("HyperCasualCore/Scene Builder")]
        public static void ShowWindow()
        {
            GetWindow<SceneBuilderWindow>("Scene Builder");
        }

        private void OnGUI()
        {
            GUILayout.Label("Scene Builder", EditorStyles.boldLabel);

            GUILayout.Space(10);

            _sceneName = EditorGUILayout.TextField("Scene Name", _sceneName);

            GUILayout.Space(10);

            if (GUILayout.Button("Create New Game Scene"))
            {
                _sceneBuilder.BuildGameLevelScene(_sceneName);
            }
        }
    }
}