using UnityEngine;

//made by Mikael
//added fetures by Andreas

public class TriggerController : MonoBehaviour
{
    [SerializeField] private GameObject ObjectToActivate;

    [Header("Left mouse button is interaction")] 
    public bool isNeedOfInteraction = false;

    private bool playerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;

            if (!isNeedOfInteraction)
            {
                ObjectToActivate.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    private void Update()
    {
        if (isNeedOfInteraction && playerInside && Input.GetKeyDown(KeyCode.Mouse0))
        {
            ObjectToActivate.SetActive(true);
        }
    }
}

