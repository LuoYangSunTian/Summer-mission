using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire3 : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private SpriteRenderer sprite;

    [Header("参数")]
    private float mouseStartTime;
    private float mouseDurationTime = 0.15f;
    private float startTime;
    [SerializeField] private float durationTime;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        startTime = 0f;

    }
    // Update is called once per frame
    void Update()
    {
        startTime += Time.deltaTime;
        if (startTime > durationTime)
        {
            gameObject.SetActive(false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            mouseStartTime = Time.time;
        }
        if (Input.GetMouseButtonUp(1))
        {
            float durationTime = Time.time - mouseStartTime;
            if (durationTime >= mouseDurationTime)
            {
                anim.SetTrigger("isAttack2");
            }
            else
            {
                anim.SetTrigger("isAttack1");
            }
        }
        TurnDirec();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyAttack"))
        {
            Destroy(other.gameObject);
        }
    }
    private void TurnDirec()
    {
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
        {
            transform.localScale = new Vector3(-2f, 2f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(2f, 2f, 1f);
        }
    }
}
