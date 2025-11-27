using UnityEngine;
using Xasu.HighLevel;

public class GO_Activator_Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjectsToActivate;
    [SerializeField] private float delayToActivate = 10f;

    [SerializeField] private GameObject[] gameObjectsToDeactivate;
    [SerializeField] private float delayToDeactivate = 15f;

    private void Start()
    {
        if (gameObjectsToActivate != null && gameObjectsToActivate.Length > 0)
        {
            // Deactivate initially if needed
            foreach (var obj in gameObjectsToActivate)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
            // Schedule activation
            Invoke(nameof(ActivateObjects), delayToActivate);
        }
        else
        {
            Debug.LogWarning("No GameObjects assigned for activation");
        }

        if (gameObjectsToDeactivate != null && gameObjectsToDeactivate.Length > 0)
        {
            // Schedule deactivation
            Invoke(nameof(DeactivateObjects), delayToDeactivate);
        }
    }

    private void ActivateObjects()
    {
        foreach (var obj in gameObjectsToActivate)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
        CompletableTracker.Instance.Initialized("GiveUp", CompletableTracker.CompletableType.Race);
    }

    private void DeactivateObjects()
    {
        foreach (var obj in gameObjectsToDeactivate)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}
