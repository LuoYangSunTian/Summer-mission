using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private player gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    private HurtCheck playerHurtCheck => gamePlayer.GetComponentInChildren<HurtCheck>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = gamePlayer.transform.position;
    }
    private void OnParticleCollision(GameObject other)
    {
        playerHurtCheck.HurtDisplay();
        gamePlayer.currentHp -= 2f;
        EventHandler.CallUpdateStatus(gamePlayer.currentHp, gamePlayer.maxHp, gamePlayer.currentMp, gamePlayer.maxMp);
    }
}
