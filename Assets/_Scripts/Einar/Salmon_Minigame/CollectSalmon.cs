using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Xasu.HighLevel;
public class CollectSalmon : MonoBehaviour
{
    [SerializeField] TMP_Text counterText;
    private int salmonCount = 0;
    [SerializeField] int maxSalmonCount;

    [SerializeField] private string targetSceneName;

    [SerializeField] private string salmonKey;
    [SerializeField] private string requiredKey;
    [SerializeField] private string finishedSalmonKey;

    private void Start()
    {
        counterText.text = "Salmon caught: " + salmonCount.ToString() + "/" + maxSalmonCount.ToString();
        CompletableTracker.Instance.Initialized(SceneManager.GetActiveScene().name, CompletableTracker.CompletableType.Level);

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered with: " + other.gameObject.name);

        if (other.CompareTag("Salmon"))
        {
            salmonCount++;
            UpdateCounterText();
            Destroy(other.gameObject);
        }
    }

    private void UpdateCounterText()
    {
        Debug.Log("Updating counter text: " + salmonCount);
        counterText.text = "Salmon caught: " + salmonCount.ToString() +"/" + maxSalmonCount.ToString();
        if (salmonCount == maxSalmonCount)
        {
            PlayerPrefs.DeleteKey(salmonKey);
            PlayerPrefs.SetString(requiredKey, "true");
            PlayerPrefs.SetString(finishedSalmonKey, "true");
            PlayerPrefs.Save();
            CompletableTracker.Instance.Completed(SceneManager.GetActiveScene().name, CompletableTracker.CompletableType.Level).WithSuccess(true);
            SceneController.Instance.LoadScene(targetSceneName);
        }
    }

    public void GiveUpSalmonMinigame()
    {
            PlayerPrefs.DeleteKey(salmonKey);
            PlayerPrefs.SetString(requiredKey, "true");
            PlayerPrefs.SetString(finishedSalmonKey, "true");
            PlayerPrefs.Save();
            SceneController.Instance.LoadScene(targetSceneName);

        CompletableTracker.Instance.Completed(SceneManager.GetActiveScene().name, CompletableTracker.CompletableType.Level).WithSuccess(false);
    }
}
