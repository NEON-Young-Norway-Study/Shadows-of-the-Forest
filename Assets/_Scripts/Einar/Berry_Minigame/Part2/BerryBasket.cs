using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BerryBasket : MonoBehaviour
{
    [SerializeField] private string targetSceneName;
    [SerializeField] float counter;
    [SerializeField] float maxCollected;

    [SerializeField] private string berryKey;
    [SerializeField] private string requiredKey;
    [SerializeField] private string finishedBerryKey;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object has the tag "Poisonous"
        if (other.CompareTag("Poison"))
        {
            // Reset the scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        // Optional: handle good berries or other objects
        else if (other.CompareTag("Berry"))
        {
            counter++;
            Destroy(other.gameObject);
            if (counter == maxCollected)
            {
                PlayerPrefs.DeleteKey(berryKey);
                PlayerPrefs.SetString(requiredKey, "true");
                PlayerPrefs.SetString(finishedBerryKey, "true");
                PlayerPrefs.Save();

                Cursor.visible = true;
                SceneController.Instance.LoadScene(targetSceneName);
            }
        }
    }
}
