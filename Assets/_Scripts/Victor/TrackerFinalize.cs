using UnityEngine;
using Xasu;
using Xasu.HighLevel;

public class TrackerFinalize : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        CompletableTracker.Instance.Completed("ShadowsOfTheForest", CompletableTracker.CompletableType.Game);
        await XasuTracker.Instance.Finalize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
