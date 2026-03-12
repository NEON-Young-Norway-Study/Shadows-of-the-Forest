using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Xasu;
using Xasu.HighLevel;

public class GameLogger : MonoBehaviour
{
    async void Start()
    {
        await XasuTracker.Instance.Init();
        CompletableTracker.Instance.Initialized("ShadowsOfTheForest", CompletableTracker.CompletableType.Game);
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene current, Scene next)
    {
        AccessibleTracker.Instance.Accessed(next.name, AccessibleTracker.AccessibleType.Area);
    }

    void OnDestroy()
    {
    }
}
