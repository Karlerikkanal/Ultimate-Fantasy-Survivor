using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    public static GameHUD Instance;
    public Image HealthBar;
    
    public GameObject RestartPanel;
    

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
}