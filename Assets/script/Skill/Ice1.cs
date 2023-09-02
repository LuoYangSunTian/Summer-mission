using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice1 : MonoBehaviour, Recycleable
{
    private Camera mainCamera;
    private Animator anim;
    private Rigidbody2D rigi;
    public static string prefabWays = "Prefabs/Ice1";
    private Vector2 rec;
    [Header("参数")]
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
        PoolManager.Recycle<Ice1>(this, prefabWays);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Enemy"))
        {
            GetComponent<Collider2D>().enabled = false;
            anim.SetTrigger("isHit");
            canMove = false;
            Enemy enemy1 = other.GetComponentInParent<Enemy>();
            Enemy03 enemy2 = other.GetComponentInParent<Enemy03>();
            Invoke("DestroyGameObject", 1f);
            if (enemy1 != null)
            {
                enemy1.gameObject.GetComponentInParent<SpriteRenderer>().color = Color.blue;
                enemy1.Speed *= 0.4f;
                StartCoroutine(RelieveIce(enemy1));

            }
            else if (enemy2 != null)
            {
                enemy2.canMove = false;
                enemy2.gameObject.GetComponentInParent<SpriteRenderer>().color = Color.blue;
                enemy2.speed *= 0.4f;
                StartCoroutine(RelieveIce(enemy2));
            }
        }

    }

    IEnumerator RelieveIce(Enemy enemy)
    {
        yield return new WaitForSeconds(0.5f);

        enemy.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        enemy.Speed /= 0.4f;
    }
    IEnumerator RelieveIce(Enemy03 enemy)
    {
        yield return new WaitForSeconds(0.5f);
        enemy.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        enemy.speed /= 0.4f;
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
        GetComponent<Collider2D>().enabled = true;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        rec = (mouseWorldPos - transform.position).normalized;
        float angle = Mathf.Atan2(rec.y, rec.x) * Mathf.Rad2Deg;//计算角度
        transform.eulerAngles = new Vector3(0, 0, angle);
        canMove = true;
    }
}
