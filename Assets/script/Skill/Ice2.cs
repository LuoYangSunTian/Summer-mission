using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice2 : MonoBehaviour, Recycleable
{
    private Camera mainCamera;
    private Animator anim;
    private Rigidbody2D rigi;
    public static string prefabWays = "Prefabs/Ice2";
    private Vector2 rec;
    [Header("参数")]
    [SerializeField] private float damage;
    [SerializeField] private float speed;
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
        PoolManager.Recycle<Ice2>(this, prefabWays);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Enemy"))
        {
            anim.SetTrigger("isHit");
            transform.eulerAngles = new Vector3(0, 0, 0);
            canMove = false;
            Enemy enemy1 = other.GetComponentInParent<Enemy>();
            Enemy03 enemy2 = other.GetComponentInParent<Enemy03>();
            if (enemy1 != null)
            {
                enemy1.canMove = false;
                enemy1.prepareMove = false;
                StartCoroutine(RelieveIce(enemy1));
            }
            else if (enemy2 != null)
            {
                enemy2.canMove = false;
                StartCoroutine(RelieveIce(enemy2));
            }
        }

    }

    IEnumerator RelieveIce(Enemy enemy)
    {
        yield return new WaitForSeconds(1.5f);
        enemy.canMove = true;
    }
    IEnumerator RelieveIce(Enemy03 enemy)
    {
        yield return new WaitForSeconds(1.5f);
        enemy.canMove = true;
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
