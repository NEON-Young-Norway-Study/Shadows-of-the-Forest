using UnityEngine;

public class PlayerPrefsReseter : MonoBehaviour
{
    private void Start()
    {
    PlayerPrefs.DeleteAll();
    PlayerPrefs.Save();
    }
}
