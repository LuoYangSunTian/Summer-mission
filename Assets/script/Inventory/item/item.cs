using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Gameobject.Inventory;//使用inventory的命名空间
public class item : MonoBehaviour
{
    public int itemID;

    private SpriteRenderer spriteRenderer;//通过SpriteRenderer组件为itemBase添加图片

    public ItemDetails itemDetails;//物品的详细信息

    private BoxCollider2D coll;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();//获得itemBase子物体的SpriteRenderer
        coll = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        if (itemID != 0)
        {
            Init(itemID);
        }
    }
    public void Init(int ID)//加载对应的物体
    {
        itemID = ID;
        //为itemdetails赋值
        itemDetails = InventoryManager.Instance.GetItemDetails(itemID);

        if (itemDetails != null)//itemDetails得到的是一个类，需要判断是不是为空
        {
            spriteRenderer.sprite = itemDetails.itemOnWorldSprite != null ? itemDetails.itemOnWorldSprite : itemDetails.itemIcon;

            //修改碰撞体的尺寸
            Vector2 newSize = new Vector2(spriteRenderer.sprite.bounds.size.x, spriteRenderer.sprite.bounds.size.y);//获取图片的尺寸
            coll.size = newSize;
            coll.offset = new Vector2(0, spriteRenderer.sprite.bounds.center.y);//设置偏移量，通过center获取图片的中心
        }
    }
}
