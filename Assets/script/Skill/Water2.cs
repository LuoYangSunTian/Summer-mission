using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water2 : MonoBehaviour, Recycleable
{
    [Header("参数")]
    private Vector2 rec;
    public static string prefabWays = "Prefabs/Water2";
    [SerializeField] private float speed;


    private Rigidbody2D rigi => GetComponent<Rigidbody2D>();



    void Update()
    {
        MoveMent();
    }
    public void MoveMent()
    {
        rigi.MovePosition(rigi.position + rec * speed * Time.deltaTime);
    }

    public void DestroyGameObject()
    {
        PoolManager.Recycle<Water2>(this, prefabWays);
    }
    public void AfterGet()
    {
        Vector3 mousePosX = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosX.y = transform.position.y;
        mousePosX.z = 0f;
        rec = (mousePosX - transform.position).normalized;
        if (mousePosX.x - transform.position.x > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
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
