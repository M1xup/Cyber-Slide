using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Backstage _backstage;
    
    public void RunnerScene()
    {
        _backstage.CloseHorizontalBackstage();
        Invoke("LoadBackRunnerScene", 0.6f);
    }
    
    private void LoadBackRunnerScene()
    {
        SceneManager.LoadScene("Runner");
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void AgainGame()
    {
        Time.timeScale = 1.0f;
        _backstage.CloseHorizontalBackstage();
        Invoke("LoadAgainGame", 0.6f);
    }
    
    private void LoadAgainGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void BackMainMenu()
    {
        Time.timeScale = 1.0f;
        _backstage.CloseVerticalBackstage();
        Invoke("LoadBackMainMenu", 0.6f);
    }

    private void LoadBackMainMenu()
    {
        SceneManager.LoadScene("MainScene");
    }
}
