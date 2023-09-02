using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holy1 : MonoBehaviour, Recycleable
{
    public static string prefabWays = "Prefabs/Holy1";
    private Transform gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    private float startTime;
    [SerializeField] private float durationTime;

    private void OnEnable()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= startTime + durationTime)
            PoolManager.Recycle<Holy1>(this, prefabWays);
    }
    public void AfterGet()
    {
        transform.SetParent(gamePlayer);
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

}
