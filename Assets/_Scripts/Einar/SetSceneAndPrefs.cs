using UnityEngine;

public class SetSceneAndPrefs : MonoBehaviour
{
    [SerializeField] private string targetSceneName;
    [SerializeField] private string key;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneController.Instance.LoadScene(targetSceneName);
        }
    }

    public void loadSceneAndSetPlayerPref()
    {
        PlayerPrefs.SetString(key, "true");
        SceneController.Instance.LoadScene(targetSceneName);
    }
}
