using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;

    ScoreKeeper scoreKeeper;

    // Awake will be run just before Start()
    void Awake()
    {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }

    // show the final score in the game over canvas
    public void ShowFinalScore()
    {
        finalScoreText.text = $"Congrats!\nYou scored: {scoreKeeper.GetScore()}%";
    }
}
