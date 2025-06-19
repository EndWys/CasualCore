using UnityEngine;
using UnityEngine.Pool;

namespace Assets.CasualCore.Scripts.ObjecPoolSystem
{
    public abstract class GenericObjectPool<TObject> where TObject : Object, IPoolObject
    {
        protected ObjectPool<TObject> _pool;

        protected IPrefabHolder<TObject> _prefabHolder;

        protected abstract bool _collectionCheck { get; }
        protected abstract int _defaultCapacity { get; }

        public GenericObjectPool(IPrefabHolder<TObject> prefabHolder)
        {
            _prefabHolder = prefabHolder;

            _pool = new ObjectPool<TObject>(
                createFunc: CratePoolObject,
                actionOnGet: OnGetObjectFromPool,
                actionOnRelease: OnReleaseObjectToPool,
                actionOnDestroy: Object.Destroy,
                collectionCheck: _collectionCheck,
                defaultCapacity: _defaultCapacity
            );
        }

        protected abstract TObject CratePoolObject();
        protected virtual void OnGetObjectFromPool(TObject poolObject)
        {
            poolObject.OnGetFromPool();
        }
        protected virtual void OnReleaseObjectToPool(TObject poolObject)
        {
            poolObject.OnReleaseToPool();
        }

        public virtual TObject GetObject()
        {
            return _pool.Get();
        }

        public virtual void ReleaseObject(TObject poolObject)
        {
            _pool.Release(poolObject);
        }
    }
}