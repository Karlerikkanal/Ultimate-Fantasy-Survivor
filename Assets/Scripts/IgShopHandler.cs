using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgShopHandler : MonoBehaviour
{
    public static IgShopHandler Instance;

    public Dictionary<string, int> upgrades = new Dictionary<string, int>()
    {
        {"Angle", 0},
        {"Damage", 0},
        {"Firerate", 0},
        {"Movement", 0},
        {"ExtraBullet", 0},
        {"Velocity", 0},
        {"ExplosiveRadius", 0}
    };

    //Stats UP buttons
    [Header("Stats UP Modifiers")]
    public IgUpgradePresenter ReduceAngleButton; //
    public IgUpgradePresenter DamageUpButton; //
    public IgUpgradePresenter FirerateUpButton; //
    public IgUpgradePresenter MovementSpeedUpButton;
    public IgUpgradePresenter ExtraBulletButton;
    public IgUpgradePresenter BulletVelocityUpButton; //
    public IgUpgradePresenter ExplosiveRadiusUpButton; //

    //Weapon buttons
    [Header("Change Weapons")]
    public IgUpgradePresenter GreenLaser;
    public IgUpgradePresenter RocketButton;
    public IgUpgradePresenter AsteroidButton;
    public IgUpgradePresenter BombButton;
    public IgUpgradePresenter BlueLaserButton;
    public IgUpgradePresenter SMGButton;


    private void Awake()
    {
        Instance = this;
    }

    public void ReduceAngle()
    {
        ReduceAngleButton.ClickPurchase();
        Shooting.Instance.ReduceShootingAngle();
        upgrades["Angle"] = upgrades["Angle"] + 1;
    }
    
    public void DamageUp()
    {
        DamageUpButton.ClickPurchase();
        Shooting.Instance.IncreaseDamage();
        upgrades["Damage"] = upgrades["Damage"] + 1;
    }

    public void FirerateUp()
    {
        FirerateUpButton.ClickPurchase();
        Shooting.Instance.IncreaseFirerate();
        upgrades["Firerate"] = upgrades["Firerate"] + 1;
    }

    public void MovementSpeedUp()
    {
        MovementSpeedUpButton.ClickPurchase();
        Player.Instance.IncreaseMovementSpeed();
        upgrades["Movement"] = upgrades["Movement"] + 1;
    }

    public void ExtraBullet()
    {
        ExtraBulletButton.ClickPurchase();
        Shooting.Instance.numShots++;
        upgrades["ExtraBullet"] = upgrades["ExtraBullet"] + 1;
    }

    public void BulletVelocityUp()
    {
        BulletVelocityUpButton.ClickPurchase();
        Shooting.Instance.IncreaseBulletVelocity();
        upgrades["Velocity"] = upgrades["Velocity"] + 1;
    }

    public void ExplosiveRadiusUp()
    {
        ExplosiveRadiusUpButton.ClickPurchase();
        Shooting.Instance.IncreaseExplosiveRadius();
        upgrades["ExplosiveRadius"] = upgrades["ExplosiveRadius"] + 1;
    }

    public void ReApplyUpgrades()
    {
        foreach (string key in upgrades.Keys)
        {
            switch (key)
            {
                case "Angle":
                    for (int i = 0; i < upgrades[key]; i++) Shooting.Instance.ReduceShootingAngle();
                    break;
                case "Damage":
                    for (int i = 0; i < upgrades[key]; i++) Shooting.Instance.IncreaseDamage();
                    break;
                case "Firerate":
                    for (int i = 0; i < upgrades[key]; i++) Shooting.Instance.IncreaseFirerate();
                    break;
                case "Velocity":
                    for (int i = 0; i < upgrades[key]; i++) Shooting.Instance.IncreaseBulletVelocity();
                    break;
                case "ExplosiveRadius":
                    for (int i = 0; i < upgrades[key]; i++) Shooting.Instance.IncreaseExplosiveRadius();
                    break;
            }
        }
    }

    public void ChangeGreenLaser()
    {
        GreenLaser.ClickPurchase();
        Shooting.Instance.ChangeBullet(0);
        ReApplyUpgrades();
    }

    public void ChangeRocket()
    {
        RocketButton.ClickPurchase();
        Shooting.Instance.ChangeBullet(1);
        ReApplyUpgrades();
    }

    public void ChangeAsteroid()
    {
        AsteroidButton.ClickPurchase();
        Shooting.Instance.ChangeBullet(2);
        ReApplyUpgrades();
    }

    public void ChangeBomb()
    {
        BombButton.ClickPurchase();
        Shooting.Instance.ChangeBullet(3);
        ReApplyUpgrades();
    }

    public void ChangeBlueLaser()
    {
        BlueLaserButton.ClickPurchase();
        Shooting.Instance.ChangeBullet(4);
        ReApplyUpgrades();
    }

    public void ChangeSMG()
    {
        SMGButton.ClickPurchase();
        Shooting.Instance.ChangeBullet(5);
        ReApplyUpgrades();
    }
}
