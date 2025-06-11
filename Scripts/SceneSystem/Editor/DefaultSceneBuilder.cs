using Assets.CasualCore.Scripts.Runtime.SceneSystem;
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

            if (!AssetDatabase.IsValidFolder("Assets/Scenes"))
                AssetDatabase.CreateFolder("Assets", "Scenes");

            if (!AssetDatabase.IsValidFolder(sceneFolderPath))
                AssetDatabase.CreateFolder("Assets/Scenes", sceneName);

            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            EditorSceneManager.SaveScene(scene, sceneAssetPath);
            EditorSceneManager.OpenScene(sceneAssetPath, OpenSceneMode.Single);

            GameObject bootstrapperGO = new GameObject("Bootstrapper");

            var bootstrapperType = typeof(Bootstrapper);
            if (bootstrapperType != null)
            {
                bootstrapperGO.AddComponent(bootstrapperType);
            }
            else
            {
                Debug.LogWarning("Bootstrapper.cs not found. Add the script to the object manually.");
            }

            EditorSceneManager.SaveScene(SceneManager.GetActiveScene());

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

            if (!AssetDatabase.IsValidFolder("Assets/Scenes"))
                AssetDatabase.CreateFolder("Assets", "Scenes");

            string sceneFolderPath = $"Assets/Scenes/{sceneName}";
            if (!AssetDatabase.IsValidFolder(sceneFolderPath))
                AssetDatabase.CreateFolder("Assets/Scenes", sceneName);

            string sceneAssetPath = $"{sceneFolderPath}/{sceneName}.unity";

            Scene newScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            EditorSceneManager.SaveScene(newScene, sceneAssetPath);
            EditorSceneManager.OpenScene(sceneAssetPath, OpenSceneMode.Single);

            CreateHierarchy();

            EditorSceneManager.SaveScene(SceneManager.GetActiveScene());

            SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(sceneAssetPath);
            CreateSceneDataAsset(sceneAsset, sceneFolderPath);
        }

        private void CreateHierarchy()
        {
            // ===== BOOTSTRAPPING =====
            var bootstrapping = CreateEmptyGroup("===== BOOTSTRAPPING =====");
            CreateChild("GameLevel_EntryPoint", bootstrapping.transform);
            CreateChild("GameLevel_ServiceLoader", bootstrapping.transform);

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
    }
}