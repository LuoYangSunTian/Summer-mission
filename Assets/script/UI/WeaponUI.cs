using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameobject.Weapon
{
    public class WeaponUI : MonoBehaviour
    {

        [SerializeField] private WeaponSlotUI[] weaponSlotUIs;//武器格



        [Header("鼠标图片")]
        [SerializeField] private Sprite normal;
        private void OnEnable()
        {
            EventHandler.UpdateInventoryWeapon += OnUpdateInventoryWeapon;
            EventHandler.CancelWeapon += CancelWeaponDisplay;
        }
        private void OnDisable()
        {
            EventHandler.UpdateInventoryWeapon -= OnUpdateInventoryWeapon;
            EventHandler.CancelWeapon -= CancelWeaponDisplay;
        }


        // Start is called before the first frame update
        void Start()
        {
            //给每个格子一个序号
            for (int i = 0; i < weaponSlotUIs.Length; i++)
                weaponSlotUIs[i].slotIndex = i;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && weaponSlotUIs[0].weaponDetails.weaponId != 0)
            {
                SelectWeapon(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && weaponSlotUIs[1].weaponDetails.weaponId != 0)
            {
                SelectWeapon(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) && weaponSlotUIs[2].weaponDetails.weaponId != 0)
            {
                SelectWeapon(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && weaponSlotUIs[3].weaponDetails.weaponId != 0)
            {
                SelectWeapon(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) && weaponSlotUIs[4].weaponDetails.weaponId != 0)
            {
                SelectWeapon(4);
            }
        }

        //UI武器是否选中
        public void SelectWeapon(int id)
        {
            if (weaponSlotUIs[id].isSelected)
            {
                weaponSlotUIs[id].isSelected = false;
                CancelSlotHighlight(id);
                WeaponManager.Instance.CancelWeapon(weaponSlotUIs[id].weaponDetails.weaponId);
                EventHandler.CallSwitchMouseImageEvent(normal);
                return;
            }
            for (int x = 0; x < weaponSlotUIs.Length; x++)
            {
                if (x == id)
                {
                    weaponSlotUIs[x].isSelected = true;
                }
                else
                {
                    weaponSlotUIs[x].isSelected = false;
                }
            }
            DisplaySlotHightlight(id);
            WeaponManager.Instance.CreatWeapon(weaponSlotUIs[id].weaponDetails.weaponId);
        }


        //UI显示时取消武器
        private void CancelWeaponDisplay()
        {
            if (weaponSlotUIs[0].isSelected)
            {
                weaponSlotUIs[0].isSelected = false;
                CancelSlotHighlight(0);
                WeaponManager.Instance.CancelWeapon(weaponSlotUIs[0].weaponDetails.weaponId);
                EventHandler.CallSwitchMouseImageEvent(normal);
                return;
            }
            else if (weaponSlotUIs[1].isSelected)
            {
                weaponSlotUIs[1].isSelected = false;
                CancelSlotHighlight(1);
                WeaponManager.Instance.CancelWeapon(weaponSlotUIs[1].weaponDetails.weaponId);
                EventHandler.CallSwitchMouseImageEvent(normal);
                return;
            }
        }


        //更新武器的UI显示
        private void OnUpdateInventoryWeapon(List<InventoryWeapon> list)
        {
            Debug.Log("|||");
            for (int i = 0; i < weaponSlotUIs.Length; i++)
            {
                if (list[i].weaponAmount > 0)
                {
                    var weapon = WeaponManager.Instance.GetWeaponDetails(list[i].weaponId);
                    weaponSlotUIs[i].UpdateWeaponSlot(weapon);
                }
                else
                {
                    weaponSlotUIs[i].UpdateEmptySlot();
                }
            }

        }


        //显示高亮
        public void DisplaySlotHightlight(int index)
        {
            foreach (var slot in weaponSlotUIs)
            {

                if (slot.slotIndex == index && slot.isSelected)
                {
                    slot.weaponHighlight.enabled = true;
                }
                else
                    slot.weaponHighlight.enabled = false;
            }
        }

        //取消高亮

        public void CancelSlotHighlight(int index)
        {
            foreach (var slot in weaponSlotUIs)
            {
                if (slot.slotIndex == index && !slot.isSelected)
                    slot.weaponHighlight.enabled = false;
            }
        }
    }
}

