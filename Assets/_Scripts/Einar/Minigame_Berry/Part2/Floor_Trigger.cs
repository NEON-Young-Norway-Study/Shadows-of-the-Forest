using UnityEngine;
using UnityEngine.SceneManagement;

public class Floor_Trigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Berry"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
