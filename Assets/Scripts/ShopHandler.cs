using TMPro;
using UnityEngine;

public class ShopHandler : MonoBehaviour
{
    public float money = 0;
    public UpgradePresenter HealthPresenter;
    public UpgradePresenter SpeedPresenter;
    public UpgradePresenter FireratePresenter;
    public UpgradePresenter MultishotPresenter;
    public TextMeshProUGUI MoneyText;
    void Start()
    {
        
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat("money", money);
        PlayerPrefs.SetInt("healthPrice", HealthPresenter.price);
        PlayerPrefs.SetInt("healthLevel", HealthPresenter.level);
        PlayerPrefs.SetInt("speedPrice", SpeedPresenter.price);
        PlayerPrefs.SetInt("speedLevel", SpeedPresenter.level);
        PlayerPrefs.SetInt("fireratePrice", FireratePresenter.price);
        PlayerPrefs.SetInt("firerateLevel", FireratePresenter.level);
        PlayerPrefs.SetInt("multishotPrice", MultishotPresenter.price);
        PlayerPrefs.SetInt("multishotLevel", MultishotPresenter.level);
    }

    void OnEnable()
    {
        if (!PlayerPrefs.HasKey("healthLevel")) //Kui mingi key on olemas, siis on teised ka
        {
            HealthPresenter.SetPriceAndLevel(0, 1000);
            SpeedPresenter.SetPriceAndLevel(0, 2000);
            FireratePresenter.SetPriceAndLevel(0, 3000);
            MultishotPresenter.SetPriceAndLevel(0, 1000);
        }
        else
        {
            HealthPresenter.SetPriceAndLevel(PlayerPrefs.GetInt("healthLevel"), PlayerPrefs.GetInt("healthPrice"));
            SpeedPresenter.SetPriceAndLevel(PlayerPrefs.GetInt("speedLevel"), PlayerPrefs.GetInt("speedPrice"));
            FireratePresenter.SetPriceAndLevel(PlayerPrefs.GetInt("firerateLevel"), PlayerPrefs.GetInt("fireratePrice"));
            MultishotPresenter.SetPriceAndLevel(PlayerPrefs.GetInt("multishotLevel"), PlayerPrefs.GetInt("multishotPrice"));
        }
        money = PlayerPrefs.GetFloat("money");
        MoneyText.text = "MONEY: " + money.ToString();
    }

    public void BuyHealth()
    {
        if (HealthPresenter.level < 5 && money >= HealthPresenter.price)
        {
            money -= HealthPresenter.price;
            MoneyText.text = "MONEY: " + money.ToString();
            HealthPresenter.level += 1;
            HealthPresenter.price *= 3;
            HealthPresenter.UpdateData();
        }
    }

    public void BuySpeed()
    {
        if (SpeedPresenter.level < 5 && money >= SpeedPresenter.price)
        {
            money -= SpeedPresenter.price;
            MoneyText.text = "MONEY: " + money.ToString();
            SpeedPresenter.level += 1;
            SpeedPresenter.price *= 3;
            SpeedPresenter.UpdateData();
        }
    }

    public void BuyFirerate()
    {
        if (FireratePresenter.level < 5 && money >= FireratePresenter.price)
        {
            money -= FireratePresenter.price;
            MoneyText.text = "MONEY: " + money.ToString();
            FireratePresenter.level += 1;
            FireratePresenter.price *= 3;
            FireratePresenter.UpdateData();
        }
    }

    public void BuyMultishot()
    {
        if (MultishotPresenter.level < 5 && money >= MultishotPresenter.price)
        {
            money -= MultishotPresenter.price;
            MoneyText.text = "MONEY: " + money.ToString();
            MultishotPresenter.level += 1;
            MultishotPresenter.price *= 3;
            MultishotPresenter.UpdateData();
        }
    }

}
