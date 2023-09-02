using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Attack1 : MonoBehaviour, Recycleable
{
    public static string prefabWays = "Prefabs/Enemy/Boss2Attack1";
    [SerializeField] private float rotationSpeed;
    [SerializeField] public Transform createPos;
    [SerializeField] public Transform targetPos;
    [SerializeField] private float speed;
    public bool canMove;
    private Vector2 rec;
    private Rigidbody2D rigi => GetComponent<Rigidbody2D>();

    // Update is called once per frame
    void Update()
    {
        Spin();

    }

    private void FixedUpdate()
    {
        if (canMove)
            Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Wall"))
        {
            PoolManager.Recycle<Boss2Attack1>(this, prefabWays);
        }
    }
    private void Spin()//环绕目标旋转
    {
        Vector3 rotationCenter = transform.position;
        Vector3 rotationAxis = Vector3.forward;

        transform.RotateAround(rotationCenter, rotationAxis, rotationSpeed * Time.deltaTime);
    }

    public void Move()
    {
        rec = (targetPos.position - createPos.position).normalized;
        rigi.MovePosition(rigi.position + rec * speed * Time.deltaTime);
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
