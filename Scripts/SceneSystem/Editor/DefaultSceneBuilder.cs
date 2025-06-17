using Assets.CasualCore.Scripts.GameInitialization;
using Assets.CasualCore.Scripts.Runtime.SceneSystem;
using Assets.CasualCore.Scripts.ServiceLocatorSystem;
using System;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.CasualCore.Scripts.SceneSystem.Editor
{
    public class DefaultSceneBuilder : IGameLevelSceneBuilder, IBootstrapSceneBuilder
    {
        #region BootstrapScene

        public void BuildBootstrapScene()
        {
            string sceneName = "Bootstrapper";
            string sceneFolderPath = $"Assets/Scenes/{sceneName}";
            string sceneAssetPath = $"{sceneFolderPath}/{sceneName}.unity";

            if (File.Exists(sceneAssetPath))
            {
                Debug.LogWarning($"Scene '{sceneName}' already exists at {sceneAssetPath}. Aborting.");
                return;
            }

            if (!AssetDatabase.IsValidFolder(sceneFolderPath))
                AssetDatabase.CreateFolder("Assets/Scenes", sceneName);

            Action AfterSceneOpened = () =>
            {
                GameObject bootstrapperGO = new GameObject("Bootstrapper");
                bootstrapperGO.AddComponent<Bootstrapper>();
            };

            CreateScene(sceneAssetPath, AfterSceneOpened);

            Debug.Log($"Bootstrap scene created at {sceneAssetPath}");
        }

        #endregion

        #region GameLevelScene

        public void BuildGameLevelScene(string sceneName)
        {
            if (string.IsNullOrWhiteSpace(sceneName))
            {
                Debug.LogError("Scene name is empty.");
                return;
            }

            string sceneFolderPath = GetSceneFolderPath(sceneName);
            string sceneAssetPath = GetSceneAssetPath(sceneFolderPath, sceneName);

            if (File.Exists(sceneAssetPath))
            {
                Debug.LogWarning($"Scene '{sceneName}' already exists at {sceneAssetPath}. Aborting.");
                return;
            }

            if (!AssetDatabase.IsValidFolder(sceneFolderPath))
                AssetDatabase.CreateFolder("Assets/Scenes", sceneName);

            CreateScene(sceneAssetPath, CreateHierarchy);

            SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(sceneAssetPath);
            CreateSceneDataAsset(sceneAsset, sceneFolderPath);

        }

        private void CreateHierarchy()
        {
            // ===== BOOTSTRAPPING =====
            var bootstrapping = CreateEmptyGroup("===== BOOTSTRAPPING =====");
            GameObject entryPoint = ObjectFactory.CreateGameObject("GameLevel_EntryPoint", typeof(GameLevel_EntryPoint));
            entryPoint.transform.SetParent(bootstrapping.transform);

            // ===== MANAGERS =====
            var managers = CreateEmptyGroup("===== MANAGERS =====");
            CreateChild("GameManager", managers.transform);

            // ===== VISION =====
            var vision = CreateEmptyGroup("===== VISION =====");
            GameObject camera = ObjectFactory.CreateGameObject("Camera", typeof(Camera));
            camera.transform.SetParent(vision.transform);

            GameObject light = new GameObject("Directional Light");
            light.transform.rotation = Quaternion.Euler(50, -30, 0);
            Light lightComp = light.AddComponent<Light>();
            lightComp.type = LightType.Directional;
            light.transform.SetParent(vision.transform);

            // ===== ENVIRONMENT =====
            CreateEmptyGroup("===== ENVIRONMENT =====");
        }

        private GameObject CreateEmptyGroup(string name)
        {
            GameObject go = new GameObject(name);
            go.tag = "EditorOnly";
            return go;
        }

        private void CreateChild(string name, Transform parent)
        {
            GameObject child = new GameObject(name);
            child.transform.SetParent(parent);
        }

        private void CreateSceneDataAsset(SceneAsset scene, string folderPath)
        {
            string path = $"{folderPath}/{scene.name}.asset";
            SceneData sceneData = ScriptableObject.CreateInstance<SceneData>();

            sceneData.SetScene(scene);

            AssetDatabase.CreateAsset(sceneData, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            AssetDatabase.SaveAssets();

            Debug.Log($"SceneData created at: {path}");
        }

        #endregion

        private string GetSceneFolderPath(string folderName)
        {
            return $"Assets/Scenes/{folderName}";
        }

        private string GetSceneAssetPath(string sceneFolderPath, string sceneName)
        {
            return $"{sceneFolderPath}/{sceneName}.unity";
        }

        private void CreateScene(string sceneAssetPath, Action AfterSceneOpened)
        {
            Scene newScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            EditorSceneManager.SaveScene(newScene, sceneAssetPath);
            EditorSceneManager.OpenScene(sceneAssetPath, OpenSceneMode.Single);

            AfterSceneOpened.Invoke();

            EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        }
    }
}