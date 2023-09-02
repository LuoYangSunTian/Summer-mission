using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameobject.Weapon;


public class HeroSword : MonoBehaviour
{
    private Transform playerTransform => GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    private Rigidbody2D rigi => GetComponent<Rigidbody2D>();
    private Animator anim => GetComponent<Animator>();
    public List<Transform> enemyTransforms = new List<Transform>();
    private float distance = 1.5f;
    private Transform targetTransform;
    private Vector2 rec;
    [SerializeField] private float speed;

    private float startTime;
    [SerializeField] private float intevalTime;
    [SerializeField] private float consume;
    private player gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    // Start is called before the first frame update
    void Start()
    {
        SwitchTarget();
    }

    private void OnEnable()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePlayer.currentMp < consume)
            WeaponManager.Instance.CancelWeapon(1001);
        if (Time.time > startTime + intevalTime)
        {
            gamePlayer.currentMp -= consume;
            startTime = Time.time;
            EventHandler.CallUpdateStatus(gamePlayer.currentHp, gamePlayer.maxHp, gamePlayer.currentMp, gamePlayer.maxMp);
        }
        SwitchTarget();
        //Debug.Log(enemyTransforms.Count);
        if (enemyTransforms.Count > 0)
        {

            if (targetTransform != null)
            {
                if (Vector2.Distance(transform.position, targetTransform.position) > distance)
                    Move();
                else
                {
                    //攻击
                    Attack();
                }
            }
        }
        else
        {
            MoveToPlayer();
            //跟随角色
        }
    }


    public void SwitchTarget()
    {
        for (int x = 0; x < enemyTransforms.Count; x++)
        {
            if (enemyTransforms.Contains(enemyTransforms[x]))
            {
                if (enemyTransforms[x] == null)
                {
                    enemyTransforms.Remove(enemyTransforms[x]);
                }
                else
                {
                    targetTransform = enemyTransforms[x];
                    break;
                }
            }
        }
    }

    public void MoveToPlayer()
    {
        targetTransform = playerTransform;
        if (Vector2.Distance(playerTransform.position, transform.position) > 2f)
            Move();
        else
            AdjustDirection();
    }
    public void Attack()
    {
        anim.SetTrigger("isAttack");
    }

    public void Move()
    {
        AdjustDirection();
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);
    }

    public void AdjustDirection()
    {
        rec = (targetTransform.position - transform.position).normalized;
        float angle = Mathf.Atan2(rec.y, rec.x) * Mathf.Rad2Deg;//计算角度
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
