using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder1 : MonoBehaviour, Recycleable
{
    public static string prefabWays = "Prefabs/Thunder1";
    private Camera mainCamera;
    private Animator anim;
    private Rigidbody2D rigi;
    private Vector2 rec;
    [Header("参数")]
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    private bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rigi = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
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
        PoolManager.Recycle<Thunder1>(this, prefabWays);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Enemy"))
        {
            anim.SetTrigger("isHit");
            canMove = false;
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
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        rec = (mouseWorldPos - transform.position).normalized;
        float angle = Mathf.Atan2(rec.y, rec.x) * Mathf.Rad2Deg;//计算角度
        transform.eulerAngles = new Vector3(0, 0, angle);
        canMove = true;
    }
}
