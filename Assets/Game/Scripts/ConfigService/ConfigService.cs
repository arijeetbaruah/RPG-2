using System.Collections.Generic;
using System.Linq;
using RPG.Services;

namespace RPG.ConfigServices
{
    public class ConfigService : IService
    {
        public ConfigDatabase _database;
        
        public Dictionary<System.Type, BaseConfig> _services;

        public ConfigService(ConfigDatabase database)
        {
            _database = database;
            _services = _database.Configs.ToDictionary(c => c.GetType(), c => c);
        }
        
        public void Initialize()
        {
        }

        public void Update()
        {
        }

        public void OnDestroy()
        {
        }

        public T GetConfig<T>() where T : BaseConfig
        {
            if (_services.TryGetValue(typeof(T), out BaseConfig config))
            {
                return (T)config;
            }
            
            return null;
        }
    }
}
