using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml.Schema;
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
    public Image ArmorBar;
    public Image XpBar;
    public AudioClipGroup menuSounds;
    public AudioClipGroup fightSound;
    public GameObject RestartPanel;
    public TextMeshProUGUI ScoreNumberText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI NextLevelText;
    private AudioSource audioSource;
    

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        HealthBar.fillAmount = 1f;
        ArmorBar.fillAmount = 0f;
        XpBar.fillAmount = 0f;
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

    public void SetArmor(float armor)
    {
        ArmorBar.fillAmount = armor;
    }

    public void SetScore(float score)
    {
        ScoreNumberText.text = score.ToString();
    }

    public void SetLevelText(int level)
    {
        LevelText.text = "LEVEL: " + level.ToString();
    }

    public void SetNextLevelText(float xp, float xpneededfornextlevel)
    {
        float total = xpneededfornextlevel - xp;
        NextLevelText.text = "XP NEEDED UNTIL NEXT LEVEL: " + total.ToString();
    }

    public void SetXp(float xp, float xpneededfornextlevel)
    {
        float fill = xp * 100 / xpneededfornextlevel / 100;
        //Debug.Log("Kalkuleeritud fill on: " + fill.ToString());
        XpBar.fillAmount = fill;
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

    public void Click()
    {
        menuSounds.PlayAtIndex(1);
    }
}
