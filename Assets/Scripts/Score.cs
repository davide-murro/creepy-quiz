using UnityEngine;

public class Score : MonoBehaviour
{
    // correct answers count
    int correctAnswers = 0;
    // question seen count
    int questionsSeen = 0;


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
