using UnityEngine;
using UnityEngine.SceneManagement;

public class SchoolTriggerArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneController.Instance.LoadScene("Quiz_Minigame");
    }
}
