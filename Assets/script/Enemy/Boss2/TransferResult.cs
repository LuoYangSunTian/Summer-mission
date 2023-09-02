using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferResult : MonoBehaviour, Recycleable
{
    public static string prefabWays = "Prefabs/Enemy/TransferResult";

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

    public void Destory()
    {
        PoolManager.Recycle<TransferResult>(this, prefabWays);
    }
}
