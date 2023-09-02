using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceThornClone : MonoBehaviour, Recycleable
{
    public static string prefabWays = "Prefabs/Enemy/IceThornClone";
    private float startTime;
    [SerializeField] private float durationTime;

    private void Update()
    {
        if (Time.time > startTime + durationTime)
            PoolManager.Recycle<IceThornClone>(this, prefabWays);
    }
    private void OnEnable()
    {
        startTime = Time.time;
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
