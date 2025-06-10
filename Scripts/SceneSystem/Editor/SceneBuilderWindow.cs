using Assets.CasualCore.Scripts.Editor;
using UnityEditor;
using UnityEngine;

namespace Assets.CasualCore.Scripts.SceneSystem.Editor
{
    public class SceneBuilderWindow : EditorWindow
    {
        [MenuItem("HyperCasualCore/Scene Builder")]
        public static void ShowWindow()
        {
            GetWindow<SceneBuilderWindow>("Scene Builder");
        }

        private void OnGUI()
        {
            GUILayout.Label("Scene Builder", EditorStyles.boldLabel);

            GUILayout.Space(10);

            if (GUILayout.Button("Create New Game Scene"))
            {
                Debug.Log("Button clicked!");
            }
        }
    }
}