using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToAnswerQuestion = 60f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;

    public bool isAnsweringQuestion = false;
    public float timerFillAmount;
    public bool loadNextQuestion;

    float timerValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    // update timer
    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

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
    }


    // cancel / reset timer
    public void CancelTimer()
    {
        timerValue = 0f;
    } 
}
