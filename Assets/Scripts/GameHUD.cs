using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    public static GameHUD Instance;
    public Image HealthBar;
    public Image ArmorBar;
    public Image XpBar;
    public AudioClipGroup menuSounds;
    public AudioClipGroup fightSound;

    public GameObject PausePanel;
    public TextMeshProUGUI CurrentScoreNumberText;

    public GameObject RestartPanel;
    public TextMeshProUGUI ScoreNumberText;

    public GameObject UpgradePanel;

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
        PausePanel.SetActive(false);
        fightSound?.Play();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void PauseGame(bool upgrade)
    {
        if (upgrade) {
            UpgradePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0f;
            CurrentScoreNumberText.text = Player.Instance.Score.ToString();
        }
    }

    public void ResumeGame(bool upgrade)
    {
        if (upgrade) {
            UpgradePanel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            PausePanel.SetActive(false);
            Time.timeScale = 1;
        }
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

    public void ResumeButtonClicked() //Eraldi selleks, sest kui nupuga pannakse resume, siis boolean ei muutu, kuid on vaja et ta muutuks
    {
        Player.Instance.gamePaused = !Player.Instance.gamePaused;
    }
}
