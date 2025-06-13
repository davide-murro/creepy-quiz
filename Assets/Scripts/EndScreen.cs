using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    // final textbox object
    [SerializeField] TextMeshProUGUI finalScoreTextbox;
    // score object
    Score score;

    // Awake will be run just before Start()
    void Awake()
    {
        score = FindFirstObjectByType<Score>();
    }

    // show the final score in the game over canvas
    public void ShowFinalScore()
    {
        finalScoreTextbox.text = $"Congrats!\nYou scared: {score.GetScore()}%";
    }
}
