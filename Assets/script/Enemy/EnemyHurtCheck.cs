using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyHurtCheck : MonoBehaviour
{

    private string attackNumway = "Prefabs/EnemyHurtDisplay";
    [SerializeField] private Transform displayPos;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerDistanceAttack"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            Enemy enemy = GetComponentInParent<Enemy>();
            DisplayAttackNum displayAttack = PoolManager.GetItem<DisplayAttackNum>(attackNumway, displayPos.position);
            enemy.Hp -= bullet.attackNum;
            displayAttack.transform.position = displayPos.position;
            displayAttack.attackNum.text = bullet.attackNum.ToString();
        }
        else if (other.CompareTag("PlayerCloseAttack"))
        {
            CloseWeapon closeWeapon = other.GetComponentInParent<CloseWeapon>();
            DisplayAttackNum displayAttack = PoolManager.GetItem<DisplayAttackNum>(attackNumway, displayPos.position);
            Enemy enemy = GetComponentInParent<Enemy>();
            enemy.Hp -= closeWeapon.attackNum;
            displayAttack.transform.position = displayPos.position;
            displayAttack.attackNum.text = closeWeapon.attackNum.ToString();
        }
        else if (other.CompareTag("PlayerAttack"))
        {
            PlayerDamage damage = other.GetComponent<PlayerDamage>();
            DisplayAttackNum displayAttack = PoolManager.GetItem<DisplayAttackNum>(attackNumway, displayPos.position);
            Enemy enemy = GetComponentInParent<Enemy>();
            enemy.Hp -= damage.playerDamage;
            displayAttack.transform.position = displayPos.position;
            displayAttack.attackNum.text = damage.playerDamage.ToString();
        }
    }

}
