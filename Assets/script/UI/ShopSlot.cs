using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShopSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private Image icon;
    [Header("点击")]
    private int clickCount = 0;
    [SerializeField] private float clickInterval;
    [SerializeField] private Button button;
    private WeaponDetails weaponDetails;

    private void Start()
    {
        button.onClick.AddListener(CheckClick);
    }

    public void CheckClick()
    {
        clickCount++;
        if (clickCount == 1)
        {
            //延迟判断是否是双击
            StartCoroutine(CheckDoubleClick());
        }
        else if (clickCount == 2)
        {
            //执行双击的操作
            ShopManager.Instance.BuyItem(weaponDetails.prince, weaponDetails, button);
        }
    }

    private IEnumerator CheckDoubleClick()
    {
        yield return new WaitForSeconds(clickInterval);
        if (clickCount == 1)
        {
            clickCount = 0;
        }
    }
    public void UpdateSlot(WeaponDetails weapon, int money)
    {
        weaponDetails = weapon;
        price.text = weaponDetails.prince.ToString();
        if (money < weaponDetails.prince)
        {
            price.color = Color.red;
        }
        else
        {
            price.color = Color.yellow;
        }
        icon.sprite = weaponDetails.weaponImage;
    }

    public void UpdateCurrentSlot(int money)
    {
        price.text = weaponDetails.prince.ToString();
        if (money < weaponDetails.prince)
        {
            price.color = Color.red;
        }
        else
        {
            price.color = Color.yellow;
        }
        icon.sprite = weaponDetails.weaponImage;
    }
}
