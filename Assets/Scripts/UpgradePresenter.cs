using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class UpgradePresenter : MonoBehaviour
{
    public TextMeshProUGUI CostText;
    public TextMeshProUGUI CountText;
    public Image Icon;
    private Button button;
    public AudioClipGroup purchaseSound;

    public void ClickPurchase()
    {
        purchaseSound?.PlayAtIndex(0);
    }
    public int price;
    public int level;

    public void SetPriceAndLevel(int Level, int Price)
    {
        CostText.text = "Price: " + Price.ToString();
        CountText.text = Level.ToString() + "/5";
        price = Price;
        level = Level;
    }

    public void UpdateData()
    {
        CostText.text = "Price: " + price.ToString();
        CountText.text = level.ToString() + "/5";
    }
}
