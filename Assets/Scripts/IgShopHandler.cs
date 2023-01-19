using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgShopHandler : MonoBehaviour
{
    public IgUpgradePresenter ExtraBulletPresenter;
    public IgUpgradePresenter LaserBulletPresenter;
    
    public void BuyExtraBullet()
    {
        ExtraBulletPresenter.ClickPurchase();
    }
}
