using UnityEngine;

[CreateAssetMenu(menuName = "Question", fileName = "New Question SO")]
public class QuestionSO : ScriptableObject
{
    /// <summary>
    /// Text of the question
    /// </summary> 
    [SerializeField]
    [TextArea(2, 6)]
    public string questionText = "Enter new question here";
    /// <summary>
    /// List of anwsers related to the question
    /// </summary> 
    [SerializeField] 
    string[] answerList = new string[4];
    /// <summary>
    /// Correct answer index to the question
    /// </summary> 
    [SerializeField] 
    int correctAnswerIndex;

    /// <summary>
    /// Get the text of the question
    /// </summary> 
    public string GetQuestionText()
    {
        return questionText;
    }
    /// <summary>
    /// Get the list of anwsers related to the question
    /// </summary> 
    public string[] GetAnswerList()
    {
        return answerList;
    }
    /// <summary>
    /// Get the correct answer index to the question
    /// </summary> 
    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }
}
