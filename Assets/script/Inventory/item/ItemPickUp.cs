using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gameobject.Inventory//写在inventory的命名空间中
{
    public class ItemPickUp : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {

            item item_ = other.GetComponent<item>();

            if (item_ != null)
            {

                if (item_.itemDetails.canPickUp)
                {
                    InventoryManager.Instance.AddItem(item_, true);//拾取物品
                    TaskManager.Instance.UpdateTaskProgress(item_.itemDetails.itemName, 1);
                }
            }
        }
    }
}

