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

    public void ClickPurchase()
    {
        purchaseSound?.Play();
        GameHUD.Instance.ResumeGame(true);
    }
}
