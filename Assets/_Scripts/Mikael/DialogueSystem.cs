using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] string[] dialogueLines;
    [SerializeField] private float textSpeed = 0.05f;
    [SerializeField] private MonoBehaviour playerMovement;
    [SerializeField] private NPCMovement npc;
    [SerializeField] private string sceneToLoad;


    private int lineIndex;
    private CharacterController controller;

    void Start()
    {
        dialogueText.text = string.Empty;
        npc = Object.FindFirstObjectByType<NPCMovement>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<MainCharacterController>();
        }
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == dialogueLines[lineIndex])
            {
                nextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }

    void StartDialogue()
    {
        if (playerMovement != null)
        {
            ((MainCharacterController)playerMovement).movementLocked = true;
        }

        lineIndex = 0;
        StartCoroutine(TypeLine());
    }


    IEnumerator TypeLine()
    {
        foreach (char letter in dialogueLines[lineIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void nextLine()
    {
        if (lineIndex < dialogueLines.Length - 1)
        {
            lineIndex++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }
    
    void EndDialogue()
    {
        gameObject.SetActive(false);

        if (playerMovement != null)
        {
            ((MainCharacterController)playerMovement).movementLocked = false;
        }

        if (npc != null)
        {
            npc.MoveToNextCheckpoint();
        }

        // ✅ Only load a scene if one was assigned
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneController.Instance.LoadScene(sceneToLoad);
        }
        // gameObject.SetActive(false);

        // if (playerMovement != null)
        // {
        //     ((MainCharacterController)playerMovement).movementLocked = false;
        // }

        // if (npc != null)
        // {
        //     npc.MoveToNextCheckpoint();
        // }
    }
}
