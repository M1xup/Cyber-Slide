using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChangeEnvironment : MonoBehaviour
{
    [SerializeField] private List<string> Environments = new List<string>() {"cold night", "deep dusk"};
    [SerializeField] private Material _coldNightSkybox;
    [SerializeField] private Material _deepDuskSkybox;
    [SerializeField] private LightingSettings _coldNightSettings;
    [SerializeField] private LightingSettings _deepDuskSettings;
    private static string _lastEnvironment;

    private void Awake()
    {
        
        if (_lastEnvironment == null)
        {
            _lastEnvironment = Environments[Random.Range(0, Environments.Count)];
        }
        
        if (_lastEnvironment == Environments[0])
        {
            Environments.RemoveAt(0);
            DeepDusk();
            _lastEnvironment = Environments[1];
        }
        else if (_lastEnvironment == Environments[1])
        {
            Environments.RemoveAt(1);
            ColdNight();
            _lastEnvironment = Environments[0];
        }
    }

    private void ColdNight()
    {
        RenderSettings.skybox = _coldNightSkybox;
        Lightmapping.lightingSettings = _coldNightSettings;
    }
    
    private void DeepDusk()
    {
        RenderSettings.skybox = _deepDuskSkybox;
        Lightmapping.lightingSettings = _deepDuskSettings;
    }
}
