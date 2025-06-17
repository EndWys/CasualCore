using Assets.CasualCore.Scripts.SceneSystem;
using Assets.CasualCore.Scripts.ServiceLocatorSystem;

namespace Assets.CasualCore.Scripts.GameInitialization
{
    public class GameLevel_EntryPoint : SceneEntryPoint
    {
        private ILocatorLoader _locatorLoader;

        protected override void SceneStartActions()
        {
            _locatorLoader = new ServiceLocatorLoader_Level();
            _locatorLoader.LoadLocator();

            //Initialization of all game components
        }

        protected override void SceneRunsActions()
        {
            
        }

        protected override void SceneExitActions()
        {
            _locatorLoader.UnloadLocator();
        }
    }
}