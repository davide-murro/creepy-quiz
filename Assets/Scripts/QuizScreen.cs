using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizScreen : MonoBehaviour
{
    // QUESTIONS
    [Header("Questions")]
    [Tooltip("Question text box")]
    [SerializeField]
    TextMeshProUGUI questionTextbox;
    [Tooltip("Scriptable object questions")]
    [SerializeField]
    List<QuestionSO> questions;
    QuestionSO currentQuestion;
    [Tooltip("Amount of questions that the engine will pick for a single game")]
    [SerializeField]
    int questionsPerGame = 10;

    // ANSWERS
    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;

    // BUTTON SPRITES
    [Header("Button Sprites")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite selectedAnswerSprite;

    // TIMER
    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    // SCORE
    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreTextbox;
    Score score;

    // PROGRESS BAR
    [Header("Progress Bar")]
    [SerializeField] Slider progressBarSlider;
    ProgressBar progressBar;

    // if the game is complited
    [NonSerialized] public bool isCompleted;

    // if has answered the current question
    bool hasAnswered = false;

    // loading before all questions
    bool isLoadingQuestions = true;

    // Awake will be run just before Start()
    void Awake()
    {
        // get objects
        timer = FindFirstObjectByType<Timer>();
        score = FindFirstObjectByType<Score>();
        progressBar = FindFirstObjectByType<ProgressBar>();
        // get random tot questions
        questions = questions.OrderBy(x => UnityEngine.Random.value).Take(questionsPerGame).ToList();
        // set progress bar
        progressBar.minValue = 0f;
        progressBar.maxValue = questions.Count;
    }

    // Update is called once per frame
    void Update()
    {
        // update timer image
        timerImage.fillAmount = timer.timerFillAmount;
        // set score
        scoreTextbox.text = $"Scare: {score.GetScore()}%";
        // set progressbar
        progressBarSlider.value = progressBar.GetValue();
        progressBarSlider.minValue = progressBar.minValue;
        progressBarSlider.maxValue = progressBar.maxValue;

        // time is finished
        if (timer.loadNextQuestion)
        {
            // finish loading
            isLoadingQuestions = false;
            // new question
            hasAnswered = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!isLoadingQuestions && !hasAnswered && !timer.isAnsweringQuestion)
        {
            // timer expired
            hasAnswered = true;
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
        questionTextbox.text = currentQuestion.GetQuestionText();

        // set buttons text if exists
        string[] answerList = currentQuestion.GetAnswerList();
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonTextbox = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (i < answerList.Length && answerList[i] != null)
            {
                answerButtons[i].SetActive(true);
                buttonTextbox.text = answerList[i];
            }
            else
            {
                answerButtons[i].SetActive(false);
            }
        }
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
            questionTextbox.text = "Correct!";
            score.IncrementCorrectAnswers();
        }
        else
        {
            //Debug.Log("Wrong answer! :(");
            string correctAnswer = currentQuestion.GetAnswerList()[correctAnswerIndex];
            questionTextbox.text = "Sorry, the correct answer was: \n" + correctAnswer;
        }

        // Increment question seen
        score.IncrementQuestionsSeen();
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
            GetQuestion();
            // display question
            DisplayQuestion();
            // progress bar
            progressBar.IncrementProgress();
        }
        else
        {
            isCompleted = true;
        }
    }

    // get random question and delete it from the list
    void GetQuestion()
    {
        currentQuestion = questions[0];

        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }
}

