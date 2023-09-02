using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spear : MonoBehaviour
{
    private Vector2 rec;
    [SerializeField] private float consume;
    private PolygonCollider2D coll => GetComponent<PolygonCollider2D>();
    private Camera mainCamera => GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    private player gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    private Rigidbody2D rigi => GetComponent<Rigidbody2D>();
    [SerializeField] private float maxForcePower;
    [Header("蓄力条")]
    [SerializeField] private Image attackStrip;
    [SerializeField] private float maxStrip;
    [SerializeField] private float currentStrip;
    [Header("阻力")]
    [SerializeField] private float resistance;
    private bool canShot;
    private bool isShot;
    private bool isIdel = true;

    [Header("回来")]
    [SerializeField] private Transform pos;
    [SerializeField] private float speed;
    private bool isBack;

    private void Start()
    {
        coll.enabled = false;
    }

    private void Update()
    {
        attackStrip.fillAmount = currentStrip / maxStrip;
        if (isIdel)
            AdjustDirecToMouse();
        if (Input.GetMouseButton(0) && !isShot && !isBack)
        {
            currentStrip += 0.01f;
        }
        if (Input.GetMouseButtonUp(0) && attackStrip.fillAmount > 0.3f)
        {
            isIdel = false;
            canShot = true;
            isShot = true;
            if (gamePlayer.currentMp >= consume)
            {
                gamePlayer.currentMp -= consume;
                coll.enabled = true;
                EventHandler.CallUpdateStatus(gamePlayer.currentHp, gamePlayer.maxHp, gamePlayer.currentMp, gamePlayer.maxMp);
            }
        }
        if (Input.GetMouseButtonUp(0) && attackStrip.fillAmount <= 0.3f)
        {
            currentStrip = 0f;
        }
        if (isBack)
        {
            AdjustToPlayer();
            MoveToPlayer();
            if (Vector2.Distance(transform.position, pos.position) < 0.1f)
            {
                isIdel = true;
                isBack = false;
                coll.enabled = false;
            }
        }
    }
    private void FixedUpdate()
    {

        if (canShot)
        {
            Shot();
            canShot = false;
        }
        if (isShot)
        {
            Invoke("Stop", 0.5f);
        }

    }
    public void AdjustDirecToMouse()
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        rec = (mouseWorldPos - transform.position).normalized;
        float angle = Mathf.Atan2(rec.y, rec.x) * Mathf.Rad2Deg;//计算角度
        transform.eulerAngles = new Vector3(0, 0, angle - 45f);
        transform.localPosition = Vector2.zero;
    }

    public void AdjustToPlayer()
    {
        rec = (pos.position - transform.position).normalized;
        float angle = Mathf.Atan2(rec.y, rec.x) * Mathf.Rad2Deg;//计算角度
        transform.eulerAngles = new Vector3(0, 0, angle - 45f);
    }

    public void Stop()
    {
        Vector3 velocity = rigi.velocity;
        float decreaseAmount = resistance * Time.deltaTime;
        velocity = Vector3.Lerp(velocity, Vector3.zero, decreaseAmount);
        rigi.velocity = velocity;
        Invoke("Alter", 0.5f);
    }

    public void Alter()
    {
        rigi.velocity = Vector2.zero;
        isShot = false;
        isBack = true;
    }

    public void Shot()
    {
        rigi.AddForce(maxForcePower * attackStrip.fillAmount * rec);
        currentStrip = 0f;
    }

    public void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, pos.position, speed * Time.deltaTime);
    }
}
