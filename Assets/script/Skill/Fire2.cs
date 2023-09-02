using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire2 : MonoBehaviour, Recycleable
{
    public LayerMask targetMask;
    private Camera mainCamera;
    private Animator anim;
    private Rigidbody2D rigi;
    public static string prefabWays = "Prefabs/Fire2";
    private Vector2 rec;
    [Header("参数")]
    [SerializeField] private float speed;
    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionRadius;
    public Transform pos;
    private bool canMove = true;
    private void Start()
    {
        anim = GetComponent<Animator>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rigi = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (canMove)
            MoveMent();
    }

    public void MoveMent()
    {
        rigi.MovePosition(rigi.position + rec * speed * Time.deltaTime);
    }

    public void DestroyGameObject()
    {
        PoolManager.Recycle<Fire2>(this, prefabWays);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Enemy"))
        {

            anim.SetTrigger("isHit");
            transform.eulerAngles = new Vector3(0, 0, 0);
            canMove = false;
            Explosion();
        }

    }

    public void Explosion()
    {
        Vector2 point = new Vector2(transform.position.x, transform.position.y);
        Collider2D[] enemys = Physics2D.OverlapCircleAll(point, explosionRadius, targetMask);//返回检测范围内的所有碰撞体
        foreach (var enemy in enemys)
        {

            if (enemy.gameObject.CompareTag("Enemy"))
            {

                Rigidbody2D rigi = enemy.GetComponentInParent<Rigidbody2D>();
                if (rigi != null)
                {

                    Vector3 explosionDirection = enemy.transform.position - transform.position;
                    rigi.AddForce(explosionDirection.normalized * explosionForce, ForceMode2D.Impulse);//通过rigibody添加力
                    StartCoroutine(CancelForce(rigi));
                }
            }
        }
    }
    IEnumerator CancelForce(Rigidbody2D enemy)
    {
        yield return new WaitForSeconds(1f);
        enemy.velocity = Vector3.zero;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
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
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        rec = (mouseWorldPos - transform.position).normalized;
        float angle = Mathf.Atan2(rec.y, rec.x) * Mathf.Rad2Deg;//计算角度
        transform.eulerAngles = new Vector3(0, 0, angle);
        canMove = true;
    }
}
