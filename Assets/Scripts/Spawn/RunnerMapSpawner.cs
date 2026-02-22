using System;
using System.Collections.Generic;
using UnityEngine;

public class RunnerMapSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ChangeEnvironment _changeEnvironment;
    
    [Header("RunningMap")]
    [SerializeField] private List<GameObject> _runnerMapPrefabs;
    public GameObject RunnerMap;
    private GameObject _lastRunnerMap;
    private GameObject _beforeLastRunnerMap;
    private Vector3 _lastRunnerMapPosition;
    public GameObject _mapClone;
    private int _numberOfDeletionsMap;
    private GameObject _mapClones;
    
    [Header("Environment")]
    private GameObject[] _blizzard;
    private GameObject[] _rain;
    private GameObject[] _snow;
    private GameObject[] _meteorRain;
    private void Awake()
    {
        _mapClones = new("MapClones");
    }

    private void Start()
    {
        _lastRunnerMapPosition = RunnerMap.transform.position;
        _mapClone = RunnerMap;
        
        ChangeEnvironment();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NewSectionTrigger"))
        {
            _numberOfDeletionsMap += 1;
            var _randomPrafab = UnityEngine.Random.Range(0, _runnerMapPrefabs.Count);

            if (_numberOfDeletionsMap == 1)
            {
                _beforeLastRunnerMap = _mapClone;
            }
            else if (_numberOfDeletionsMap == 2)
            {
                _lastRunnerMap = _mapClone;
            }
            _mapClone = Instantiate(_runnerMapPrefabs[_randomPrafab], _lastRunnerMapPosition + new Vector3(0, 0, 120), Quaternion.identity);
            _mapClone.name = "MapClone";
            _mapClone.transform.parent = _mapClones.transform;
            _lastRunnerMapPosition = _mapClone.transform.position;

            ChangeEnvironment();

            Destroy(other.gameObject);
        }

        else if (other.gameObject.CompareTag("DestroySectionTrigger"))
        {
            if (_numberOfDeletionsMap == 2)
            {
                Destroy(_beforeLastRunnerMap);
                _numberOfDeletionsMap = 0;
            }
            else if (_numberOfDeletionsMap == 1)
            {
                Destroy(_lastRunnerMap);
            }
            Destroy(other.gameObject);
        }
    }

    private void ChangeEnvironment()
    {
        _blizzard = GameObject.FindGameObjectsWithTag("Blizzard");
        _rain = GameObject.FindGameObjectsWithTag("Rain");
        _snow = GameObject.FindGameObjectsWithTag("Snow");
        _meteorRain = GameObject.FindGameObjectsWithTag("MeteorRain");
        
        switch (_changeEnvironment.ActiveEnvironment)
        {
            case "cold night":
                foreach (var snow in _snow)
                {
                    snow.SetActive(false);
                }
                
                foreach (var meteorRain in _meteorRain)
                {
                    meteorRain.SetActive(false);
                }
                break;
                
            case "deep dusk":
                foreach (var rain in _rain)
                {
                    rain.SetActive(false);
                }
                
                foreach (var meteorRain in _meteorRain)
                {
                    meteorRain.SetActive(false);
                }
                break;
                
            case "epic blue sunset":
                foreach (var rain in _rain)
                {
                    rain.SetActive(false);
                }
                
                foreach (var meteorRain in _meteorRain)
                {
                    meteorRain.SetActive(false);
                }
                break;
                
            case "epic glorious pink":
                foreach (var rain in _rain)
                {
                    rain.SetActive(false);
                }
                
                foreach (var snow in _snow)
                {
                    snow.SetActive(false);
                }
                break;
                
            case "night moon burst":
                foreach (var blizzard in _blizzard)
                {
                    blizzard.SetActive(false);
                }
                
                foreach (var rain in _rain)
                {
                    rain.SetActive(false);
                }
               
                foreach (var meteorRain in _meteorRain)
                {
                    meteorRain.SetActive(false);
                }
                break;
                
            case "space another planet":
                foreach (var rain in _rain)
                {
                    rain.SetActive(false);
                }
                
                foreach (var snow in _snow)
                {
                    snow.SetActive(false);
                }
                
                foreach (var meteorRain in _meteorRain)
                {
                    meteorRain.SetActive(false);
                }
                break;
        }
        _blizzard = null;
        _rain = null;
        _snow = null;
        _meteorRain = null;
    }
}
