using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightSword : MonoBehaviour
{
    private player gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    private HurtCheck playerHurtCheck => gamePlayer.GetComponentInChildren<HurtCheck>();
    private Rigidbody2D rigi => GetComponent<Rigidbody2D>();
    private Vector2 rec;
    private float startTime;
    //private bool canMove;
    private bool canAttack;
    [SerializeField] private BossSwordType swordType;
    [SerializeField] private float intervalTime;
    [SerializeField] private float speed;
    [SerializeField] private Transform FirePos;
    [SerializeField] private float radius;
    void Start()
    {
        canAttack = true;
    }
    private void OnEnable()
    {
        startTime = Time.time;
        canAttack = true;
    }

    // Update is called once per frame


    private void Update()
    {
        if (canAttack)
        {
            if (Time.time < startTime + intervalTime)
            {
                AdjustDirectionToPlayer();
            }
            else
            {
                if (Vector2.Distance(transform.position, FirePos.position) >= radius)
                    canAttack = false;
            }
        }
        else
        {
            if (Vector2.Distance(FirePos.position, transform.position) < 0.2f)
            {
                canAttack = true;
                startTime = Time.time;
            }
        }
    }
    private void FixedUpdate()
    {

        if (canAttack)
        {
            if (Time.time >= startTime + intervalTime)
            {
                MoveToPlayer();
            }
        }
        else
        {
            MoveToFirePos();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch (swordType)
            {
                case BossSwordType.Fire:
                    gamePlayer.fireHurt.SetActive(true);
                    break;
                case BossSwordType.Ice:
                    gamePlayer.iceDisplay.SetActive(true);
                    gamePlayer.isIce = true;
                    gamePlayer.speed *= 0.6f;
                    playerHurtCheck.iceStartTime = Time.time;
                    break;

            }
        }
    }
    public void AdjustDirectionToPlayer()
    {

        rec = (gamePlayer.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(rec.y, rec.x) * Mathf.Rad2Deg;//计算角度
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public void AdjustDirectionToFirePos()
    {
        rec = (FirePos.position - transform.position).normalized;
        float angle = Mathf.Atan2(rec.y, rec.x) * Mathf.Rad2Deg;//计算角度
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public void Move()
    {
        rigi.MovePosition(rigi.position + rec * speed * Time.deltaTime);
    }
    public void MoveToPlayer()
    {
        Move();
    }
    public void MoveToFirePos()
    {
        AdjustDirectionToFirePos();
        Move();

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(FirePos.position, radius);
    }
}
