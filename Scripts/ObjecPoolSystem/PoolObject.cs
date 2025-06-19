using UnityEngine;

namespace Assets.CasualCore.Scripts.ObjecPoolSystem
{
    public abstract class PoolObject : MonoBehaviour, IPoolObject
    {
        public abstract void OnGetFromPool();

        public abstract void OnReleaseToPool();
    }
}