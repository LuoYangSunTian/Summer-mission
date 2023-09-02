using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn2 : MonoBehaviour, Recycleable
{
    public static string prefabWays = "Prefabs/Enemy/Thorn2";
    private float startTime;
    [SerializeField] private float durationTime;

    private void OnEnable()
    {
        startTime = Time.time;
    }
    private void Update()
    {
        if (Time.time > startTime + durationTime)
            PoolManager.Recycle<Thorn2>(this, prefabWays);
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
