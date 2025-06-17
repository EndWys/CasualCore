namespace Assets.CasualCore.Scripts.SceneSystem.Editor
{
    public interface IBootstrapSceneBuilder
    {
        void BuildBootstrapScene();
    }

    public interface IGameLevelSceneBuilder
    {
        void BuildGameLevelScene(string sceneName);
    }

    public interface IPopUpSceneBuilder
    {

    }
}