using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    public static GameHUD Instance;
    public Image HealthBar;
    
    public GameObject RestartPanel;
    public TextMeshProUGUI ScoreNumberText;
    

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        HealthBar.fillAmount = 1f;
        RestartPanel.SetActive(false);
    }

    void Update()
    {
        
    }

    public void SetHealth(float health)
    {
        HealthBar.fillAmount = health;
    }

    public void SetScore(float score)
    {
        ScoreNumberText.text = score.ToString();
    }

    public void RestartPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        RestartPanel.SetActive(false);
    }

    public void ShowLosePanel()
    {
        RestartPanel.SetActive(true);
        Player.Instance.gameObject.SetActive(false);
        if (PlayerPrefs.HasKey("money"))
        {
            PlayerPrefs.SetFloat("money", float.Parse(ScoreNumberText.text) + PlayerPrefs.GetFloat("money"));
        }
        else
        {
            PlayerPrefs.SetFloat("money", float.Parse(ScoreNumberText.text));
        }
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
