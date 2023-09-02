using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceThorn : MonoBehaviour, Recycleable
{
    public static string prefabWays = "Prefabs/Enemy/IceThorn";
    private float startTime;
    [SerializeField] private float durationTime;
    private float startCreateTime = 0f;
    [SerializeField] private float intervalTime;

    [SerializeField] private float speed;


    private Transform gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


    private void OnEnable()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time > startTime + durationTime)
        {
            PoolManager.Recycle<IceThorn>(this, prefabWays);
        }
        else
        {
            MoveToPlayer();
            startCreateTime += Time.deltaTime;
            if (startCreateTime >= intervalTime)
            {
                PoolManager.GetItem<IceThornClone>(IceThornClone.prefabWays, transform.position);
                startCreateTime = 0f;
            }
        }
    }

    public void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, gamePlayer.position, speed * Time.deltaTime);
    }

    public void BeforeRecycle()
    {

    }

    public void AfterRecycle()
    {

    }

    public void BeforeGet()
    {

    }

    public void AfterGet()
    {

    }
}
