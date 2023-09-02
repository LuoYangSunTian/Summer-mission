using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameobject.Weapon
{
    public class WeaponSlotUI : MonoBehaviour
    {
        [Header("组件")]
        [SerializeField] private Image weaponImage;
        [SerializeField] public Image weaponHighlight;
        [SerializeField] private Image symbol;

        [Header("参数")]
        public bool isSelected;//判断是否被选中
        public int slotIndex; //格子序号从0开始
        public WeaponDetails weaponDetails; //武器的详细信息

        private WeaponUI weaponUI => GetComponentInParent<WeaponUI>();

        //更新格子为指定的武器
        public void UpdateWeaponSlot(WeaponDetails weapon)
        {
            weaponDetails = weapon;
            weaponImage.sprite = weapon.weaponImage;
            weaponImage.enabled = true;
            symbol.enabled = false;
            weaponUI.DisplaySlotHightlight(slotIndex);
        }

        //更新格子为空
        public void UpdateEmptySlot()
        {
            symbol.enabled = true;
            if (isSelected)
                isSelected = false;
            weaponImage.enabled = false;
        }
    }
}
