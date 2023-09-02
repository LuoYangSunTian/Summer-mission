using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//创建scriptobject文件
[CreateAssetMenu(fileName = "InventoryBag_So", menuName = "Inventory/InventoryBag_So")]
public class InventoryBag_So : ScriptableObject
{
    public List<InventoryItem> itemList;
}
