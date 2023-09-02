using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataList_So", menuName = "Weapon/WeaponDataList_So")]//设置菜单的位置
public class WeaponDataList_So : ScriptableObject//创建数据库
{
    public List<WeaponDetails> weaponDetailsList;
}