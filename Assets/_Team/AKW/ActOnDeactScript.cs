using UnityEngine;

public class ActOnDeactScript : MonoBehaviour
{

    public GameObject[] ThingsToActivate;


    private void OnDisable()
    {
        foreach (GameObject obj in ThingsToActivate)
        {
            obj.SetActive(true);
        }
        //ThingsToActivate.SetActive(true);
    }

    private void OnEnable()
    {
        Debug.Log("The {gameobject} is active");
    }
}
