using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boos1 : MonoBehaviour
{
    private string attackNumway = "Prefabs/EnemyHurtDisplay";
    [SerializeField] private GameObject enemyManager;
    [SerializeField] private float currentHp;
    [SerializeField] private float maxHP;
    private Vector2 rec;
    [SerializeField] private GameObject Transfer;
    private player gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    private Rigidbody2D rigi => GetComponent<Rigidbody2D>();
    private Animator anim => GetComponent<Animator>();
    [Header("剑阵")]
    private bool canCure = true;
    private float startCureTime = 0f;
    [SerializeField] private float intervalCureTime;
    [SerializeField] private Transform createPos;
    [SerializeField] public List<GameObject> squareSword = new List<GameObject>();
    [SerializeField] private GameObject cure;
    [SerializeField] private Image Hp;
    [SerializeField] private Transform displayPos;
    [Header("剑雨")]
    private bool isRain;
    private bool canRain = true;
    private float cdStartTime = 0f;
    [SerializeField] private float cdIntervalTime;
    private float rainStartTime = 0f;
    [SerializeField] private float rainDurationTime;
    [SerializeField] private Transform originPos;
    [SerializeField] private Transform SwordRainPos;
    [SerializeField] private float speed;
    [SerializeField] private GameObject trail;
    [SerializeField] private GameObject swordRain;
    [Header("剑气")]
    private float mode1StartTime = 0f;
    [SerializeField] private float mode1IntervalTime;
    private bool isMode1 = true;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Hp.fillAmount = currentHp / maxHP;
        if (currentHp <= 0)
        {

            Destroy(enemyManager);
            for (int x = 0; x < 10; x++)
            {
                PoolManager.GetItem<MpRecover>(MpRecover.prefabWays, transform.position);
                PoolManager.GetItem<GoldPickUp>(GoldPickUp.prefabWays, transform.position);
            }
            Transfer.SetActive(true);
            return;
        }
        if (isMode1)
            Mode1();
        if (currentHp <= maxHP / 2 && canRain)
        {
            cdStartTime += Time.deltaTime;
            if (cdStartTime > cdIntervalTime)
            {
                isMode1 = false;
                Mode3();
            }
        }
        if (currentHp <= maxHP / 4 && canCure)
        {
            isMode1 = false;
            canRain = false;
            Mode2();
        }

    }

    private void FixedUpdate()
    {

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack") && !cure.activeSelf)
        {
            PlayerDamage damage = other.GetComponent<PlayerDamage>();
            currentHp -= damage.playerDamage;
            DisplayAttackNum displayAttack = PoolManager.GetItem<DisplayAttackNum>(attackNumway, displayPos.position);
            displayAttack.transform.position = displayPos.position;
            displayAttack.attackNum.text = damage.playerDamage.ToString();
        }
        if (other.CompareTag("PlayerDistanceAttack"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            DisplayAttackNum displayAttack = PoolManager.GetItem<DisplayAttackNum>(attackNumway, displayPos.position);
            currentHp -= bullet.attackNum;
            displayAttack.transform.position = displayPos.position;
            displayAttack.attackNum.text = bullet.attackNum.ToString();
        }
        else if (other.CompareTag("PlayerCloseAttack"))
        {
            CloseWeapon closeWeapon = other.GetComponentInParent<CloseWeapon>();
            DisplayAttackNum displayAttack = PoolManager.GetItem<DisplayAttackNum>(attackNumway, displayPos.position);
            currentHp -= closeWeapon.attackNum;
            displayAttack.transform.position = displayPos.position;
            displayAttack.attackNum.text = closeWeapon.attackNum.ToString();
        }
    }
    /*    public void CheckAmount()
        {
            for (int x = 0; x < squareSword.Count; x++)
            {
                if (squareSword[x] == null)
                {
                    squareSword.Remove(squareSword[x]);
                    Debug.Log(squareSword.Count);
                }
            }
        }*/

    //剑气攻击
    public void Mode1()
    {
        if (Time.time >= mode1StartTime + mode1IntervalTime)
        {
            anim.SetTrigger("isAttack");
            AdjustDirectionToPlayer();
            mode1StartTime = Time.time;
        }

    }


    //四方之剑回血
    public void Mode2()
    {
        //CheckAmount();

        if (squareSword.Count == 0)
        {
            cure.SetActive(false);
            canCure = false;
            isMode1 = true;
            canRain = true;
        }
        else
        {
            cure.SetActive(true);
        }
        if (cure.activeSelf)
        {
            startCureTime += Time.deltaTime;
            if (startCureTime >= intervalCureTime)
            {
                currentHp += 5f;
                startCureTime = 0f;
            }
        }
    }

    //剑雨
    public void Mode3()
    {

        rainStartTime += Time.deltaTime;
        if (rainStartTime <= rainDurationTime)
        {
            trail.SetActive(true);
            if (Vector2.Distance(transform.position, SwordRainPos.position) <= 0.2f)
            {
                AdjustDirectionToOrigin();
                swordRain.SetActive(true);
            }
            else
            {
                AdjustDirectionToSwordRain();
                transform.position = Vector2.MoveTowards(transform.position, SwordRainPos.position, speed * Time.deltaTime);
            }
        }
        else
        {

            swordRain.SetActive(false);
            if (Vector2.Distance(transform.position, originPos.position) <= 0.2f)
            {
                rainStartTime = 0f;
                cdStartTime = 0f;
                trail.SetActive(false);
            }
            else
            {
                AdjustDirectionToOrigin();
                transform.position = Vector2.MoveTowards(transform.position, originPos.position, speed * Time.deltaTime);
            }
            isMode1 = true;
        }
    }

    public void AdjustDirectionToPlayer()
    {
        rec = (gamePlayer.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(rec.y, rec.x) * Mathf.Rad2Deg;//计算角度
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public void AdjustDirectionToSwordRain()
    {
        rec = (SwordRainPos.position - transform.position).normalized;
        float angle = Mathf.Atan2(rec.y, rec.x) * Mathf.Rad2Deg;//计算角度
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public void AdjustDirectionToOrigin()
    {
        rec = (originPos.position - transform.position).normalized;
        float angle = Mathf.Atan2(rec.y, rec.x) * Mathf.Rad2Deg;//计算角度
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public void SwordGasAttack()
    {
        PoolManager.GetItem<SwordGas>(SwordGas.prefabWays, createPos.position);
    }
}
