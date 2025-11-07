using UnityEngine;
using TMPro;
public class CollectSalmon : MonoBehaviour
{
    [SerializeField] TMP_Text counterText;
    private int salmonCount = 0;
    [SerializeField] int maxSalmonCount;

    [SerializeField] private string targetSceneName;

    [SerializeField] private string salmonKey;
    [SerializeField] private string requiredKey;
    [SerializeField] private string finishedSalmonKey;


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
        counterText.text = "Salmon caught: " + salmonCount.ToString();
        if (salmonCount == maxSalmonCount)
        {
            PlayerPrefs.DeleteKey(salmonKey);
            PlayerPrefs.SetString(requiredKey, "true");
            PlayerPrefs.SetString(finishedSalmonKey, "true");
            PlayerPrefs.Save();
            SceneController.Instance.LoadScene(targetSceneName);
        }
    }
}
