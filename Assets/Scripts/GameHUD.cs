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

    public GameObject PausePanel;
    public TextMeshProUGUI CurrentScoreNumberText;

    public GameObject RestartPanel;
    public TextMeshProUGUI ScoreNumberText;

    public GameObject UpgradePanel;

    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI NextLevelText;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Time.timeScale = 1f;
        HealthBar.fillAmount = 1f;
        ArmorBar.fillAmount = 0f;
        XpBar.fillAmount = 0f;
        RestartPanel.SetActive(false);
        PausePanel.SetActive(false);
    }

    void Update()
    {
        
    }

    public void PauseGame(bool upgrade)
    {
        if (upgrade) {
            UpgradePanel.SetActive(true);
            IgShopHandler.Instance.SetTwoRandomUpgradesActive();
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
            IgShopHandler.Instance.SetAllUpgradesInactive();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        RestartPanel.SetActive(false);
    }

    public void ShowLosePanel()
    {
        RestartPanel.SetActive(true);
        //Destroy(Player.Instance.gameObject, 1f);
        Player.Instance.gameObject.SetActive(false);
        Time.timeScale = 0f;
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
        menuSounds?.Play();
    }

    public void ResumeButtonClicked() //Eraldi selleks, sest kui nupuga pannakse resume, siis boolean ei muutu, kuid on vaja et ta muutuks
    {
        Player.Instance.gamePaused = !Player.Instance.gamePaused;
    }
}
