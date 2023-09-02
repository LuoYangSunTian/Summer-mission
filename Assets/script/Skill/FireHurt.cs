using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHurt : MonoBehaviour
{
    [SerializeField] private FireHurtTarget target;
    private float startHurtTime;
    private float startTime;
    private player gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    private HurtCheck playerHurtCheck => gamePlayer.GetComponentInChildren<HurtCheck>();
    [SerializeField] private float intervalTime;
    [SerializeField] private float durationTime;

    private void OnEnable()
    {
        startHurtTime = Time.time;
        startTime = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time > startHurtTime + intervalTime)
        {
            gamePlayer.currentHp -= 0.5f;
            playerHurtCheck.HurtDisplay();
            EventHandler.CallUpdateStatus(gamePlayer.currentHp, gamePlayer.maxHp, gamePlayer.currentMp, gamePlayer.maxMp);
            startHurtTime = Time.time;
        }
        if (Time.time > durationTime + startTime)
        {
            gameObject.SetActive(false);
        }
    }
}
