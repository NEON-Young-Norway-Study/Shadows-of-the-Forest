using UnityEngine;

public class SceneTriggerArea : MonoBehaviour
{
    [SerializeField] private string targetSceneName;
    [SerializeField] private GameObject lockedText;
    [SerializeField] private GameObject finishedText;

    [SerializeField] private string requiredKey;
    [SerializeField] private string secondaryKey;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerPrefs.HasKey(requiredKey))
            {
                if (lockedText != null)
                    lockedText.SetActive(false);
                SceneController.Instance.LoadScene(targetSceneName);
            }
            else if (PlayerPrefs.HasKey(secondaryKey))
            {
                if (finishedText != null)
                    finishedText.SetActive(true);
            }
            else
            {
                if (lockedText != null)
                    lockedText.SetActive(true);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (lockedText != null)
            lockedText.SetActive(false);
        if (finishedText != null)
            finishedText.SetActive(false);
    }
}

