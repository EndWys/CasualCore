using Assets._Project.Scripts.ServiceLocatorSystem;

namespace Assets.CasualCore.Scripts.ServiceLocatorSystem
{
    public class ServiceLocatorLoader_Global : ILocatorLoader
    {
        private ServiceLocator _locator;

        public void LoadLocator()
        {
            _locator = ServiceLocator.CreateGlobalSceneServiceLocator();

            RegisterAllServices();
        }

        public void UnloadLocator()
        {
            if (_locator == null)
                return;

            UnregisterAllServices();
        }

        private void RegisterAllServices()
        {

        }

        private void UnregisterAllServices()
        {

        }
    }
}