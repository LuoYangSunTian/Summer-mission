using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameobject.Inventory  //添加一个命名空间,只有使用了该命名空间才能调用其中的内容
{
    //管理数据
    public class InventoryManager : Singleton<InventoryManager>//继承至单例模式
    {
        [Header("物品数据")]
        #region 物品数据
        public ItemDataList_So itemDataList_So;
        #endregion

        [Header("背包数据")]
        #region 背包数据
        public InventoryBag_So playerBag;
        #endregion




        /// <summary>
        /// 根据ID查找对应的ItemDetails
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// 
        private void Start()
        {
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }
        public ItemDetails GetItemDetails(int ID)//根据ID查找对应的ItemDetails
        {
            return itemDataList_So.itemDetailsList.Find(i => i.itemID == ID);//通过itemDataList_So脚本中的itemDetailsList根据ID查找对应的itemdetails
        }


        /// <summary>
        /// 根据ID添加物品
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="amount"></param>
        public void AddItemByID(int itemID, int amount)
        {
            var index = GetItemIndexInBag(itemID);//如果物品存在获取序号

            AddItemIndex(itemID, index, amount);//根据序号添加物品
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }


        /// <summary>
        /// 拾取地上的物品放进背包
        /// </summary>
        /// <param name="item_">对象的信息</param>
        /// <param name="toDestory">是否要销毁</param>
        public void AddItem(item item_, bool toDestory)
        {

            var index = GetItemIndexInBag(item_.itemID);//如果物品存在获取序号

            AddItemIndex(item_.itemID, index, 1);//根据序号添加物品

            if (toDestory)
            {
                Destroy(item_.gameObject);
            }

            //更新UI，通过事件喊话的方式更新UI
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);

        }
        /// <summary>
        /// 检查背包中是否已经有该物品
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private int GetItemIndexInBag(int ID)
        {
            for (int i = 0; i < playerBag.itemList.Count; i++)
            {
                if (playerBag.itemList[i].itemID == ID)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// 检查背包是否有空位
        /// </summary>
        /// <returns></returns>
        private bool CheckBagCapacity()//判断背包是否有空位
        {
            for (int i = 0; i < playerBag.itemList.Count; i++)
            {
                if (playerBag.itemList[i].itemID == 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 在背包序号位置添加物品
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="index"></param>
        /// <param name="amount"></param>
        public void AddItemIndex(int ID, int index, int amount)
        {
            if (index == -1 && CheckBagCapacity())//背包中没有这个物品，需要添加进去，同时有空位
            {
                var item = new InventoryItem { itemID = ID, itemAmount = amount };
                for (int i = 0; i < playerBag.itemList.Count; i++)
                {
                    if (playerBag.itemList[i].itemID == 0)
                    {
                        playerBag.itemList[i] = item;
                        break;
                    }
                }
            }
            else//背包中有该物品直接更改该物品的数目
            {
                int currentAmount = playerBag.itemList[index].itemAmount + amount;
                var item = new InventoryItem { itemID = ID, itemAmount = currentAmount };
                playerBag.itemList[index] = item;
            }
        }

        /// <summary>
        /// player背包的范围内交换物品
        /// </summary>
        /// <param name="fromIndex"></param>
        /// <param name="targetIndex"></param>
        public void SwapItem(int fromIndex, int targetIndex)
        {
            InventoryItem currentItem = playerBag.itemList[fromIndex];
            InventoryItem targetItem = playerBag.itemList[targetIndex];

            if (targetItem.itemID != 0)
            {
                playerBag.itemList[fromIndex] = targetItem;
                playerBag.itemList[targetIndex] = currentItem;
            }
            else
            {
                playerBag.itemList[targetIndex] = currentItem;
                playerBag.itemList[fromIndex] = new InventoryItem { itemID = 0, itemAmount = 0 };
            }

            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }

        //检查背包中是否存在任务需要的物品
        public void CheckTaskItemInBag(string taskItemName)
        {
            foreach (var item in playerBag.itemList)
            {
                if (item.itemAmount > 0)
                {
                    if (InventoryManager.Instance.GetItemDetails(item.itemID).itemName == taskItemName)
                        TaskManager.Instance.UpdateTaskProgress(InventoryManager.Instance.GetItemDetails(item.itemID).itemName, item.itemAmount);
                }
            }
        }
    }
}

