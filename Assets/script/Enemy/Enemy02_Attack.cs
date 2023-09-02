using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02_Attack : MonoBehaviour, Recycleable
{
    private player gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    private Rigidbody2D rigi => GetComponent<Rigidbody2D>();
    public static string prefabWays = "Prefabs/Enemy/Enemy02_Attack";
    [SerializeField] private float speed;

    private Vector2 rec;

    // Update is called once per frame
    void Update()
    {
        rigi.MovePosition(rigi.position + rec * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Player"))
        {
            PoolManager.Recycle<Enemy02_Attack>(this, prefabWays);
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
    }
}
