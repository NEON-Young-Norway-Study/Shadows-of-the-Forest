using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

// oh boy, can't wait till this is scrapet by next week

public class MainMenuManager : MonoBehaviour
{
    public GameObject funnyThingIDK; // should probobly remove this... i'm not gonna do it, you do it


    // For copy-pasteing to add new scene to load on function
    /*
    public void nameFunction()
    {
        SceneManager.LoadScene("name of scene");
    }
    */


    public void PlayGame()
    {
        SceneManager.LoadScene("Overworld_T1"); // replace with the acual scene whene thats added
    }

    public void Options()
    {
        SceneManager.LoadScene("OptionsScene"); // do we even want/need this?
    }

    public void QuitGame()
    {
        Application.Quit(); // yanomás, habssat, q'wit
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // to get back to main menu
    }

    public void SadSceneTest()
    {
        SceneManager.LoadScene("Cutscene1SadBears"); 
        // just to test the cutscene, but maybe we can use this script to load every scene when needed?
    }

    public void TheForbiddenScene()
    {
        SceneManager.LoadScene("TestScene");
    }

    public void T1()
    {
        SceneManager.LoadScene("Overworld_T1");
    }
}
