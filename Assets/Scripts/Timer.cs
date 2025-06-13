using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToAnswerQuestion = 60f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;

    [NonSerialized] public bool isAnsweringQuestion = false;
    [NonSerialized] public float timerFillAmount = 0f;
    [NonSerialized] public bool loadNextQuestion = false;

    // current timer value
    float timerValue = 0f;


    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    // update timer
    void UpdateTimer()
    {
        if (isAnsweringQuestion)
        {
            // answering...
            if (timerValue > 0f)
            {
                // change the fill amount value of the timer image
                timerFillAmount = timerValue / timeToAnswerQuestion;
            }
            else
            {
                // finish time for answering
                //Debug.Log("Out of time for answering!");
                timerValue = timeToShowCorrectAnswer;
                isAnsweringQuestion = false;
            }
        }
        else
        {
            // showing the correct answer...
            if (timerValue > 0f)
            {
                // change the fill amount value of the timer image
                timerFillAmount = timerValue / timeToShowCorrectAnswer;
            }
            else
            {
                // finish time for showing the correct answer
                //Debug.Log("The time to see the correct answer is over!");
                timerValue = timeToAnswerQuestion;
                loadNextQuestion = true;
                isAnsweringQuestion = true;
            }
        }

        timerValue -= Time.deltaTime;
    }


    // cancel / reset timer
    public void CancelTimer()
    {
        timerValue = 0f;
    } 
}
