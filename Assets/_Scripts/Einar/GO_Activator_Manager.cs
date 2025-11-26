using UnityEngine;
using Xasu.HighLevel;

public class GO_Activator_Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjectsToActivate;
    [SerializeField] private float delayInSeconds = 10f;

    private void Start()
    {
        if (gameObjectsToActivate != null && gameObjectsToActivate.Length > 0)
        {
            foreach (var obj in gameObjectsToActivate)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
            Invoke(nameof(ActivateObjects), delayInSeconds);
        }
        else
        {
            Debug.LogWarning("No GameObjects assigned");
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
}
