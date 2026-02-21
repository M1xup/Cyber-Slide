using System;
using System.Collections.Generic;
using UnityEngine;

public class RunnerMapSpawner : MonoBehaviour
{
    [Header("RunningMap")]
    [SerializeField] private List<GameObject> _runnerMapPrefabs;
    public GameObject RunnerMap;
    private GameObject _lastRunnerMap;
    private GameObject _beforeLastRunnerMap;
    private Vector3 _lastRunnerMapPosition;
    public GameObject _mapClone;
    private int _numberOfDeletionsMap;
    private GameObject _mapClones;

    private void Awake()
    {
        _mapClones = new("MapClones");
    }

    private void Start()
    {
        _lastRunnerMapPosition = RunnerMap.transform.position;
        _mapClone = RunnerMap;
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
}
