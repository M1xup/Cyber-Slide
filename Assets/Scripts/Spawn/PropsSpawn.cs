using System.Collections.Generic;
using UnityEngine;

public class PropsSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> _propsPrefabs;
    [SerializeField] private List<Transform> _propsSpawnPoints;
    [SerializeField] private float _delayTime = 0.1f;
    private GameObject[] _leftPropsSpawnPointsWithTag;
    private GameObject[] _rightPropsSpawnPointsWithTag;
    private GameObject _props;
    private int _numberOfDeletions;

    private void Awake()
    {
        SpawnProps();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NewSectionTrigger"))
        {
            Invoke("SpawnProps", _delayTime);
        }
        
        else if (other.gameObject.CompareTag("DestroySectionTrigger"))
        {
            if (_numberOfDeletions == 3)
            {
                _numberOfDeletions = 0;
            }
        }
    }

    private void SpawnProps()
    {
        _numberOfDeletions += 1;
        _leftPropsSpawnPointsWithTag = GameObject.FindGameObjectsWithTag("LeftPropsSpawnPoint");
        _rightPropsSpawnPointsWithTag = GameObject.FindGameObjectsWithTag("RightPropsSpawnPoint");
        for (int i = 0; i < _leftPropsSpawnPointsWithTag.Length; i += 1)
        {
            _propsSpawnPoints.Add(_leftPropsSpawnPointsWithTag[i].GetComponent<Transform>());
        }
        for (int i = 0; i < _rightPropsSpawnPointsWithTag.Length; i += 1)
        {
            _propsSpawnPoints.Add(_rightPropsSpawnPointsWithTag[i].GetComponent<Transform>());
        }


        if (_propsPrefabs != null)
        {
            foreach (var point in _propsSpawnPoints)
            {
                var _randomPrafab = Random.Range(0, _propsPrefabs.Count);

                var _propsRotation = new Quaternion();
                if (point.tag == "LeftPropsSpawnPoint")
                {
                    _propsRotation = _propsPrefabs[_randomPrafab].transform.localRotation;
                }
                else if (point.tag == "RightPropsSpawnPoint")
                {
                    _propsRotation = _propsPrefabs[_randomPrafab].transform.localRotation * Quaternion.Euler(0, 180, 0);
                }

                _props = Instantiate(_propsPrefabs[_randomPrafab], point.position, _propsRotation);
                _props.name = "Props";

                if (_numberOfDeletions == 1)
                {
                    _props.tag = "DeleteBuildingBefore";
                }
                else if (_numberOfDeletions == 2)
                {
                    _props.tag = "DeleteBuildingLater";
                }
                else if (_numberOfDeletions == 3)
                {
                    _props.tag = "DeleteBuilding";
                }
            }
        }

        for (int i = 0; i < _leftPropsSpawnPointsWithTag.Length; i += 1)
        {
            Destroy(_leftPropsSpawnPointsWithTag[i]);
        }
        for (int i = 0; i < _rightPropsSpawnPointsWithTag.Length; i += 1)
        {
            Destroy(_rightPropsSpawnPointsWithTag[i]);
        }
        _propsSpawnPoints.Clear();
    }
}
