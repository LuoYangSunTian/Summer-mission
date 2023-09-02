using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn1 : MonoBehaviour, Recycleable
{
    public static string prefabWays = "Prefabs/Enemy/Thorn1";
    private float startTime;
    [SerializeField] private float durationTime;

    private void OnEnable()
    {
        startTime = Time.time;
    }
    private void Update()
    {
        if (Time.time > startTime + durationTime)
            PoolManager.Recycle<Thorn1>(this, prefabWays);
    }
    public void AfterGet()
    {

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
