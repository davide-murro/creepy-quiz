using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [Tooltip("Scriptable object questions")][SerializeField] List<QuestionSO> questions;
    QuestionSO currentQuestion;
    [Tooltip("question text box")][SerializeField] TextMeshProUGUI questionTextBox;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;

    [Header("Button Sprites")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite selectedAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreTextBox;
    string scoreDefaultText = "Score: 0%";
    ScoreKeeper scoreKeeper;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;

    bool hasAnswered = true;
    public bool isCompleted;

    // Awake will be run just before Start()
    void Awake()
    {
        timer = FindFirstObjectByType<Timer>();
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        scoreTextBox.text = scoreDefaultText;
        progressBar.value = 0f;
        progressBar.minValue = 0f;
        progressBar.maxValue = questions.Count;
    }

    // Update is called once per frame
    void Update()
    {
        // update timer image
        timerImage.fillAmount = timer.timerFillAmount;

        // time is finished
        if (timer.loadNextQuestion)
        {
            // new question
            hasAnswered = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnswered && !timer.isAnsweringQuestion)
        {
            // timer expired
            DisplayAnswer();
            SetButtonsState(false);
        }
    }

    // on answer selected
    public void OnAnswerSelected(int index)
    {
        // set has answered
        hasAnswered = true;
        // display answer
        DisplayAnswer(index);
        // set buttons state
        SetButtonsState(false);
        // cancel timer
        timer.CancelTimer();
    }

    // display question
    void DisplayQuestion()
    {
        // set question text
        questionTextBox.text = currentQuestion.GetQuestionText();

        // set buttons text if exists
        string[] answerList = currentQuestion.GetAnswerList();
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonTextBox = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (i < answerList.Length && answerList[i] != null)
            {
                answerButtons[i].SetActive(true);
                buttonTextBox.text = answerList[i];
            }
            else
            {
                answerButtons[i].SetActive(false);
            }
        }

        // set score
        scoreTextBox.text = $"Score: {scoreKeeper.GetScore()}%";
    }

    // display answer
    void DisplayAnswer(int? index = null)
    {
        // set the color of the selected button
        if (index != null && index >= 0)
        {
            Image buttonImage = answerButtons[(int)index].GetComponent<Image>();
            buttonImage.sprite = selectedAnswerSprite;
        }

        // set the result
        int correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
        if (index != null && index == correctAnswerIndex)
        {
            //Debug.Log("Correct answer! :D");
            questionTextBox.text = "Correct!";
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            //Debug.Log("Wrong answer! :(");
            string correctAnswer = currentQuestion.GetAnswerList()[correctAnswerIndex];
            questionTextBox.text = "Sorry, the correct answer was: \n" + correctAnswer;
        }
    }

    // enable or disable buttons
    void SetButtonsState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    // reset buttons sprite
    void SetDefaultButtonsSprite()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    // get the next question
    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            // set buttons state
            SetButtonsState(true);
            // set buttons sprite
            SetDefaultButtonsSprite();
            // get random question
            GetRandomQuestion();
            // display question
            DisplayQuestion();
            // Increment question seen
            scoreKeeper.IncrementQuestionsSeen();
            // progress bar
            progressBar.value++;
        }
        else if (progressBar.value == progressBar.maxValue)
        {
            isCompleted = true;
        }
    }

    // get random question and delete it from the list
    void GetRandomQuestion()
    {
        int index = UnityEngine.Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }
}

