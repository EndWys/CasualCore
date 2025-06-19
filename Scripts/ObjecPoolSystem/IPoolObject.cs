using System.Collections;

namespace Assets.CasualCore.Scripts.ObjecPoolSystem
{
    public interface IPoolObject
    {
        public void OnGetFromPool();

        public void OnReleaseToPool();
    }
}