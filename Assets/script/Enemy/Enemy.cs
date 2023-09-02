using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("组件")]
    [SerializeField] private BoxCollider2D coll;
    private Animator anim;
    [SerializeField] private Transform player;
    private Rigidbody2D rigi;
    [SerializeField] private SpriteRenderer emotion;
    [SerializeField] private Sprite warning1;
    /*    [SerializeField] private Sprite warning2;
        [SerializeField] private Sprite warning3;*/
    public Transform pos;
    public float radius;



    [Header("参数")]
    [SerializeField] private bool isSlime;
    [SerializeField] public float Speed;
    [SerializeField] public float Hp;
    [SerializeField] public bool canMove;
    [SerializeField] private float attackRange;
    [SerializeField] private float spyScope;
    [SerializeField] private bool canAttack;
    [SerializeField] private bool prepareAttack = true;
    [SerializeField] public bool prepareMove = true;
    [SerializeField] private float waitTime;//表情生成等待的时间
    private float attackStartTime = -10f;//攻击开始的时间
    [SerializeField] private float attackIntervalTime;//攻击间隔的时间
    void Start()
    {
        emotion.sprite = null;
        rigi = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Hp <= 0f)
        {
            prepareAttack = false;
            prepareMove = false;
            canAttack = false;
            canMove = false;
            coll.enabled = false;

            anim.SetBool("isDie", true);

        }
        if (player.position.x < transform.position.x)
            transform.localScale = new Vector3(-4f, 4f, 0f);
        else
            transform.localScale = new Vector3(4f, 4f, 0f);
        Attack();
        if (canMove)
            EnemyMove();
        if (canAttack && Time.time > attackStartTime + attackIntervalTime)
        {
            SwitchToAttack();
            canAttack = false;
            prepareAttack = false;
            anim.SetBool("isRun", false);
            attackStartTime = Time.time;
        }
    }

    public virtual void Attack()
    {
        if ((Vector2.Distance(player.position, transform.position)) < spyScope)
            if (prepareMove)
                canMove = true;
        if ((Vector2.Distance(player.position, transform.position)) < attackRange)
        {
            if (prepareAttack)
            {
                canAttack = true;
                canMove = false;
                prepareMove = false;
                rigi.velocity = new Vector2(0f, 0f);
            }
        }
    }

    public void SwitchEmotionToWarning_1()
    {
        emotion.sprite = warning1;

    }
    /*    public void SwitchEmotionToWarning_2()
        {
            emotion.sprite = warning2;
        }
        public void SwitchEmotionToWarning_3()
        {
            emotion.sprite = warning3;
        }*/
    public void SwithEmotionToNone()
    {
        emotion.sprite = null;
        prepareAttack = true;
        prepareMove = true;
    }
    public virtual void SwitchToAttack()
    {
        attackStartTime = Time.time;
        anim.SetTrigger("isAttack");
        SwitchEmotionToWarning_1();
        Invoke("SwithEmotionToNone", waitTime);
    }
    public virtual void EnemyMove()
    {
        anim.SetBool("isRun", true);

        transform.position = Vector2.MoveTowards(transform.position, player.position, Speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pos.position, radius);
    }
    public void EnemyDie()
    {
        int index = Random.Range(0, 2);
        if (index == 1)
            PoolManager.GetItem<MpRecover>(MpRecover.prefabWays, transform.position);
        int num = Random.Range(2, 4);
        if (num == 2)
            PoolManager.GetItem<GoldPickUp>(GoldPickUp.prefabWays, transform.position);
        if (isSlime)
            TaskManager.Instance.UpdateTaskProgress("史莱姆", 1);
        Destroy(gameObject);
    }
}
