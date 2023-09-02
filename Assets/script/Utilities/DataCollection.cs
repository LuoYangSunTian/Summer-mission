
using UnityEngine;
[System.Serializable]//使得创建的类都能被unity识别

public class ItemDetails//物品的详细信息
{
    public int itemID;

    public string itemName;

    public ItemType itemType;

    public Sprite itemIcon;//在背包中的图标

    public Sprite itemOnWorldSprite;//物品在世界地图上的图片

    public string itemDescription;//物品的描述

    public int itemRadius;//表示物品在网格地图中可以使用的范围

    //可以创建bool值表示物品的状态

    public bool canPickUp;
    public int itemPrice;//物品的价值

    [Range(0, 1)]//设置范围
    public float sellPercentage;//出售时价格占购买价格的百分比
}

[System.Serializable]

public class WeaponDetails//武器详情
{
    public Sprite weaponImage;
    public int weaponId;
    public float weaponDamage;
    public float weaponConsume;
    public float weaponCriticalStrike;//暴击
    public string weaponDescribe;
    public WeaponName weaponName;
    public int prince;
}


[System.Serializable]
public class SkillDetails//技能详情
{
    public int skillIndex;
    public SkillType skillType;
    public Sprite SkillIcon;
}


[System.Serializable]//序列化，使得能被unity识别
//创建背包
public struct InventoryItem
{
    public int itemID;

    public int itemAmount;//物品的数目


}

//武器背包
[System.Serializable]
public struct InventoryWeapon
{
    public int weaponId;
    public int weaponAmount;
    public WeaponName weaponName;
}


[System.Serializable]
public class SerializableVector3    //序列化场景坐标，储存物品的坐标
{
    public float x, y, z;

    public SerializableVector3(Vector3 pos)
    {
        this.x = pos.x;
        this.y = pos.y;
        this.z = pos.z;
    }

    public Vector3 ToVector()
    {
        return new Vector3(x, y, z);
    }

    public Vector2Int ToVctor2Int()//返回物品所在格子的位置
    {
        return new Vector2Int((int)x, (int)y);
    }
}

[System.Serializable]
public class SceneItem  //序列化场景中的物品信息
{
    public int itemID;
    public SerializableVector3 position;
}

