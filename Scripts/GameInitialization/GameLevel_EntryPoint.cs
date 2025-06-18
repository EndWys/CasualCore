using Assets.CasualCore.Scripts.GameRunnerSystem;
using Assets.CasualCore.Scripts.SceneSystem;
using Assets.CasualCore.Scripts.ServiceLocatorSystem;

namespace Assets.CasualCore.Scripts.GameInitialization
{
    public class GameLevel_EntryPoint : SceneEntryPoint
    {
        private ILocatorLoader _locatorLoader;

        private GameRunner _gameRunner;

        protected override void SceneStartActions()
        {
            _locatorLoader = new ServiceLocatorLoader_Level();
            _locatorLoader.LoadLocator();

            //Initialization of all game components
            _gameRunner = new GameRunner();
            _gameRunner.StartGame();
        }

        protected override void SceneRunsActions()
        {
            _gameRunner.Run();
        }

        protected override void SceneExitActions()
        {
            _gameRunner.ExitGame();
            _locatorLoader.UnloadLocator();
        }
    }
}