using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour
{
    [SerializeField] private float recover;
    [SerializeField] private float intervalTime;
    private float startTime = -10f;
    private player gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    private Camera mainCamera => GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    void Update()
    {

        if (Time.time > intervalTime + startTime )
        {
            if (gamePlayer.currentMp < gamePlayer.maxMp - recover)
                gamePlayer.currentMp += recover;
            else
                gamePlayer.currentHp = gamePlayer.maxHp;
            EventHandler.CallUpdateStatus(gamePlayer.currentHp, gamePlayer.maxHp, gamePlayer.currentMp, gamePlayer.maxMp);
            startTime = Time.time;
        }
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < transform.position.x)
            transform.localScale = new Vector3(-1.5f, 1.5f, 0);
        else
            transform.localScale = new Vector3(1.5f, 1.5f, 0);
    }
}
