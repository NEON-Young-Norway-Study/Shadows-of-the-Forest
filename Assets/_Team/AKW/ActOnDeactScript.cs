using UnityEngine;

public class ActOnDeactScript : MonoBehaviour
{

    public GameObject[] ThingsToActivate;
    public GameObject[] Deactivate;


    private void OnDisable()
    {
        foreach (GameObject obj in ThingsToActivate)
        {
            obj.SetActive(true);
        }
        
        foreach (GameObject obj in Deactivate)
        {
            obj.SetActive(false);
        }
    }

    private void OnEnable()
    {
        Debug.Log("The {gameobject} is active");
    }
}
