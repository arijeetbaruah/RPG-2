using System.Collections.Generic;

namespace RPG.Services
{
    public static class ServiceManager
    {
        private static Dictionary<System.Type, IService> _activeServices = new();

        public static void Add<T>(T service) where T : IService
        {
            if (_activeServices.TryAdd(typeof(T), service))
               service.Initialize();
        }

        public static T Get<T>() where T : class, IService
        {
            if (_activeServices.TryGetValue(typeof(T), out var service))
            {
                return (T)service;
            }

            return null;
        }

        public static void Update()
        {
            foreach (var service in _activeServices.Values)
            {
                service.Update();
            }
        }

        public static void Remove<T>() where T : IService
        {
            _activeServices.Remove(typeof(T));
        }
    }
}
