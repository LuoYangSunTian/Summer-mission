using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holy2 : MonoBehaviour, Recycleable
{
    public static string prefabWays = "Prefabs/Holy2";
    private Camera mainCamera => GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    private player gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    private Collider2D[] enemy;
    [SerializeField] private LayerMask targetMask;
    [Header("参数")]
    [SerializeField] private float attackRadius;
    [SerializeField] private bool canMove;
    [SerializeField] private bool isPrepareState;
    private Vector3 rec;
    private Vector3 targetPos;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
            MoveToTarger();
        if (enemy == null)
        {
            AddEnemyInList();
        }
        else
            SelectTarget();
        if (isPrepareState)
        {
            transform.localPosition = Vector3.zero;
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;
            rec = (mouseWorldPos - transform.position).normalized;
            float angle = Mathf.Atan2(rec.y, rec.x) * Mathf.Rad2Deg;//计算角度
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            DestoryGameObject();
        }
    }

    public void DestoryGameObject()
    {
        PoolManager.Recycle<Holy2>(this, prefabWays);
    }

    public void AddEnemyInList()
    {
        Vector2 point = new Vector2(transform.position.x, transform.position.y);
        enemy = Physics2D.OverlapCircleAll(point, attackRadius, targetMask);
    }

    public void SelectTarget()
    {
        for (int x = 0; x < enemy.Length; x++)
        {
            if (enemy[x] != null && enemy[x].gameObject.CompareTag("Enemy"))
            {
                Transform target = enemy[x].GetComponentInParent<Transform>();
                targetPos = target.position;
                canMove = true;
                isPrepareState = false;
                transform.SetParent(null);
                return;
            }
        }
        AddEnemyInList();
    }

    private void MoveToTarger()
    {
        rec = (targetPos - transform.position).normalized;
        float angle = Mathf.Atan2(rec.y, rec.x) * Mathf.Rad2Deg;//计算角度
        transform.eulerAngles = new Vector3(0, 0, angle);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
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
        isPrepareState = true;
        canMove = false;
    }
}
