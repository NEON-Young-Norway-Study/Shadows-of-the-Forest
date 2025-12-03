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

            if (playerMovement != null)
            {
                playerMovement.movementLocked = true;
            }
        }

        public void DialogueSequenceA()
        {
            DialogueOptionA.SetActive(true);
            CloseMenu();
        }

        public void DialogueSequenceB()
        {
            DialogueOptionB.SetActive(true);
            CloseMenu();
        }

        public void DialogueSequenceC()
        {
            DialogueOptionC.SetActive(true);
            CloseMenu();
        }

        public void DialogueSequenceD()
        {
            DialogueOptionD.SetActive(true);
            CloseMenu();
        }

        private void CloseMenu()
        {
            if (playerMovement != null)
            {
                playerMovement.movementLocked = false;
            }

            gameObject.SetActive(false);
        }
    }