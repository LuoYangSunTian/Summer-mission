using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss2 : MonoBehaviour
{
    private Transform playerPos => GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    private Animator anim => GetComponent<Animator>();
    private string attackNumway = "Prefabs/EnemyHurtDisplay";
    [SerializeField] private Transform displayPos;
    [SerializeField] private float currentHp;
    [SerializeField] private float maxHp;
    [SerializeField] private Image Hp;
    [Header("左边钳子攻击")]
    [SerializeField] private Transform createPos1;
    [SerializeField] private Transform[] targetPos1;
    [Header("右边钳子攻击")]
    [SerializeField] private Transform createPos2;
    [SerializeField] private Transform[] targetPos2;
    [Header("刺")]
    [SerializeField] private Transform[] poses;
    [Header("移动")]
    private bool canMove;
    [Header("冰刺")]
    [SerializeField] private Transform iceThornCreatePos;

    [Header("治疗")]
    private bool canCure = true;
    [SerializeField] GameObject body;
    private float startCureTime = 0f;
    [SerializeField] private float dutationCureTime;
    private float startAddHp = 999f;
    [SerializeField] private float intervalAddHp;
    private float startSummomTime = 999f;//召唤小弟
    [SerializeField] private float intervalSummomTime;
    [SerializeField] private Transform[] summomPos;
    [SerializeField] private GameObject littleCrab;

    private Rigidbody2D rigi => GetComponent<Rigidbody2D>();
    [SerializeField] private float speed;
    [SerializeField] private bool canAttack = true;
    private float attackStartTime;
    [SerializeField] private float attackIntervalTime;
    [SerializeField] private GameObject gameover;

    // Start is called before the first frame update
    private void OnEnable()
    {
        canAttack = true;
        attackStartTime = -10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHp <= 0f)
        {
            Destroy(gameObject);
            Time.timeScale = 0f;
            gameover.SetActive(true);
        }
        Hp.fillAmount = currentHp / maxHp;
        if (currentHp <= maxHp / 2 && canCure)
        {

            canAttack = false;
        }
        if (!canAttack)
            CureMode();

        if (canAttack && Time.time >= attackIntervalTime + attackStartTime)
        {
            int index = Random.Range(0, 3);
            switch (index)
            {
                case 0:
                    AttackMode1();
                    break;
                case 1:
                    AttackMode2();
                    break;
                case 2:
                    AttackMode3();
                    break;
            }
            attackStartTime = Time.time;
        }
        if (canMove)
            Move();
    }

    private void FixedUpdate()
    {
        /*AttackMode1();*/
    }


    public void CureMode()
    {
        body.SetActive(false);
        startCureTime += Time.deltaTime;
        Debug.Log(startCureTime);
        if (startCureTime > dutationCureTime)
        {
            body.SetActive(true);
            canAttack = true;
            startCureTime = 0f;
            canCure = false;
        }
        else
        {
            startAddHp += Time.deltaTime;
            if (startAddHp > intervalAddHp)
            {
                currentHp += 5f;
                startAddHp = 0f;
            }
            startSummomTime += Time.deltaTime;
            int index = Random.Range(0, summomPos.Length);
            if (startSummomTime > intervalSummomTime)
            {
                startSummomTime = 0f;
                PoolManager.GetItem<TransferResult>(TransferResult.prefabWays, summomPos[index].position);
                Instantiate(littleCrab, summomPos[index].position, summomPos[index].rotation);
            }
        }
    }



    /// <summary>
    /// 左右横条
    /// </summary>
    public void AttackMode1()
    {
        anim.SetTrigger("isAttack");
        canMove = true;

    }
    /// <summary>
    /// 地刺攻击
    /// </summary>
    public void AttackMode2()
    {
        anim.SetTrigger("isAttack2");
    }

    /// <summary>
    /// 冰刺攻击
    /// </summary>

    public void AttackMode3()
    {
        anim.SetTrigger("isAttack3");

    }


    //生成冰刺
    public void Mode4()
    {
        PoolManager.GetItem<IceThorn>(IceThorn.prefabWays, iceThornCreatePos.position);
    }

    //生成地刺，动画帧
    public void Mode3()
    {
        StartCoroutine(CreateThorn());
    }


    IEnumerator CreateThorn()
    {
        foreach (var pos in poses)
        {
            int index = Random.Range(1, 4);
            switch (index)
            {
                case 1:
                    Thorn1 thorn1 = PoolManager.GetItem<Thorn1>(Thorn1.prefabWays, pos.position);
                    if (pos.position.x < transform.position.x)
                    {
                        thorn1.transform.localScale = new Vector3(-3, 3, 0);
                    }
                    break;
                case 2:
                    Thorn2 thorn2 = PoolManager.GetItem<Thorn2>(Thorn2.prefabWays, pos.position);
                    if (pos.position.x < transform.position.x)
                    {
                        thorn2.transform.localScale = new Vector3(-3, 3, 0);
                    }
                    break;
                case 3:
                    Thorn3 thorn3 = PoolManager.GetItem<Thorn3>(Thorn3.prefabWays, pos.position);
                    if (pos.position.x < transform.position.x)
                    {
                        thorn3.transform.localScale = new Vector3(-3, 3, 0);
                    }
                    break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }


    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
    }

    //左边钳子攻击,动画帧事件
    public void Mode1()
    {
        foreach (var targer in targetPos1)
        {
            Boss2Attack1 attack1 = PoolManager.GetItem<Boss2Attack1>(Boss2Attack1.prefabWays, createPos1.position);
            attack1.targetPos = targer;
            attack1.createPos = createPos1;
            attack1.canMove = true;
        }
    }


    //右边钳子攻击，动画帧事件
    public void Mode2()
    {
        foreach (var targer in targetPos2)
        {
            Boss2Attack1 attack1 = PoolManager.GetItem<Boss2Attack1>(Boss2Attack1.prefabWays, createPos2.position);
            attack1.targetPos = targer;
            attack1.createPos = createPos2;
            attack1.canMove = true;
        }
        canMove = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            PlayerDamage damage = other.GetComponent<PlayerDamage>();
            DisplayAttackNum displayAttack = PoolManager.GetItem<DisplayAttackNum>(attackNumway, displayPos.position);
            currentHp -= damage.playerDamage;
            displayAttack.transform.position = displayPos.position;
            displayAttack.attackNum.text = damage.playerDamage.ToString();
        }
        if (other.CompareTag("PlayerDistanceAttack"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            DisplayAttackNum displayAttack = PoolManager.GetItem<DisplayAttackNum>(attackNumway, displayPos.position);
            currentHp -= bullet.attackNum;
            displayAttack.transform.position = displayPos.position;
            displayAttack.attackNum.text = bullet.attackNum.ToString();
        }
        else if (other.CompareTag("PlayerCloseAttack"))
        {
            CloseWeapon closeWeapon = other.GetComponentInParent<CloseWeapon>();
            DisplayAttackNum displayAttack = PoolManager.GetItem<DisplayAttackNum>(attackNumway, displayPos.position);
            currentHp -= closeWeapon.attackNum;
            displayAttack.transform.position = displayPos.position;
            displayAttack.attackNum.text = closeWeapon.attackNum.ToString();
        }
    }
}
