using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndRunnerGame : MonoBehaviour
{
    [Header("EndGame")]
    [SerializeField] private CameraShake _camShake;
    [SerializeField] private DistanceCounter _disCounter;
    [SerializeField] private DualHooks _dh;
    [SerializeField] private PauseRunner _pauseRunner;
    [SerializeField] private LearningMenu _learningMenu;
    [SerializeField] private GameObject _hudUI;
    [SerializeField] private GameObject _endGameUI;
    [SerializeField] private TMP_Text _reasonText;
    [SerializeField] private TMP_Text _distanceText;
    [SerializeField] private Rigidbody _rb;
    
    public bool _endGame;
    private string _reason;
    
    [Header("ExitZone")]
    [SerializeField] private TMP_Text _remainingTimeText;
    [SerializeField] private int _exitedTime = 3;
    
    private float _timeRemaining;
    private bool _exited;
    
    [Header("ZeroSpeed")]
    [SerializeField] private Speedometer _speedometer;
    
    [Header("AudioEffects")]
    [SerializeField] private AudioSource _nightCity;
    
    [SerializeField] private AudioSource _rain;
    [SerializeField] private AudioSource _day;
    [SerializeField] private AudioSource _thaw;
    [SerializeField] private AudioSource _meteorRain;
    [SerializeField] private AudioSource _spaceAnotherPlanet;
    
    public List<AudioSource> _rotationBlades;
    private GameObject[] _blades;
    
    
    private void Update()
    {
        if (_timeRemaining > 0.0f && _exited)
        {
            _remainingTimeText.text = "Вы вышли за границы зоны!\nВернитесь обратно в течении: " + Mathf.RoundToInt(_timeRemaining).ToString() + "с";
            _timeRemaining -= Time.deltaTime;
        }

        if (_timeRemaining <= 0.0f && _exited && !_endGame && !_pauseRunner.Paused && !_learningMenu.IsLearningMenu)
        {
            _reason = "выход из зоны";
            EndGame();
            _remainingTimeText.text = string.Empty;
        }

        if (_speedometer.Speed <= 0 && _disCounter.AllDistance > 1 && !_endGame && !_pauseRunner.Paused && !_learningMenu.IsLearningMenu && !_pauseRunner._rbStopped)
        {
            _reason = "остановка";
            EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_endGame || _pauseRunner.Paused || _learningMenu.IsLearningMenu) return;
        
        if (other.gameObject.CompareTag("ExitZoneTrigger"))
        {
            if (!_exited)
            {
                _exited = true;
                _timeRemaining += _exitedTime;
            }
        }

        else if (other.gameObject.CompareTag("Blade"))
        {
            _reason = "столкновение с лопастью";
            EndGame();
        }
        
        else if (other.gameObject.CompareTag("Floor"))
        {
            _reason = "касание пола";
            EndGame();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ExitZoneTrigger"))
        {
            _exited = false;
            _timeRemaining = 0;
            _remainingTimeText.text = string.Empty;
        }
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        _endGame = true;
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
        _rotationBlades.Clear();
        _blades = null;

        foreach (var hooksShotSource in _dh._hooksShotSources)
        {
            hooksShotSource.Stop();
        }
        
        _rain.Stop();
        _day.Stop();
        _thaw.Stop();
        _meteorRain.Stop();
        _spaceAnotherPlanet.Stop();
        
        _nightCity.Play();
        
        _endGameUI.SetActive(true);
        _hudUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        _reasonText.text = "причина: " + _reason;
        _distanceText.text = "дистанция: " + Mathf.RoundToInt(_disCounter.AllDistance).ToString() + "м";
    }
    
    public void OpenLearningMenu()
    {
        _learningMenu.BeforeEndRunnerGame = true;
        _learningMenu.OpenLearningMenu();
    }
}
