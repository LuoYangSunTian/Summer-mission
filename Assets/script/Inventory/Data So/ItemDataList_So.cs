using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataList_So", menuName = "Inventory/ItemDataList_So")]//设置菜单的位置
public class ItemDataList_So : ScriptableObject//创建数据库
{
    public List<ItemDetails> itemDetailsList;
}
