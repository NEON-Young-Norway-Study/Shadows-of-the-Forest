using UnityEngine;

public class Dad_Prefs : MonoBehaviour
{
    [SerializeField] GameObject dad_Home;
    [SerializeField] GameObject dad_Dock;
    [SerializeField] GameObject dad_School;

    [SerializeField] string foodMinigame_Finished;
    [SerializeField] string school_Finished;

    void Start()
    {
        if (PlayerPrefs.HasKey(school_Finished))
        {
            dad_Home.SetActive(false);
            dad_Dock.SetActive(false);
            dad_School.SetActive(true);
        }
        else if (PlayerPrefs.HasKey(foodMinigame_Finished))
        {
            dad_Home.SetActive(false);
            dad_Dock.SetActive(true);
            dad_School.SetActive(false);
        }
        else
        {
            dad_Home.SetActive(true);
            dad_Dock.SetActive(false);
            dad_School.SetActive(false);
        }
    }
}
