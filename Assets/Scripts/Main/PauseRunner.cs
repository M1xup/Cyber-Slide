using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseRunner : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] private CameraShake _camShake;
    [SerializeField] private DualHooks _dh;
    [SerializeField] private EndRunnerGame _endGame;
    [SerializeField] private LearningMenu _learningMenu;
    [SerializeField] private Speedometer _speedometer;
    [SerializeField] private DistanceCounter _disCounter;
    [SerializeField] private GameObject _hudUI;
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private TMP_Text _speedText;
    [SerializeField] private TMP_Text _distanceText;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private int _stoppedTime = 1;
    private float _timeRemaining;
    public bool _rbStopped;
    public bool Paused;
    
    [Header("AudioEffects")]
    [SerializeField] private AudioSource _nightCity;
    
    [SerializeField] private AudioSource _rain;
    public List<AudioSource> _rotationBlades;
    private GameObject[] _blades;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Pause();
        }
        
        if (_timeRemaining > 0.0f)
        {
            _timeRemaining -= Time.deltaTime;
        }
        
        if (_timeRemaining <= 0.0f)
        {
            _rbStopped = false;
        }
    }

    public void Pause()
    {
                if (_endGame._endGame || Paused || _learningMenu.IsLearningMenu) return;

                Paused = true;
                if (!_learningMenu.BeforePauseRunner)
                {
                    Time.timeScale = 0f;
                    _camShake.CanShake = false;
                    _rb.constraints = RigidbodyConstraints.FreezeAll;

                    _blades = GameObject.FindGameObjectsWithTag("RotationBladeSound");
                    foreach (var blade in _blades)
                    {
                        _rotationBlades.Add(blade.GetComponent<AudioSource>());
                    }
                    foreach (var rotationBlade in _rotationBlades)
                    {
                        rotationBlade.Stop();
                    }
                    _blades = null;
                    
                    foreach (var hooksShotSource in _dh._hooksShotSources)
                    {
                        hooksShotSource.Stop();
                    }
                    
                    _rain.Stop();
                    _nightCity.Play();
                    
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                }

                _hudUI.SetActive(false);
                _pauseUI.SetActive(true);
                _speedText.text = "скорость: " + _speedometer.Speed.ToString() + "км/ч";
                _distanceText.text = "дистанция: " + Mathf.RoundToInt(_disCounter.AllDistance).ToString() + "м";
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        _camShake.CanShake = true;
        Paused = false;
        
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        
        foreach (var rotationBlade in _rotationBlades)
        {
            rotationBlade.Play();
        }
        _rotationBlades.Clear();
        
        _nightCity.Stop();
        _rain.Play();

        _hudUI.SetActive(true);
        _pauseUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _rbStopped = true;
        _timeRemaining += _stoppedTime;
    }
    
    public void OpenLearningMenu()
    {
        Paused = false;
        _learningMenu.BeforePauseRunner = true;
        _learningMenu.OpenLearningMenu();
    }
}
