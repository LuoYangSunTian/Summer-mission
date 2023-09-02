using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Gameobject.Inventory
{
    public class TaskRewardSoltUI : MonoBehaviour
    {
        public ItemDetails itemDetails;
        public Image image;
        public TextMeshProUGUI Number;
        private int itemAmount;
        public void UpdateTaskRewardUI(int itemID, int amount)
        {
            itemDetails = InventoryManager.Instance.GetItemDetails(itemID);
            image.sprite = itemDetails.itemIcon;
            itemAmount = amount;
            Number.text = amount.ToString();
            if (itemAmount <= 0)
                gameObject.SetActive(false);
        }
    }

}