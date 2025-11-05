using UnityEngine;

public class SimpleSceneTrigger : MonoBehaviour
{
    [SerializeField] private string targetSceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneController.Instance.LoadScene(targetSceneName);
        }
    }
}
