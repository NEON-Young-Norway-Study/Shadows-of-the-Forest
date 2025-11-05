using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    [SerializeField] GameObject[] questions;
    [SerializeField] GameObject loseScreen;
    [SerializeField] private string targetSceneName;
    [SerializeField] private string schoolKey;
    [SerializeField] private string homeKey;
    [SerializeField] private string finishedSchoolKey;

    int currentQuestion;

    public void CorrectAnswer()
    {
        if (currentQuestion + 1 != questions.Length)
        {
            questions[currentQuestion].SetActive(false);

            currentQuestion++;
            questions[currentQuestion].SetActive(true);
        }
    }

    public void WrongAnswer()
    {
        questions[currentQuestion].SetActive(false);

        loseScreen.SetActive(true);
    }

    public void Retry()
    {
        questions[currentQuestion].SetActive(false);

        currentQuestion = 0;

        questions[currentQuestion].SetActive(true);

        loseScreen.SetActive(false);
    }

    public void exitSchool()
    {
        PlayerPrefs.DeleteKey(schoolKey);
        PlayerPrefs.SetString(finishedSchoolKey, "true");
        PlayerPrefs.SetString(homeKey, "true");
        SceneController.Instance.LoadScene(targetSceneName);
    }

    //public void LoadSceneByName(string Overworld_Prototype)
    //{
    //    SceneManager.LoadScene(Overworld_Prototype);
    //}
}
