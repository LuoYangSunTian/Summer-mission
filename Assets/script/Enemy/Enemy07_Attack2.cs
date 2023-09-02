using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy07_Attack2 : MonoBehaviour, Recycleable
{

    public Vector2 rec;
    public Vector3 targetPos;
    public static string prefabWays = "Prefabs/Enemy/Enemy07_Attack2";
    private player gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    private HurtCheck playerHurtCheck => gamePlayer.GetComponentInChildren<HurtCheck>();
    private Rigidbody2D rigi => GetComponent<Rigidbody2D>();
    [SerializeField] private float speed;
    private bool canMove;

    private void Start()
    {
        rec = new Vector2(0.71f, 0.71f);
    }
    private void Update()
    {
        if (canMove)
            Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gamePlayer.iceDisplay.SetActive(true);
            gamePlayer.isIce = true;
            gamePlayer.speed *= 0.6f;
            playerHurtCheck.iceStartTime = Time.time;
        }
        if (other.CompareTag("Wall"))
        {
            PoolManager.Recycle<Enemy07_Attack2>(this, prefabWays);
        }
    }

    public void AfterGet()
    {
        rec = new Vector2(0.71f, 0.71f);
        float angle = Mathf.Atan2(rec.y, rec.x) * Mathf.Rad2Deg;//计算角度
        transform.eulerAngles = new Vector3(0, 0, angle);
        canMove = true;
    }

    public void Move()
    {
        rigi.MovePosition(rigi.position + rec * speed * Time.deltaTime);

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
