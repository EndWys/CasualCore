using UnityEngine;

namespace Assets.CasualCore.Scripts.SceneSystem
{
    public abstract class SceneEntryPoint : MonoBehaviour
    {
        private bool _isStarted;

        private void Start()
        {
            if (Bootstrapper.IsBootstrapperLoaded)
            {
                SceneStartActions();
                _isStarted = true;
            }
        }

        protected abstract void SceneStartActions();

        private void Update()
        {
            if(_isStarted)
                SceneRunsActions();
        }

        protected abstract void SceneRunsActions();

        private void OnDestroy()
        {
            if (_isStarted)
                SceneExitActions();
        }

        protected abstract void SceneExitActions();
    }
}