using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameobject.Weapon;

public class Shield : MonoBehaviour
{

    private Camera mainCamera => GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    private player gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    private float startTime;
    [SerializeField] private float intervalTime;

    private void OnEnable()
    {
        startTime = -10f;
    }

    void Update()
    {
        if (gamePlayer.currentMp < 5f)
        {
            WeaponManager.Instance.CancelWeapon(5001);
        }
        transform.localPosition = Vector3.zero;
        if (Time.time >= startTime + intervalTime)
        {
            gamePlayer.currentMp -= 5f;
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
