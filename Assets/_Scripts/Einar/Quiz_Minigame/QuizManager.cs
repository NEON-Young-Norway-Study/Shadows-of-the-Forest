using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Xasu.HighLevel;

public class QuizManager : MonoBehaviour
{
    [SerializeField] GameObject[] questions;
    [SerializeField] GameObject loseScreen;
    [SerializeField] private string targetSceneName;
    [SerializeField] private string schoolKey;
    [SerializeField] private string homeKey;
    [SerializeField] private string finishedSchoolKey;

    [SerializeField] private AudioClip correctSFX;
    [SerializeField] private AudioClip incorrectSFX;

    int currentQuestion;

    private void Start()
    {
        //CompletableTracker.Instance.Initialized(SceneManager.GetActiveScene().name, CompletableTracker.CompletableType.Level);
    }

    public void CorrectAnswer()
    {
        SoundEffectManager.Instance.PlaySoundFXClip(correctSFX, transform, 0.2f);
        if (currentQuestion + 1 != questions.Length)
        {
            questions[currentQuestion].SetActive(false);

            currentQuestion++;
            questions[currentQuestion].SetActive(true);

            //AlternativeTracker.Instance.Selected(questions[currentQuestion].name, "correct").WithSuccess(true);
            //CompletableTracker.Instance.Progressed(SceneManager.GetActiveScene().name, CompletableTracker.CompletableType.Level, currentQuestion / (float)questions.Length);
        }
    }

    public void WrongAnswer()
    {
        SoundEffectManager.Instance.PlaySoundFXClip(incorrectSFX, transform, 0.2f);
        questions[currentQuestion].SetActive(false);

        loseScreen.SetActive(true);

        //AlternativeTracker.Instance.Selected(questions[currentQuestion].name, "wrong").WithSuccess(false);
    }

    public void Retry()
    {
        questions[currentQuestion].SetActive(false);

        currentQuestion = 0;

        questions[currentQuestion].SetActive(true);

        loseScreen.SetActive(false);
        //CompletableTracker.Instance.Completed(SceneManager.GetActiveScene().name, CompletableTracker.CompletableType.Level).WithSuccess(false);
    }

    public void NextQuestion()
    {
        questions[currentQuestion].SetActive(false);

        currentQuestion++;
        questions[currentQuestion].SetActive(true);

        loseScreen.SetActive(false);
    }

    public void exitSchool()
    {
        PlayerPrefs.DeleteKey(schoolKey);
        PlayerPrefs.SetString(finishedSchoolKey, "true");
        PlayerPrefs.SetString(homeKey, "true");
        SceneController.Instance.LoadScene(targetSceneName);
        //CompletableTracker.Instance.Completed(SceneManager.GetActiveScene().name, CompletableTracker.CompletableType.Level).WithSuccess(true);
    }

    //public void LoadSceneByName(string Overworld_Prototype)
    //{
    //    SceneManager.LoadScene(Overworld_Prototype);
    //}
}
