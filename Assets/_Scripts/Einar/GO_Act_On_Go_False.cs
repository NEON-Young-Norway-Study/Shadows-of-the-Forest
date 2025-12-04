using UnityEngine;

public class GO_Act_On_Go_False : MonoBehaviour
{
    [SerializeField] private GameObject GO_To_Activate;

    [SerializeField] private GameObject GO_To_Disable;

    void Update()
    {
        if (GO_To_Disable != null && !GO_To_Disable.activeSelf)
        {
            if (GO_To_Activate != null && !GO_To_Activate.activeSelf)
            {
                GO_To_Activate.SetActive(true);
            }
        }
    }
}
