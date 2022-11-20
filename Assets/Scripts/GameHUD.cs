using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    public static GameHUD Instance;
    public Image HealthBar;
    public AudioClipGroup menuSounds;
    public AudioClipGroup fightSound;
    public GameObject RestartPanel;
    public TextMeshProUGUI ScoreNumberText;
    private AudioSource audioSource;
    

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        HealthBar.fillAmount = 1f;
        RestartPanel.SetActive(false);
        fightSound?.Play();
        audioSource = GetComponent<AudioSource>();
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
        //fightSound?.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        RestartPanel.SetActive(false);
    }

    public void ShowLosePanel()
    {
        audioSource.Play();
        RestartPanel.SetActive(true);
        Player.Instance.gameObject.SetActive(false);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Click()
    {
        menuSounds.PlayAtIndex(1);
    }
}
