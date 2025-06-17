using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.ServiceLocatorSystem
{
    public interface IService 
    {
        void OnRegister();
        void OnUnregister();
    }
    public class ServiceLocator
    {
        public static ServiceLocator Global { get; private set; }

        public static ServiceLocator Local { get; private set; }

        public static ServiceLocator CreateLocalSceneServiceLocator()
        {
            if (Local == null)
                Local = new ServiceLocator();

            return Local;
        }

        public static ServiceLocator CreateGlobalSceneServiceLocator()
        {
            if (Global == null)
                Global = new ServiceLocator();

            return Local;
        }

        private readonly Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        public void Register<T>(T service) where T : IService
        {
            var key = typeof(T);
            if (_services.ContainsKey(key))
            {
                return;
            }

            _services.Add(key, service);
            service.OnRegister();

            return;
        }

        public void Unregister<T>() where T : IService
        {
            var key = typeof(T);
            if (!_services.ContainsKey(key))
            {
                return;
            }

            IService service = _services[key];

            _services.Remove(key);

            service.OnUnregister();
        }

        public T Get<T>() where T : IService
        {
            var key = typeof(T);
            if (!_services.ContainsKey(key))
            {
                Debug.LogError($"{key} not registered service!");
                throw new InvalidOperationException();
            }

            return (T)_services[key];
        }
    }
}