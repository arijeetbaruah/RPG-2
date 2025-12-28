using UnityEngine;

namespace RPG.Services
{
    public class ServiceHandler : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            ServiceManager.Update();
        }
    }
}
