using UnityEngine;
using UnityEngine.UI;

public class MultiChoiceDialogueScript : MonoBehaviour
{
    [Header("Option Buttons")]

    public Button optionA;
    public Button optionB;
    public Button optionC;
    public Button optionD;

    [Header("Dialogue paths")]

    public GameObject DialogueOptionA;
    public GameObject DialogueOptionB;
    public GameObject DialogueOptionC;
    public GameObject DialogueOptionD;

    [Header("Player Movement Control")]
    private MainCharacterController playerMovement;



    void Awake()
    {
        playerMovement = Object.FindFirstObjectByType<MainCharacterController>();
    }

    private void Start()
    {
        optionA.onClick.AddListener(DialogueSequenceA);
        optionB.onClick.AddListener(DialogueSequenceB);
        optionC.onClick.AddListener(DialogueSequenceC);
        optionD.onClick.AddListener(DialogueSequenceD);

        playerMovement.movementLocked = true;
    }

    public void DialogueSequenceA()
    {
        Debug.Log("Amazing");
        DialogueOptionA.SetActive(true);
        gameObject.SetActive(false);

        playerMovement.movementLocked = false;
    }

    public void DialogueSequenceB()
    {
        Debug.Log("Bisqut");
        DialogueOptionB.SetActive(true);
        gameObject.SetActive(false);

        playerMovement.movementLocked = false;
    }

    public void DialogueSequenceC()
    {
        Debug.Log("Coolio");
        DialogueOptionC.SetActive(true);
        gameObject.SetActive(false);

        playerMovement.movementLocked = false;
    }

    public void DialogueSequenceD()
    {
        Debug.Log("dialogueing 4");
        DialogueOptionD.SetActive(true);
        gameObject.SetActive(false);

        playerMovement.movementLocked = false;
    }
}
