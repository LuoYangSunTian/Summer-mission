using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameobject.Weapon;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>
{
    public WeaponDataList_So weapons;
    private bool isFirst = true;
    public int money;

    private void Start()
    {
        EventHandler.CallUpdateMoney(money);
    }



    /// <summary>
    /// 购买物品
    /// </summary>
    public void BuyItem(int itemPrice, WeaponDetails details, Button button)
    {
        if (money < itemPrice)
        {
            //购买不起物品
        }
        else
        {
            //可以购买物品
            WeaponManager.Instance.AddWeapon(details.weaponId, 1, details.weaponName);
            money -= itemPrice;
            EventHandler.CallUpdateMoney(money);
            button.enabled = false;
        }

    }


    public void UpdateShop()
    {
        if (isFirst)
        {
            EventHandler.CallUpdateShopUI(weapons);
            isFirst = false;
        }
        else
        {
            EventHandler.CallOpenShop();
        }
    }
}
