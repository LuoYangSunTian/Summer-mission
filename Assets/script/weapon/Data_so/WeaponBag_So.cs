using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//创建scriptobject文件
[CreateAssetMenu(fileName = "WeaponBag_So", menuName = "Weapon/WeaponBag_So")]
public class WeaponBag_So : ScriptableObject
{
    public List<InventoryWeapon> weaponList;
}