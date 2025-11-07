using Unity.VisualScripting;
using UnityEngine;

public class TempDialogManager : MonoBehaviour
{

    [SerializeField] private string salmonKey;
    [SerializeField] private string targetSceneName;
    [SerializeField] private GameObject dialogBox;
    
    private void FixedUpdate()
    {
        if (!dialogBox.activeInHierarchy)
        {
            PlayerPrefs.SetString(salmonKey, "true");
            SceneController.Instance.LoadScene(targetSceneName);
        }
    }
}
