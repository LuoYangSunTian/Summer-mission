using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Gameobject.Inventory//背包系统的命名空间
{


    public class InventoryUI : MonoBehaviour
    {

        [Header("拖拽")]
        public Image dragImage;

        [Header("玩家背包")]
        [SerializeField] private GameObject Bag;//背包

        [SerializeField] private SlotUI[] playerSlots;//物品格

        /// <summary>
        /// 注册事件的方法
        /// </summary>
        private void OnEnable()
        {
            EventHandler.UpdateInventoryUI += OnUpdateInventoryUI;
        }

        private void OnDisable()
        {
            EventHandler.UpdateInventoryUI -= OnUpdateInventoryUI;
        }



        private void Start()
        {
            //给每个格子一个序号
            for (int i = 0; i < playerSlots.Length; i++)
            {
                playerSlots[i].slotIndex = i;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (Bag.activeSelf)
                    Bag.SetActive(false);
                else
                    Bag.SetActive(true);
            }
        }
        private void OnUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)//更新UI事件
        {
            switch (location)
            {
                case InventoryLocation.Player:
                    for (int i = 0; i < playerSlots.Length; i++)
                    {
                        if (list[i].itemAmount > 0)
                        {
                            var item = InventoryManager.Instance.GetItemDetails(list[i].itemID);
                            playerSlots[i].UppdateSlot(item, list[i].itemAmount);
                        }
                        else
                        {
                            playerSlots[i].UpdateEmptySlot();
                        }
                    }
                    break;
            }
        }


        /// <summary>
        /// 更新高亮显示
        /// </summary>
        /// <param name="index"></param>
        public void UpdateSlotHightlight(int index)//打开高亮的图标
        {
            foreach (var slot in playerSlots)
            {
                if (slot.slotIndex == index && slot.isSelected)
                {
                    slot.slotHightlight.gameObject.SetActive(true);
                }
                else
                {
                    slot.isSelected = false;
                    slot.slotHightlight.gameObject.SetActive(false);
                }
            }
        }
    }
}

