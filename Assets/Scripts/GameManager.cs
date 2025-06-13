using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // quiz screen object
    QuizScreen quizScreen;
    // end screen object
    EndScreen endScreen;

    // Awake will be run just before Start()
    void Awake()
    {
        quizScreen = FindFirstObjectByType<QuizScreen>();
        endScreen = FindFirstObjectByType<EndScreen>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        quizScreen.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (quizScreen.isCompleted)
        {
            quizScreen.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore();
        }
    }

    // restart game
    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Go to home main menu
    public void OnGoHome()
    {
        SceneManager.LoadScene("Home");
    }
}
