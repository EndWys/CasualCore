using UnityEngine;

namespace Assets.CasualCore.Scripts.ObjecPoolSystem
{
    public abstract class PoolPrefabHolder<TObject> : ScriptableObject, IPrefabHolder<TObject> where TObject : Object, IPoolObject
    {
        public abstract TObject GetObjectPrefab();
    }
}