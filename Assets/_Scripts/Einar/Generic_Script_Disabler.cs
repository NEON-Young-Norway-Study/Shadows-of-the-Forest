using UnityEngine;

public class Generic_Script_Disabler : MonoBehaviour
{
    public MonoBehaviour targetScript;
    public GameObject dialogueBox;

    private void Awake()
    {
        targetScript.enabled = false;
    }
    private void Update()
    {
        if (dialogueBox != null && targetScript != null)
        {
            if (!dialogueBox.activeSelf)
            {
                if (!targetScript.enabled)
                {
                    targetScript.enabled = true;
                }
            }
            else
            {

                if (dialogueBox.activeSelf)
                {
                    targetScript.enabled = false;
                }
            }
        }
    }
}
