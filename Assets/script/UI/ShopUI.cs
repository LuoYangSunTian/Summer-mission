using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShopUI : MonoBehaviour
{
    public ShopSlot[] shopSlots;

    public WeaponDataList_So weaponList;
    public GameObject shopUI;
    public GameObject title;
    public TextMeshProUGUI moneyNum;

    private void OnEnable()
    {
        EventHandler.UpdateShopUI += OnUpdateShopUI;
        EventHandler.OpenShop += OnOpenShop;
        EventHandler.UpdateMoney += OnUpdateMoney;
    }

    private void OnDisable()
    {
        EventHandler.UpdateShopUI -= OnUpdateShopUI;
        EventHandler.OpenShop -= OnOpenShop;
        EventHandler.UpdateMoney -= OnUpdateMoney;
    }

    private void OnUpdateMoney(int money)
    {
        moneyNum.text = money.ToString();
    }

    private void OnOpenShop()
    {
        shopUI.SetActive(true);
        title.SetActive(true);
        foreach (var slot in shopSlots)
        {
            slot.UpdateCurrentSlot(ShopManager.Instance.money);
        }
    }

    private void OnUpdateShopUI(WeaponDataList_So weapons)
    {
        shopUI.SetActive(true);
        title.SetActive(true);
        weaponList = weapons;
        foreach (var slot in shopSlots)
        {
            int index = Random.Range(0, weaponList.weaponDetailsList.Count);
            slot.UpdateSlot(weaponList.weaponDetailsList[index], ShopManager.Instance.money);
        }
    }



    public void Close()
    {
        shopUI.SetActive(false);
        title.SetActive(false);
    }
}
