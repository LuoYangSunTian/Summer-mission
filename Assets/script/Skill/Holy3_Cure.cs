using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holy3_Cure : MonoBehaviour, Recycleable
{
    public static string curePrefabWays = "Prefabs/Holy3_cure";
    [SerializeField] private float cureAmount;
    private player gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    private void OnEnable()
    {
        if (gamePlayer.currentHp <= gamePlayer.maxHp - cureAmount)
            gamePlayer.currentHp += cureAmount;
        else
            gamePlayer.currentHp = gamePlayer.maxHp;
        EventHandler.CallUpdateStatus(gamePlayer.currentHp, gamePlayer.maxHp, gamePlayer.currentMp, gamePlayer.maxMp);
    }
    public void AfterGet()
    {
        transform.SetParent(gamePlayer.transform);
    }

    public void AfterRecycle()
    {

    }

    public void BeforeGet()
    {

    }

    public void BeforeRecycle()
    {

    }

    public void DestroyGameObject()
    {
        PoolManager.Recycle<Holy3_Cure>(this, curePrefabWays);
    }
}
