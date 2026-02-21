using System;
using UnityEngine;

public class LearningMenu : MonoBehaviour
{
    [SerializeField] private EndRunnerGame _endGame;
    [SerializeField] private PauseRunner _pauseRunner;
    [SerializeField] private GameObject _learningMenuUI;
    [SerializeField] private GameObject _hudUI;
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private GameObject _endGameUI;
    [SerializeField] private Rigidbody _rb;
    static bool FirstlaunchRunner = true;
    public bool BeforeEndRunnerGame;
    public bool BeforePauseRunner;
    public bool IsLearningMenu;

    private void Start()
    {
        if (FirstlaunchRunner)
        {
            FirstlaunchRunner  = false;
            Invoke("OpenLearningMenu", 0.6f);
        }
    }

    public void OpenLearningMenu()
    {
        Time.timeScale = 0f;
        IsLearningMenu = true;
        
        if (BeforeEndRunnerGame)
        {
            _endGameUI.SetActive(false);
        }
        else if (BeforePauseRunner)
        {
            _pauseUI.SetActive(false);
        }
        else
        {
            _hudUI.SetActive(false);
            _rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        
        _learningMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    
    public void CloseLearningMenu()
    {
        _learningMenuUI.SetActive(false);
        IsLearningMenu = false;
        
        if (BeforeEndRunnerGame)
        {
            _endGame.EndGame();
            BeforeEndRunnerGame  = false;
        }
        else if (BeforePauseRunner)
        {
            _pauseRunner.Pause();
            BeforePauseRunner  = false;
        }
        else
        {
            Time.timeScale = 1f;
            
            _rb.constraints = RigidbodyConstraints.FreezeRotation;
            
            _hudUI.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
