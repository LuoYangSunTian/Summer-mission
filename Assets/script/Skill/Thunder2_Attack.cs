using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder2_Attack : MonoBehaviour, Recycleable
{
    public static string prefabWays = "Prefabs/Thunder2_attack";
    private Animator anim;
    private Vector2 rec;
    [Header("参数")]
    [SerializeField] private float speed;
    public Transform targetPos;
    private bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
            MoveMent();
    }

    public void MoveMent()
    {
        if (targetPos != null)
            transform.position = Vector2.MoveTowards(transform.position, targetPos.position, speed * Time.deltaTime);
        else
            DestroyGameObject();
    }

    public void DestroyGameObject()
    {
        PoolManager.Recycle<Thunder2_Attack>(this, prefabWays);
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

    }
}
