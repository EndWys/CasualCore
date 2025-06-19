using UnityEngine;

namespace Assets.CasualCore.Scripts.ObjecPoolSystem
{
    public interface IPrefabHolder<TObject> where TObject : Object, IPoolObject
    {
        TObject GetObjectPrefab();
    }
}