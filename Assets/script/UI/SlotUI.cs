using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//使用UI的命名空间
using TMPro;//文字使用的TextMashpro
using UnityEngine.EventSystems;//使用UI事件的 命名空间
using Gameobject.Skill;


namespace Gameobject.Inventory
{
    public class SlotUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler//向物品栏中添加数据，使用点击的事件接口,开始拖拽，拖拽过程，停止拖拽过程
    {
        [Header("组件获取")]

        [SerializeField] private Image slotImage;//显示的图片
        [SerializeField] private TextMeshProUGUI amountText;//数量
        [SerializeField] public Image slotHightlight;//被点击时的图像
        [SerializeField] private Button button;//按钮
        private player gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<player>();

        [Header("格子的类型")]
        public SlotType slotType;

        public bool isSelected;//判断是否被选中

        public int slotIndex;//格子的序号
        //物品信息
        public ItemDetails itemDetails;
        public int itemAmount;


        private InventoryUI inventoryUI => GetComponentInParent<InventoryUI>();//从父级处获得InventoryUI的组件

        [Header("点击")]
        //private int clickCount = 0;
        [SerializeField] private float clickInterval;


        /// <summary>
        /// 更新格子的信息
        /// </summary>
        /// <param name="item"></param>
        /// <param name="amount">物品的数量</param>
        public void UppdateSlot(ItemDetails item, int amount)
        {
            itemDetails = item;
            slotImage.sprite = item.itemIcon;
            itemAmount = amount;
            amountText.text = itemAmount.ToString();
            slotImage.enabled = true;
            button.interactable = true;
        }

        private void Start()
        {
            //button.onClick.AddListener(CheckClick);
            isSelected = false;
            if (itemDetails.itemID == 0)
            {
                UpdateEmptySlot();
            }
        }

        private void Update()
        {
            if (isSelected && Input.GetMouseButtonDown(1))
            {
                if (itemDetails.itemType == ItemType.medicine)
                {
                    switch (itemDetails.itemID)
                    {
                        case 2001:
                            if (gamePlayer.currentMp < gamePlayer.maxMp)
                            {
                                if (gamePlayer.currentMp <= gamePlayer.maxMp - 20f)
                                    gamePlayer.currentMp += 20f;
                                else
                                    gamePlayer.currentMp = gamePlayer.maxMp;
                                EventHandler.CallUpdateStatus(gamePlayer.currentHp, gamePlayer.maxHp, gamePlayer.currentMp, gamePlayer.maxMp);
                                itemAmount--;
                                InventoryManager.Instance.playerBag.itemList[slotIndex] = new InventoryItem { itemID = itemDetails.itemID, itemAmount = itemAmount };
                                amountText.text = itemAmount.ToString();

                                if (itemAmount <= 0)
                                    UpdateEmptySlot();
                            }
                            break;
                        case 1000:
                            if (AweakeningMagic.Instance.isEnter)
                            {
                                AweakeningMagic.Instance.isUse = true;
                                itemAmount--;
                                InventoryManager.Instance.playerBag.itemList[slotIndex] = new InventoryItem { itemID = itemDetails.itemID, itemAmount = itemAmount };
                                amountText.text = itemAmount.ToString();
                            }


                            if (itemAmount <= 0)
                                UpdateEmptySlot();
                            break;
                    }
                }
            }
        }
        /*
                public void CheckClick()
                {
                    if(isSelected)
                    {
                        if (itemDetails.itemType == ItemType.medicine)
                        {
                            switch (itemDetails.itemID)
                            {
                                case 2001:
                                    if (gamePlayer.currentMp < gamePlayer.maxMp)
                                    {
                                        if (gamePlayer.currentMp <= gamePlayer.maxMp - 20f)
                                            gamePlayer.currentMp += 20f;
                                        else
                                            gamePlayer.currentMp = gamePlayer.maxMp;
                                        EventHandler.CallUpdateStatus(gamePlayer.currentHp, gamePlayer.maxHp, gamePlayer.currentMp, gamePlayer.maxMp);
                                        itemAmount--;

                                        amountText.text = itemAmount.ToString();
                                        if (itemAmount <= 0)
                                            UpdateEmptySlot();
                                    }
                                    break;
                            }
                        }
                    }*/
        /*            clickCount++;
                    if (clickCount == 1)
                    {
                        //延迟判断是否是双击
                        StartCoroutine(CheckDoubleClick());
                    }
                    if (clickCount == 2)
                    {
                        //执行双击的操作
                        Debug.Log("|||");


                    }*/
        // }

        /*        private IEnumerator CheckDoubleClick()
                {
                    yield return new WaitForSeconds(clickInterval);
                    if (clickCount == 1)
                    {
                        clickCount = 0;
                    }
                }*/


        /// <summary>
        /// 更新格子为空
        /// </summary>
        public void UpdateEmptySlot()
        {
            if (isSelected)
                isSelected = false;
            slotImage.enabled = false;
            amountText.text = string.Empty;
            itemAmount = 0;
            button.interactable = false;
            inventoryUI.UpdateSlotHightlight(-1);
        }

        public void OnPointerClick(PointerEventData eventData)//点击的事件
        {
            if (itemAmount == 0) return;
            isSelected = !isSelected;
            inventoryUI.UpdateSlotHightlight(slotIndex);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (itemAmount != 0)
            {
                inventoryUI.dragImage.enabled = true;
                inventoryUI.dragImage.sprite = slotImage.sprite;
                inventoryUI.dragImage.SetNativeSize();//将图片设置为本来的大小防止拖拽是图片变形

                isSelected = true;
                inventoryUI.UpdateSlotHightlight(slotIndex);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            inventoryUI.dragImage.transform.position = Input.mousePosition;//拖拽的过程中让图片的位置始终等于鼠标的位置
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            inventoryUI.dragImage.enabled = false;
            //Debug.Log(eventData.pointerCurrentRaycast.gameObject);

            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                if (eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>() == null)
                    return;
                var targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>();
                int targetIndex = targetSlot.slotIndex;

                //在player自身的范围内交换就行
                if (slotType == SlotType.Bag && targetSlot.slotType == SlotType.Bag)
                {
                    InventoryManager.Instance.SwapItem(slotIndex, targetIndex);
                }

                //清空高亮的显示
                inventoryUI.UpdateSlotHightlight(-1);
            }
            //扔到地上
            else
            {
                //鼠标对应的地面的坐标
                var pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));//默认情况下摄像机的z是为正的，需要补上一个负的才能到地面上
                EventHandler.CallInstantiateItemScene(itemDetails.itemID, pos);
                itemAmount--;
                InventoryManager.Instance.AddItemIndex(itemDetails.itemID, slotIndex, -1);
                amountText.text = itemAmount.ToString();
                if (itemAmount <= 0)
                    UpdateEmptySlot();
            }
        }
    }
}

