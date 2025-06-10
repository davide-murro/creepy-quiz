using UnityEngine;

[CreateAssetMenu(menuName = "Question", fileName = "New Question SO")]
public class QuestionSO : ScriptableObject
{   
    // the question
    [TextArea(2, 6)] [SerializeField] string questionText = "Enter new question here";
    // possible answers
    [SerializeField] string[] answerList = new string[4];
    // correct answer to the question
    [SerializeField] int correctAnswerIndex;                            

    // get the question
    public string GetQuestionText()
    {
        return questionText;
    }

    // get the list of the answers
    public string[] GetAnswerList()
    {
        return answerList;
    }

    // get the index of the correct answer of the answer list
    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }
}
