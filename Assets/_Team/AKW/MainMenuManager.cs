using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject funnyThingIDK; 

    public void PlayGame()
    {
        SceneManager.LoadScene("GameSceneName"); // replace with the acual scene whene thats added
    }

    public void Options()
    {
        SceneManager.LoadScene("OptionsScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
