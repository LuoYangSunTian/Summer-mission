using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Square : MonoBehaviour
{
    private string attackNumway = "Prefabs/EnemyHurtDisplay";
    [SerializeField] private Image Hp;
    [SerializeField] private float maxHp;
    [SerializeField] private float currentHp;
    [SerializeField] private Transform displayPos;
    [SerializeField] private Boos1 boss1;

    // Update is called once per frame
    void Update()
    {
        Hp.fillAmount = currentHp / maxHp;
        if (currentHp <= 0)
        {
            Destroy(gameObject);
            boss1.squareSword.Remove(gameObject);
        }
        boss1.Mode2();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            PlayerDamage damage = other.GetComponent<PlayerDamage>();
            currentHp -= damage.playerDamage;
            DisplayAttackNum displayAttack = PoolManager.GetItem<DisplayAttackNum>(attackNumway, displayPos.position);
            displayAttack.transform.position = displayPos.position;
            displayAttack.attackNum.text = damage.playerDamage.ToString();
        }
        if (other.CompareTag("PlayerDistanceAttack"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            DisplayAttackNum displayAttack = PoolManager.GetItem<DisplayAttackNum>(attackNumway, displayPos.position);
            currentHp -= bullet.attackNum;
            displayAttack.transform.position = displayPos.position;
            displayAttack.attackNum.text = bullet.attackNum.ToString();
        }
        else if (other.CompareTag("PlayerCloseAttack"))
        {
            CloseWeapon closeWeapon = other.GetComponentInParent<CloseWeapon>();
            DisplayAttackNum displayAttack = PoolManager.GetItem<DisplayAttackNum>(attackNumway, displayPos.position);
            currentHp -= closeWeapon.attackNum;
            displayAttack.transform.position = displayPos.position;
            displayAttack.attackNum.text = closeWeapon.attackNum.ToString();
        }
    }
}
