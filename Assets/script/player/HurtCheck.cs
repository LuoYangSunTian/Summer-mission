using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtCheck : MonoBehaviour
{
    [SerializeField] public GameObject HurtRed;
    [SerializeField] private Water1 water1;
    public float iceStartTime;
    private float iceDurationTime = 1.5f;
    private player gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<player>();

    private void Update()
    {
        if (gamePlayer.isIce)
        {
            if (Time.time > iceStartTime + iceDurationTime)
            {
                gamePlayer.iceDisplay.SetActive(false);
                gamePlayer.speed /= 0.6f;
                gamePlayer.isIce = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyAttack"))
        {
            AndioManager.Instance.SwitchPlayerMusic("hurt");
            player gamePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
            EnemyDamage enemyDamage = other.GetComponent<EnemyDamage>();
            if (water1.gameObject.activeSelf)
            {
                water1.defenseAmount -= enemyDamage.enemyDamage;
            }
            else
            {
                HurtDisplay();
                gamePlayer.currentHp -= enemyDamage.enemyDamage;
                EventHandler.CallUpdateStatus(gamePlayer.currentHp, gamePlayer.maxHp, gamePlayer.currentMp, gamePlayer.maxMp);
            }
        }

        if (other.CompareTag("MagicValue"))
        {
            player gamePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
            MagicValue value = other.GetComponent<MagicValue>();
            if (gamePlayer.currentMp <= gamePlayer.maxMp)
            {
                if (gamePlayer.currentMp <= gamePlayer.maxMp - value.magicValue)
                {
                    gamePlayer.currentMp += value.magicValue;
                }
                else
                    gamePlayer.currentMp = gamePlayer.maxMp;
                EventHandler.CallUpdateStatus(gamePlayer.currentHp, gamePlayer.maxHp, gamePlayer.currentMp, gamePlayer.maxMp);
            }
            PoolManager.Recycle<MagicValue>(value, MagicValue.prefabWays);
        }
    }

    private void OnParticleTrigger()
    {
        HurtDisplay();

        gamePlayer.currentHp -= 2f;
        EventHandler.CallUpdateStatus(gamePlayer.currentHp, gamePlayer.maxHp, gamePlayer.currentMp, gamePlayer.maxMp);
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("|||");
    }

    public void HurtDisplay()
    {

        HurtRed.SetActive(true);
        Invoke("CloseRed", 0.4f);
    }

    public void CloseRed()
    {
        HurtRed.SetActive(false);
    }
}
