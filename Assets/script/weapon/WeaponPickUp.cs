using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameobject.Weapon
{
    public class WeaponPickUp : MonoBehaviour
    {

        private bool canPickUp = false;
        private WeaponBase weapon;
        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.E) && canPickUp)
            {
                if (WeaponManager.Instance.AddWeapon(weapon.weaponID, 1, weapon.weaponDetails.weaponName))
                {

                    Destroy(weapon.gameObject);
                    canPickUp = false;
                }
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {

            weapon = other.GetComponent<WeaponBase>();
            if (weapon != null)
            {

                canPickUp = true;
                weapon.DisplayWeaponDescribe();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            weapon = other.GetComponent<WeaponBase>();
            if (weapon != null)
            {
                canPickUp = false;
                weapon.CancelWeaponDescribe();
            }
        }
    }
}
