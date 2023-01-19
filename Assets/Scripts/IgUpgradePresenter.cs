using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IgUpgradePresenter : MonoBehaviour
{
    public TextMeshProUGUI HeaderText;
    public TextMeshProUGUI DescriptionText;
    public Image Icon;
    private Button button;
    public AudioClipGroup purchaseSound;

    public int level;

    public void ClickPurchase()
    {
        purchaseSound?.PlayAtIndex(0);
        GameHUD.Instance.ResumeGame(true);
    }

    public void setLevel(int Level)
    {
        level = Level;
    }
    
}
