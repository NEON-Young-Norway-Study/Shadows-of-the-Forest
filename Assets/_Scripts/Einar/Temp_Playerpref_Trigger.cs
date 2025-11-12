using UnityEngine;

public class Temp_Playerpref_Trigger : MonoBehaviour
{
    [SerializeField] string key;

    private void OnTriggerEnter(Collider other)
    {
        PlayerPrefs.SetString(key, "true");
    }
}
