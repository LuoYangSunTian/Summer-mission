using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy03 : MonoBehaviour
{
    [Header("组件")]
    [SerializeField] public Transform[] pos;
    [SerializeField] private SpriteRenderer emotionSprite;
    [SerializeField] private Sprite emotionImage;
    private Animator anim => GetComponent<Animator>();
    private Transform player => GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    private Rigidbody2D rigi => GetComponent<Rigidbody2D>();


    [Header("参数")]
    [SerializeField] public float speed;
    [SerializeField] public float Hp;
    [SerializeField] public bool canMove;
    [SerializeField] private float attackRange;
    [SerializeField] private float spyScope;
    [SerializeField] private bool canAttack;
    [SerializeField] private float waitTime;//表情生成等待的时间
    private float attackStartTime = -10f;//攻击开始的时间
    [SerializeField] private float attackIntervalTime;//攻击间隔的时间
    private bool isAttacking;


    // Update is called once per frame
    void Update()
    {
        if (Hp <= 0)
        {
            anim.SetTrigger("isDie");
            return;
        }
        JudgeAttackorMove();
        if (canMove && !isAttacking)
            MoveMent();
        if (canAttack && Time.time > attackStartTime + attackIntervalTime)
        {
            isAttacking = true;
            attackStartTime = Time.time;
            emotionSprite.sprite = emotionImage;
            Invoke("Attack", 0.4f);
            Invoke("SwitchEmotionToNull", waitTime);
        }
    }

    public virtual void JudgeAttackorMove()
    {
        if ((Vector2.Distance(player.position, transform.position)) < spyScope)
        {
            canMove = true;
            canAttack = false;
        }
        if ((Vector2.Distance(player.position, transform.position)) < attackRange)
        {
            canAttack = true;
            canMove = false;
            rigi.velocity = new Vector2(0f, 0f);
        }
    }
    public void SwitchEmotionToNull()
    {
        emotionSprite.sprite = null;
        isAttacking = false;
    }
    public void MoveMent()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        if (player.position.x > transform.position.x)
            transform.localScale = new Vector3(-3, 3, 1);
        else
            transform.localScale = new Vector3(3, 3, 1);
    }

    public virtual void Attack()
    {
        foreach (var attackPos in pos)
        {
            PoolManager.GetItem<Enemy03_Attack>(Enemy03_Attack.prefabWays, attackPos.position);
        }
    }
    public void EnemyDie()
    {
        int index = Random.Range(0, 2);
        if (index == 1)
            PoolManager.GetItem<MpRecover>(MpRecover.prefabWays, transform.position);
        int num = Random.Range(2, 4);
        if (num == 2)
            PoolManager.GetItem<GoldPickUp>(GoldPickUp.prefabWays, transform.position);
        Destroy(gameObject);
    }
}
