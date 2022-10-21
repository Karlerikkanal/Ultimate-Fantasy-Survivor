using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultPanel : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;

    public void Awake()
    {
        gameObject.SetActive(false);
    }
    public void SetResult(int score)
    {
        gameObject.SetActive(true);
        ScoreText.text = "Score: " + score.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
