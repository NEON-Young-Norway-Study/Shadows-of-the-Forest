using System.Collections.Generic;
using UnityEngine;

public class Dialog_Prefs : MonoBehaviour
{
    [SerializeField] private List<GameObject> afterMinigameTriggers;
    [SerializeField] private List<GameObject> afterSchoolTriggers;
    [SerializeField] private List<GameObject> startOfDayTriggers;

    [SerializeField] string foodMinigame_Finished;
    [SerializeField] string school_Finished;

    void Start()
    {
        if (PlayerPrefs.HasKey(school_Finished))
        {
            SetActiveList(afterMinigameTriggers, false);
            SetActiveList(afterSchoolTriggers, true);
            SetActiveList(startOfDayTriggers, false);
        }
        else if (PlayerPrefs.HasKey(foodMinigame_Finished))
        {
            SetActiveList(afterMinigameTriggers, true);
            SetActiveList(afterSchoolTriggers, false);
            SetActiveList(startOfDayTriggers, false);
        }
        else
        {
            SetActiveList(afterMinigameTriggers, false);
            SetActiveList(afterSchoolTriggers, false);
            SetActiveList(startOfDayTriggers, true);
        }
    }
    private void SetActiveList(List<GameObject> objects, bool isActive)
    {
        foreach (var obj in objects)
        {
            if (obj != null)
                obj.SetActive(isActive);
        }
    }
}