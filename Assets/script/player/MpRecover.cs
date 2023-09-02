using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MpRecover : MonoBehaviour, Recycleable
{
    private player gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    public static string prefabWays = "Prefabs/MPRecover";
    [SerializeField] private float speed;
    [SerializeField] private float recover;
    private Transform pool => GameObject.FindGameObjectWithTag("PoolManager").GetComponent<Transform>();

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, gamePlayer.transform.position, speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (gamePlayer.currentMp <= gamePlayer.maxMp - recover)
                gamePlayer.currentMp += recover;
            else
                gamePlayer.currentMp = gamePlayer.maxMp;
            EventHandler.CallUpdateStatus(gamePlayer.currentHp, gamePlayer.maxHp, gamePlayer.currentMp, gamePlayer.maxMp);
            PoolManager.Recycle<MpRecover>(this, prefabWays);
        }
    }

    public void AfterGet()
    {

    }

    public void AfterRecycle()
    {
        transform.SetParent(pool);
    }

    public void BeforeGet()
    {

    }

    public void BeforeRecycle()
    {

    }
}
