using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionsSeen = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // get correct answers
    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }
    // get questions seen
    public int GetQuestionsSeen()
    {
        return questionsSeen;
    }

    // increment correct answers
    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }
    // increment questions seen
    public void IncrementQuestionsSeen()
    {
        questionsSeen++;
    }

    // calculate and retrieve the score
    public int GetScore()
    {
        int score = 0;
        if (questionsSeen > 0)
        {
            score = Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
        }
        return score;
    }
}
