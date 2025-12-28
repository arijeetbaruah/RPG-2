using System;
using RPG.ConfigServices;
using RPG.Services;
using UnityEngine;

public class TestSceneHandler : MonoBehaviour
{
    [SerializeField] ConfigDatabase _configDatabase;

    private void Start()
    {
        ServiceManager.Add(new ConfigService(_configDatabase));
    }
}
