using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, Recycleable
{
    //字典储存对象名字和预制体的路径
    public static Dictionary<string, string> BULLETS = new Dictionary<string, string>
    {
        {"Arrow","Prefabs/Bullet"}
    };
    public static Dictionary<string, float> OriginAttackDamage = new Dictionary<string, float>
    {
        {"Arrow", 5f}
    };
    [Header("参数")]
    [SerializeField] private string bulletName;
    [SerializeField] private float speed;
    [SerializeField] public float attackNum;
    [Header("组件")]
    private Rigidbody2D rigi;
    [SerializeField] private Camera mainCamera;
    private bool canShot = false;//可以射击
    [SerializeField] private GameObject trail;
    private Transform pool => GameObject.FindGameObjectWithTag("PoolManager").GetComponent<Transform>();

    private Vector2 rec;
    private void Start()
    {
        rigi = GetComponent<Rigidbody2D>();

    }
    private void FixedUpdate()
    {
        if (canShot)
            MoveMent();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Wall"))
        {
            if (other.CompareTag("Enemy"))
            {
                transform.SetParent(other.transform);
            }
            canShot = false;
            trail.SetActive(false);
            Invoke("Destroy", 2f);
        }
    }

    /*    private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                canShot = false;
                Debug.Log("||||");
                Invoke("Destroy", 1f);
            }
        }*/
    public void MoveMent()
    {
        rigi.MovePosition(rigi.position + rec * speed * Time.deltaTime);
    }

    public void Destroy()
    {
        attackNum = OriginAttackDamage[bulletName];
        transform.SetParent(null);
        PoolManager.Recycle<Bullet>(this, BULLETS[bulletName]);//回收  进对象池
    }
    /// <summary>
    /// 生成子弹
    /// </summary>
/*    public void CreateBullet(string name, float x, float y)
    {
        //判断传入的名字是否在字典中存在，存在的话调用对象池生成子弹
        if (BULLETS.ContainsKey(name))
        {
            PoolManager.GetItem<Bullet>(Bullet.BULLETS[name], x, y);
        }
    }*/


    /// <summary>
    /// 回收子弹
    /// </summary>

    public void BeforeRecycle()
    {

    }

    public void AfterRecycle()
    {
        transform.SetParent(pool);
    }

    public void BeforeGet()
    {

    }

    public void AfterGet()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        rec = (mouseWorldPos - transform.position).normalized;
        float angle = Mathf.Atan2(rec.y, rec.x) * Mathf.Rad2Deg;//计算角度
        transform.eulerAngles = new Vector3(0, 0, angle);
        canShot = true;
        trail.SetActive(true);
        transform.SetParent(pool);
    }
}
