using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private ScreenFade _screenFade;
    public void PlayGame()
    {
        Debug.Log("Play Game");
        _screenFade.PlayFade();
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
