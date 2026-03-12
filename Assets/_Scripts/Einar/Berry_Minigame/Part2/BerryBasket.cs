using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.SceneManagement;
using Xasu.HighLevel;

public class BerryBasket : MonoBehaviour
{
    [SerializeField] private string targetSceneName;
    [SerializeField] float counter;
    [SerializeField] float maxCollected;

    [SerializeField] private string berryKey;
    [SerializeField] private string requiredKey;
    [SerializeField] private string finishedBerryKey;

    [SerializeField] GameObject canvas;
    [SerializeField] GameObject bearClaw;
    [SerializeField] AudioClip berryCollectSound;


    private void Start()
    {
        CompletableTracker.Instance.Initialized(SceneManager.GetActiveScene().name, CompletableTracker.CompletableType.Level);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object has the tag "Poisonous"
        if (other.CompareTag("Poison"))
        {
            // Reset the scene
            bearClaw.SetActive(false);
            Cursor.visible = true;
            canvas.SetActive(true);
            SceneController.Instance.FreezeTime();
        }

        else if (other.CompareTag("Berry"))
        {
            counter++;
            SoundEffectManager.Instance.PlaySoundFXClip(berryCollectSound, transform, 1f);

            Destroy(other.gameObject);
            if (counter == maxCollected)
            {
                PlayerPrefs.DeleteKey(berryKey);
                PlayerPrefs.SetString(requiredKey, "true");
                PlayerPrefs.SetString(finishedBerryKey, "true");
                PlayerPrefs.Save();

                Cursor.visible = true;
                CompletableTracker.Instance.Completed(SceneManager.GetActiveScene().name, CompletableTracker.CompletableType.Level).WithSuccess(true);
                SceneController.Instance.LoadScene(targetSceneName);
            }
        }
    }

    public void GiveUpBerryGame()
    {
        Cursor.visible = true;
        PlayerPrefs.DeleteKey(berryKey);
        PlayerPrefs.SetString(requiredKey, "true");
        PlayerPrefs.SetString(finishedBerryKey, "true");
        PlayerPrefs.Save();

        SceneController.Instance.ResumeTime();
        SceneController.Instance.LoadScene(targetSceneName);
        CompletableTracker.Instance.Completed("GiveUp", CompletableTracker.CompletableType.Race).WithContextExtension("https://berry", true);
        CompletableTracker.Instance.Completed(SceneManager.GetActiveScene().name, CompletableTracker.CompletableType.Level).WithSuccess(false);
    }

    public void BerryTouchfloor()
    {
        bearClaw.SetActive(false);
        Cursor.visible = true;
        canvas.SetActive(true);
        SceneController.Instance.FreezeTime();
    }
}
