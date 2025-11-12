using UnityEngine;

public class Dialog_Prefs : MonoBehaviour
{
    [SerializeField] private GameObject dialog_trigger_1;
    [SerializeField] private GameObject dialog_trigger_2;
    [SerializeField] private GameObject dialog_trigger_3;

    [SerializeField] string foodMinigame_Finished;
    [SerializeField] string school_Finished;

    void Start()
    {
        {

            if (PlayerPrefs.HasKey(foodMinigame_Finished))
            {
                dialog_trigger_1.SetActive(true);
                dialog_trigger_2.SetActive(false);
                dialog_trigger_3.SetActive(false);
            }
            else if (PlayerPrefs.HasKey(school_Finished))
            {
                dialog_trigger_1.SetActive(false);
                dialog_trigger_2.SetActive(true);
                dialog_trigger_3.SetActive(false);
            }
            else
            {
                dialog_trigger_1.SetActive(true);
                dialog_trigger_2.SetActive(false);
                dialog_trigger_3.SetActive(false);
            }
        }
    }
}
