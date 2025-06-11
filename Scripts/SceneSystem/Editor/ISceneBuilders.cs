namespace Assets.CasualCore.Scripts.SceneSystem.Editor
{
    public interface IBootstrapSceneBuilder
    {
        public void BuildBootstrapScene();
    }

    public interface IGameLevelSceneBuilder
    {
        public void BuildGameLevelScene(string sceneName);
    }

    public interface IPopUpSceneBuilder
    {

    }
}