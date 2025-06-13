using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreen: MonoBehaviour
{
    // Play Button click event
    public void OnPlaySelected(int index)
    {
        SceneManager.LoadScene("Quiz");
    }
}
