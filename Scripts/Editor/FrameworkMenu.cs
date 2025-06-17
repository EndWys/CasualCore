using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.CasualCore.Scripts.Editor
{
    public class FrameworkMenu : EditorWindow
    {
        [MenuItem("HyperCasualCore/Show Framework Window")]
        public static void ShowWindow()
        {
            GetWindow<FrameworkMenu>("HCC Framework");
        }

        private void OnGUI()
        {
            GUILayout.Label("Hyper Casual Core", EditorStyles.boldLabel);

            if (GUILayout.Button("Do Something"))
            {
                Debug.Log("Button clicked!");
            }
        }
    }
}