using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreen: MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // on play selected
    public void OnPlaySelected(int index)
    {
        var a = SceneManager.GetSceneByName("Quiz");
        SceneManager.LoadScene("Quiz");
    }
}
