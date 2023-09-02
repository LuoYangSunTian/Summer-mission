using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice3 : MonoBehaviour, Recycleable
{
    public static string prefabWays = "Prefabs/Ice3";
    public static float ice3Wide = 0.37f;
    private float startTime;
    [SerializeField] private float durationTime;
    private Transform pool => GameObject.FindGameObjectWithTag("PoolManager").GetComponent<Transform>();


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= startTime + durationTime)
            PoolManager.Recycle<Ice3>(this, prefabWays);
    }

    public void AfterGet()
    {
        startTime = Time.time;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        transform.SetParent(pool);
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
