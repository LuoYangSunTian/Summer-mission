using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordGas : MonoBehaviour, Recycleable
{
    private player gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    private Rigidbody2D rigi => GetComponent<Rigidbody2D>();
    public static string prefabWays = "Prefabs/Enemy/SwordGas";
    [SerializeField] private float speed;
    private float startTime;
    [SerializeField] private float durationTime;
    private Transform pool => GameObject.FindGameObjectWithTag("PoolManager").GetComponent<Transform>();
    private Vector2 rec;

    private void OnEnable()
    {
        rec = (gamePlayer.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(rec.y, rec.x) * Mathf.Rad2Deg;//计算角度
        transform.eulerAngles = new Vector3(0, 0, angle);
        startTime = Time.time;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        rigi.MovePosition(rigi.position + rec * speed * Time.deltaTime);
        if (Time.time > startTime + durationTime)
        {
            PoolManager.Recycle<SwordGas>(this, prefabWays);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            PoolManager.Recycle<SwordGas>(this, prefabWays);
        }
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
        rec = (gamePlayer.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(rec.y, rec.x) * Mathf.Rad2Deg;//计算角度
        transform.eulerAngles = new Vector3(0, 0, angle);
        startTime = Time.time;
        transform.SetParent(pool);
    }
}
