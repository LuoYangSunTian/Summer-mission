using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameobject.Weapon
{
    public class WeaponManager : Singleton<WeaponManager>
    {
        [Header("武器数据")]
        public WeaponDataList_So weaponDataList_So;

        public WeaponIndex[] weaponGameObjects;

        [Header("背包数据")]
        public WeaponBag_So weaponBag;

        [Header("组件")]

        [SerializeField] private Transform player;
        [SerializeField] private Sprite distance;
        [SerializeField] private Sprite close;
        private player gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
        // Start is called before the first frame update
        void Start()
        {
            //更新武器栏的UI
            EventHandler.CallUpdateInventoryWeapon(weaponBag.weaponList);
        }

        // Update is called once per frame
        void Update()
        {
            if (gamePlayer.currentHp <= 0)
            {
                foreach (var weapon in weaponGameObjects)
                {
                    if (weapon.gameObject.activeSelf)
                        weapon.gameObject.SetActive(false);
                }
            }
        }
        //根据ID查找对应的WeaponDetails
        public WeaponDetails GetWeaponDetails(int ID)
        {
            return weaponDataList_So.weaponDetailsList.Find(i => i.weaponId == ID);
        }

        //拾取武器





        /*        //通过ID查找对应武器在武器栏中的序号
                private int GetWeaponIndexInBag(int ID)
                {
                    for (int i = 0; i < weaponBag.weaponList.Count; i++)
                    {
                        if (weaponBag.weaponList[i].weaponId == ID)
                            return i;
                    }
                    return -1;
                }*/

        //通过ID是否为0判断该武器栏是否有空位
        private bool CheckBagCapacity()
        {
            for (int i = 0; i < weaponBag.weaponList.Count; i++)
            {
                if (weaponBag.weaponList[i].weaponId == 0)
                    return true;
            }
            return false;
        }

        //根据武器栏的序号添加武器
        public bool AddWeapon(int ID, int amount, WeaponName weaponName)
        {
            if (CheckBagCapacity())
            {
                var weapon = new InventoryWeapon { weaponId = ID, weaponAmount = amount };
                for (int i = 0; i < weaponBag.weaponList.Count; i++)
                {
                    if (weaponBag.weaponList[i].weaponId == 0 && weaponBag.weaponList[i].weaponName == weaponName)
                    {
                        weaponBag.weaponList[i] = weapon;
                        EventHandler.CallUpdateInventoryWeapon(weaponBag.weaponList);
                        break;
                    }
                }
                return true;
            }
            else
                return false;

        }

        //生成武器到角色手上
        public void CreatWeapon(int ID)
        {
            foreach (var weapon in weaponGameObjects)
            {
                if (weapon.index == ID)
                {
                    weapon.gameObject.SetActive(true);
                    switch (weapon.weaponType)
                    {
                        case WeaponType.Distance:
                            EventHandler.CallSwitchMouseImageEvent(distance);
                            break;
                        case WeaponType.Close:
                            EventHandler.CallSwitchMouseImageEvent(close);
                            break;
                    }
                }
                else
                    weapon.gameObject.SetActive(false);
            }
        }

        //卸下武器
        public void CancelWeapon(int ID)
        {
            foreach (var weapon in weaponGameObjects)
            {
                if (weapon.index == ID)
                {
                    weapon.gameObject.SetActive(false);
                }
            }
        }
    }
}
