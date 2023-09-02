using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy03_Attack : MonoBehaviour, Recycleable
{
    private Transform player => GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    private Rigidbody2D rigi => GetComponent<Rigidbody2D>();
    public static string prefabWays = "Prefabs/Enemy/Enemy03_Attack";
    [SerializeField] private float speed;
    private Vector2 rec;
    private float startAdjust;
    private float startTime;
    [SerializeField] private float intervalAdjust;
    [SerializeField] private float durationTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > startAdjust + intervalAdjust)
        {
            AdjustDirection();
        }
        if (Time.time > startTime + durationTime)
            PoolManager.Recycle<Enemy03_Attack>(this, prefabWays);

    }
    private void FixedUpdate()
    {
        MoveMent();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Player"))
        {
            PoolManager.Recycle<Enemy03_Attack>(this, prefabWays);
        }
    }
    public void AdjustDirection()
    {
        startAdjust = Time.time;
        rec = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(rec.y, rec.x) * Mathf.Rad2Deg;//计算角度
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public void MoveMent()
    {
        rigi.MovePosition(rigi.position + rec * speed * Time.deltaTime);
    }
    public void AfterGet()
    {
        AdjustDirection();
        startTime = Time.time;
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
