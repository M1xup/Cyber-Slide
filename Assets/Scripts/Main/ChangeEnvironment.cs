using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChangeEnvironment : MonoBehaviour
{
    [Header("Environments")]
    private static string _lastEnvironment;
    public string ActiveEnvironment;
    private List<string> Environments = new List<string>() {"cold night", "deep dusk", "epic blue sunset", 
        "epic glorious pink", "night moon burst", "space another planet" };
    
    [Header("Skyboxes")]
    [SerializeField] private Material _coldNightSkybox;
    [SerializeField] private Material _deepDuskSkybox;
    [SerializeField] private Material _epicBlueSunsetSkybox;
    [SerializeField] private Material _epicGloriousPinkSkybox;
    [SerializeField] private Material _nightMoonBurstSkybox;
    [SerializeField] private Material _spaceAnotherPlanetSkybox;
    
    [Header("LightingSettings")]
    [SerializeField] private LightingSettings _coldNightSettings;
    [SerializeField] private LightingSettings _deepDuskSettings;
    [SerializeField] private LightingSettings _epicBlueSunsetSettings;
    [SerializeField] private LightingSettings _epicGloriousPinkSettings;
    [SerializeField] private LightingSettings _nightMoonBurstSettings;
    [SerializeField] private LightingSettings _spaceAnotherPlanetSettings;

    private void Awake()
    {
        
        if (_lastEnvironment == null)
        {
            _lastEnvironment = Environments[Random.Range(0, Environments.Count)];
        }
        
        Change();
    }

    private void Change()
    {
        Environments.Remove(_lastEnvironment);
        ActiveEnvironment = Environments[Random.Range(0, Environments.Count)];

        switch (ActiveEnvironment)
        {
            case  "cold night":
                ColdNight();
                break;
            case "deep dusk":
                DeepDusk();
                break;
            case "epic blue sunset":
                EpicBlueSunset();
                break;
            case "epic glorious pink":
                EpicGloriousPink();
                break;
            case "night moon burst":
                NightMoonBurst();
                break;
            case "space another planet":
                SpaceAnotherPlanet();
                break;
        }
        
        _lastEnvironment = ActiveEnvironment;
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
    
    private void EpicBlueSunset()
    {
        RenderSettings.skybox = _epicBlueSunsetSkybox;
        Lightmapping.lightingSettings = _epicBlueSunsetSettings;
    }
    
    private void EpicGloriousPink()
    {
        RenderSettings.skybox = _epicGloriousPinkSkybox;
        Lightmapping.lightingSettings = _epicGloriousPinkSettings;
    }
    
    private void NightMoonBurst()
    {
        RenderSettings.skybox = _nightMoonBurstSkybox;
        Lightmapping.lightingSettings = _nightMoonBurstSettings;
    }
    
    private void SpaceAnotherPlanet()
    {
        RenderSettings.skybox = _spaceAnotherPlanetSkybox;
        Lightmapping.lightingSettings = _spaceAnotherPlanetSettings;
    }
}