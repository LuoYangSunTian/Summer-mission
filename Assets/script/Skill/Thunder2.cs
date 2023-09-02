using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder2 : MonoBehaviour, Recycleable
{
    public Transform parent;
    public static string prefabWays = "Prefabs/Thunder2";
    [SerializeField] public Transform targetPos;
    private Vector3 rec;
    [SerializeField] private float spinDistance;//开始盘旋的距离
    private float startAttackTime;
    [SerializeField] private float attackIntervalTime;//攻击间隔
    [SerializeField] private float speed; //移动速度
    [SerializeField] private Transform creatAttackPos;
    private Camera mainCamera => GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    private Collider2D[] enemy;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask targetMask;
    private float startTime;
    [SerializeField] private float durationTime;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > startTime + durationTime)
            PoolManager.Recycle<Thunder2>(this, prefabWays);
        if (targetPos == null)
        {
            transform.SetParent(parent);
            SelectTarget();
            if (transform.parent != null)
                transform.localPosition = Vector3.zero;
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;
            rec = (mouseWorldPos - transform.position).normalized;
            float angle = Mathf.Atan2(rec.y, rec.x) * Mathf.Rad2Deg;//计算角度
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
        else
        {
            transform.SetParent(null);
            Movement();
            StartAttack();
        }

    }

    public void AddEnemyInList()
    {
        Vector2 point = new Vector2(transform.position.x, transform.position.y);
        enemy = Physics2D.OverlapCircleAll(point, attackRadius, targetMask);
    }

    public void SelectTarget()
    {
        AddEnemyInList();
        for (int x = 0; x < enemy.Length; x++)
        {
            if (enemy[x] != null && enemy[x].gameObject.CompareTag("Enemy"))
            {
                Transform target = enemy[x].GetComponentInParent<Transform>();
                targetPos = target;
                transform.SetParent(null);
                return;
            }
        }

    }

    private void AdjustDirection()//调整方向指向目标
    {
        rec = (targetPos.position - transform.position).normalized;
        float angle = Mathf.Atan2(rec.y, rec.x) * Mathf.Rad2Deg;//计算角度
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void Spin()//环绕目标旋转
    {
        Vector3 rotationCenter = targetPos.position;
        Vector3 rotationAxis = Vector3.forward;
        float rotationSpeed = 50f;
        transform.RotateAround(rotationCenter, rotationAxis, rotationSpeed * Time.deltaTime);
    }

    private void Movement()
    {
        if (targetPos != null)
        {
            if (Vector2.Distance(transform.position, targetPos.position) > spinDistance)
            {
                AdjustDirection();
                transform.position = Vector2.MoveTowards(transform.position, targetPos.position, speed * Time.deltaTime);
            }
            else
            {
                Spin();
            }
        }
    }
    private void StartAttack()
    {
        if (Time.time >= startAttackTime + attackIntervalTime)
        {
            Thunder2_Attack attack = PoolManager.GetItem<Thunder2_Attack>(Thunder2_Attack.prefabWays, creatAttackPos.position);
            attack.targetPos = targetPos;
            startAttackTime = Time.time;
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
        startTime = Time.time;
    }
}
