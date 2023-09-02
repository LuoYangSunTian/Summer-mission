using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder3 : MonoBehaviour, Recycleable
{
    public static string prefabWays = "Prefabs/Thunder3";



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DestoryGameObject()
    {
        PoolManager.Recycle<Thunder3>(this, prefabWays);
    }
    public void AfterGet()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
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
