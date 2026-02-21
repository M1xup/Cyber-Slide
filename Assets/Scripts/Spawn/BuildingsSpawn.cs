using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingsSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> _buildingsPrefabs;
    [SerializeField] private List<Transform> _buildingSpawnPoints;
    [SerializeField] private float _delayTime = 0.1f;
    private GameObject[] _leftBuildingSpawnPointsWithTag;
    private GameObject[] _rightBuildingSpawnPointsWithTag;
    private GameObject[] _buildingsDeleteWithTag;
    private GameObject _building;
    public int _numberOfDeletions;

    private void Awake()
    {
        SpawnBuildings();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NewSectionTrigger"))
        {
            Invoke("SpawnBuildings", _delayTime);
        }
        
        else if (other.gameObject.CompareTag("DestroySectionTrigger"))
        {
            if (_numberOfDeletions == 1)
            {
                _buildingsDeleteWithTag = GameObject.FindGameObjectsWithTag("DeleteBuildingLater");
                for (int i = 0; i < _buildingsDeleteWithTag.Length; i += 1)
                {
                    Destroy(_buildingsDeleteWithTag[i]);
                }
            }

            else if (_numberOfDeletions == 2)
            {
                _buildingsDeleteWithTag = GameObject.FindGameObjectsWithTag("DeleteBuilding");
                for (int i = 0; i < _buildingsDeleteWithTag.Length; i += 1)
                {
                    Destroy(_buildingsDeleteWithTag[i]);
                }
            }


            else if (_numberOfDeletions == 3)
            {
                _buildingsDeleteWithTag = GameObject.FindGameObjectsWithTag("DeleteBuildingBefore");
                for (int i = 0; i < _buildingsDeleteWithTag.Length; i += 1)
                {
                    Destroy(_buildingsDeleteWithTag[i]);
                }
                _numberOfDeletions = 0;
            }

        }
    }

    private void SpawnBuildings()
    {
        _numberOfDeletions += 1;
        _leftBuildingSpawnPointsWithTag = GameObject.FindGameObjectsWithTag("LeftBuildingSpawnPoint");
        _rightBuildingSpawnPointsWithTag = GameObject.FindGameObjectsWithTag("RightBuildingSpawnPoint");
        for (int i = 0; i < _leftBuildingSpawnPointsWithTag.Length; i += 1)
        {
            _buildingSpawnPoints.Add(_leftBuildingSpawnPointsWithTag[i].GetComponent<Transform>());
        }
        for (int i = 0; i < _rightBuildingSpawnPointsWithTag.Length; i += 1)
        {
            _buildingSpawnPoints.Add(_rightBuildingSpawnPointsWithTag[i].GetComponent<Transform>());
        }


        if (_buildingsPrefabs != null)
        {
            foreach (var point in _buildingSpawnPoints)
            {
                var _randomPrafab = Random.Range(0, _buildingsPrefabs.Count);
                
                var _buildingRotation = new Quaternion();
                if (point.tag == "LeftBuildingSpawnPoint")
                {
                    _buildingRotation = _buildingsPrefabs[_randomPrafab].transform.localRotation;
                }
                else if (point.tag == "RightBuildingSpawnPoint")
                {
                    _buildingRotation = _buildingsPrefabs[_randomPrafab].transform.localRotation * Quaternion.Euler(0, 180, 0);
                }
                
                _building = Instantiate(_buildingsPrefabs[_randomPrafab], point.position, _buildingRotation);
                _building.name = "Building";

                if (_numberOfDeletions == 1)
                {
                    _building.tag = "DeleteBuildingBefore";
                }
                else if (_numberOfDeletions == 2)
                {
                    _building.tag = "DeleteBuildingLater";
                }
                else if ( _numberOfDeletions == 3)
                {
                    _building.tag = "DeleteBuilding";
                }
            }
        }

        for (int i = 0; i < _leftBuildingSpawnPointsWithTag.Length; i += 1)
        {
            Destroy(_leftBuildingSpawnPointsWithTag[i]);
        }
        for (int i = 0; i < _rightBuildingSpawnPointsWithTag.Length; i += 1)
        {
            Destroy(_rightBuildingSpawnPointsWithTag[i]);
        }
        _buildingSpawnPoints.Clear();
    }
}