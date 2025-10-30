using UnityEngine;
using UnityEngine.SceneManagement;

public class BerryBasket : MonoBehaviour
{
    [SerializeField] private string targetSceneName;
    [SerializeField] float counter;
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
            if (counter == 3)
            {
                SceneController.Instance.LoadScene(targetSceneName);
            }
        }
    }
}
