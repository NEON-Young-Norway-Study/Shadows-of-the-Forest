using UnityEngine;

public class Unlock_Test : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerPrefs.HasKey("School_1");
    }
}
