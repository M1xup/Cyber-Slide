using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Backstage : MonoBehaviour
{
    [Header("Backstage")]
    [SerializeField] private GameObject _vertical;
    [SerializeField] private GameObject _horizontal;
    
    [Header("Animations")]
    [SerializeField] public Animator _verticalBackstage;
    [SerializeField] public Animator _horizontalBackstage;
    
    [Header("AudioEffects")]
    [SerializeField] private AudioSource _nightCity;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Runner")
        {
            OpenHorizontalBackstage();
        }
        else if (SceneManager.GetActiveScene().name == "MainScene")
        {
            OpenVerticalBackstage();
            Invoke("PlayNightCity", 0.1f);
        }
    }

    private void OpenVerticalBackstage()
    {
        _vertical.SetActive(true);
        _verticalBackstage.Play("OpenVerticalBackstage");
    }
    public void CloseVerticalBackstage()
    {
        _vertical.SetActive(true);
        _verticalBackstage.Play("CloseVerticalBackstage");
    }
    
    private void OpenHorizontalBackstage()
    {
        _horizontal.SetActive(true);
        _horizontalBackstage.Play("OpenHorizontalBackstage");
    }
    public void CloseHorizontalBackstage()
    {
        _horizontal.SetActive(true);
        _horizontalBackstage.Play("CloseHorizontalBackstage");
    }

    private void PlayNightCity()
    {
        _nightCity.Play();
    }
}
