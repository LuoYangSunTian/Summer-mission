using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("组件")]
    public GameObject iceDisplay;
    private string backSortLayer = "instance";
    private string frontSortLayer = "front";
    [SerializeField] private SpriteRenderer[] weaponSprite;
    private Rigidbody2D rigi;
    private Animator anim;
    [SerializeField] public GameObject fireHurt;
    [SerializeField] public GameObject hurtCheck;
    private Camera mainCamera => GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private GameObject weapon;
    [Header("火系魔法阵")]
    [SerializeField] private GameObject magicFirst_Fire;
    [SerializeField] private Animator magicFirstAnim_Fire;
    [SerializeField] private GameObject magicSecond_Fire_1;
    [SerializeField] private GameObject magicSecond_Fire_2;
    [SerializeField] private Animator magicSecondAnim_Fire;
    [SerializeField] private GameObject fire3;

    [SerializeField] private GameObject magicThird_Fire1;
    [SerializeField] private GameObject magicThird_Fire2;
    [SerializeField] private GameObject magicThird_Fire3;
    [SerializeField] private Animator magicThirdAnim_Fire;


    [Header("水系魔法阵")]
    [SerializeField] private GameObject magicFirst_Water;
    [SerializeField] private Animator magicFirstAnim_Water;
    [SerializeField] private GameObject magicSecond_Water_1;
    [SerializeField] private GameObject magicSecond_Water_2;
    [SerializeField] private Animator magicSecondAnim_Water;


    [SerializeField] private GameObject magicThird_Water1;
    [SerializeField] private GameObject magicThird_Water2;
    [SerializeField] private GameObject magicThird_Water3;
    [SerializeField] private Animator magicThirdAnim_Water;
    [SerializeField] private GameObject water1;

    [Header("冰系魔法阵")]
    [SerializeField] private GameObject magicFirst_Ice;
    [SerializeField] private Animator magicFirstAnim_Ice;
    [SerializeField] private GameObject magicSecond_Ice_1;
    [SerializeField] private GameObject magicSecond_Ice_2;
    [SerializeField] private Animator magicSecondAnim_Ice;


    [SerializeField] private GameObject magicThird_Ice1;
    [SerializeField] private GameObject magicThird_Ice2;
    [SerializeField] private GameObject magicThird_Ice3;
    [SerializeField] private Animator magicThirdAnim_Ice;
    private Vector3 pos1_Ice = Vector3.zero;
    private Vector3 pos2_Ice = Vector3.zero;
    private int iceQuantity = 0;


    [Header("光系魔法阵")]
    [SerializeField] private GameObject magicFirst_Holy;
    [SerializeField] private Animator magicFirstAnim_Holy;
    [SerializeField] private GameObject magicSecond_Holy_1;
    [SerializeField] private GameObject magicSecond_Holy_2;
    [SerializeField] private Animator magicSecondAnim_Holy;


    [SerializeField] private GameObject magicThird_Holy1;
    [SerializeField] private GameObject magicThird_Holy2;
    [SerializeField] private GameObject magicThird_Holy3;
    [SerializeField] private Animator magicThirdAnim_Holy;

    [SerializeField] private Transform[] Holy2_PosList;

    [Header("雷系魔法阵")]
    [SerializeField] private GameObject magicFirst_Thunder;
    [SerializeField] private Animator magicFirstAnim_Thunder;
    [SerializeField] private GameObject magicSecond_Thunder_1;
    [SerializeField] private GameObject magicSecond_Thunder_2;
    [SerializeField] private Animator magicSecondAnim_Thunder;


    [SerializeField] private GameObject magicThird_Thunder1;
    [SerializeField] private GameObject magicThird_Thunder2;
    [SerializeField] private GameObject magicThird_Thunder3;
    [SerializeField] private Animator magicThirdAnim_Thunder;
    [SerializeField] private Transform[] Thunder2_PosList;


    [Header("参数")]
    [SerializeField] private Transform createPos;
    [SerializeField] public float currentHp;
    [SerializeField] public float maxHp;
    [SerializeField] public float currentMp;
    [SerializeField] public float maxMp;
    public bool isIce;

    private bool isLife = true;
    public bool canMove = true;
    public float speed;
    public float inputX;
    private float inputY;
    private Vector2 movementInput;

    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.MoveToPosition += OnMoveToPosition;
        EventHandler.CreateMagic += OnCreateMagic;
        EventHandler.Resurgence += OnResurgence;
    }
    private void OnDisable()
    {
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.MoveToPosition -= OnMoveToPosition;
        EventHandler.CreateMagic -= OnCreateMagic;
        EventHandler.Resurgence -= OnResurgence;
    }

    //复活
    private void OnResurgence()
    {
        currentHp = maxHp;
        currentMp = maxMp;
        hurtCheck.SetActive(true);
        EventHandler.CallUpdateStatus(currentHp, maxHp, currentMp, maxMp);
        canMove = true;
        anim.SetTrigger("isLife");
        /*            weapon = GameObject.FindGameObjectWithTag("Weapon");
                    weapon.SetActive(false);*/
        isLife = true;
        Time.timeScale = 1f;
    }

    private void OnCreateMagic(int index)
    {
        switch (index)
        {
            case 101:
                if (currentMp >= 5f)
                    CreateFire1();
                break;
            case 102:
                if (currentMp >= 10f)
                    CreateFire2();
                break;
            case 103:
                if (currentMp >= 15f)
                    CreateFire3();
                break;
            case 201:
                if (currentMp >= 5f)
                    CreateHoly1();
                break;
            case 202:
                if (currentMp >= 10f)
                    CreateHoly2();
                break;
            case 203:
                if (currentMp >= 15f)
                    CreateHoly3();
                break;
            case 301:
                if (currentMp >= 5f)
                    CreateThunder1();
                break;
            case 302:
                if (currentMp >= 10f)
                    CreateThunder2();
                break;
            case 303:
                if (currentMp >= 15f)
                    CreateThunder3();
                break;
            case 401:
                if (currentMp >= 5f)
                    CreateWater1();
                break;
            case 402:
                if (currentMp >= 10f)
                    CreateWater2();
                break;
            case 403:
                if (currentMp >= 15f)
                    CreateWater3();
                break;
            case 501:
                if (currentMp >= 5f)
                    CreateIce1();
                break;
            case 502:
                if (currentMp >= 10f)
                    CreateIce2();
                break;
            case 503:
                if (currentMp >= 15f)
                    CreateIce3();
                break;
        }
    }

    private void OnMoveToPosition(Vector3 targetrPosition)
    {
        transform.position = targetrPosition;
    }

    private void OnAfterSceneLoadedEvent()
    {
        canMove = true;
    }

    private void OnBeforeSceneUnloadEvent()
    {
        canMove = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isLife)
            return;
        if (currentHp <= 0f && isLife)
        {
            hurtCheck.SetActive(false);
            canMove = false;
            anim.SetTrigger("isDie");
            /*            weapon = GameObject.FindGameObjectWithTag("Weapon");
                        weapon.SetActive(false);*/
            isLife = false;
            Time.timeScale = 0f;
        }

        PlayerInput();
        //Turn();
        SwitchAnim();
    }
    private void FixedUpdate()
    {
        if (canMove)
            Movement();
    }

    private void PlayerInput()//获得键盘的输入
    {
        //人物移动部分
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        if (inputX != 0 && inputY != 0)
        {
            inputX *= 0.8f;
            inputY *= 0.8f;
        }

        movementInput = new Vector2(inputX, inputY);

    }
    private void Movement()
    {
        rigi.MovePosition(rigi.position + movementInput * speed * Time.deltaTime);
    }//移动
    /*    private void Turn()//转向
        {
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;
            if ((mouseWorldPos.x - transform.position.x) < 0f)
                sp.flipX = true;
            else
                sp.flipX = false;
        }*/
    private void SwitchAnim()//动画的切换
    {

        if (inputX != 0)
        {
            anim.SetBool(" isSideMove", true);
            if (inputX < 0)
                sp.flipX = true;
            else
                sp.flipX = false;
        }
        else if (inputY != 0)
        {
            if (inputY < 0)
            {
                anim.SetBool("isFrontMove", true);
                foreach (var sprite in weaponSprite)
                {
                    sprite.sortingLayerName = frontSortLayer;
                }
            }
            else
            {
                anim.SetBool("isBackMove", true);
                foreach (var sprite in weaponSprite)
                {
                    sprite.sortingLayerName = backSortLayer;
                }
            }
        }
        if (inputX == 0)
            anim.SetBool(" isSideMove", false);
        if (inputY == 0)
        {
            anim.SetBool("isFrontMove", false);
            anim.SetBool("isBackMove", false);
        }
    }

    #region 火系魔法
    //初阶火系魔法
    private void CreateFire1()
    {
        magicFirst_Fire.SetActive(true);
        if (magicFirstAnim_Fire.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            PoolManager.GetItem<Fire1>(Fire1.prefabWays, createPos.position);
            currentMp -= 5f;
            EventHandler.CallUpdateStatus(currentHp, maxHp, currentMp, maxMp);
            magicFirst_Fire.SetActive(false);
        }
    }
    //中阶火系魔法
    private void CreateFire2()
    {
        if (magicSecondAnim_Fire.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            PoolManager.GetItem<Fire2>(Fire2.prefabWays, createPos.position);
            currentMp -= 10f;
            EventHandler.CallUpdateStatus(currentHp, maxHp, currentMp, maxMp);
            magicSecond_Fire_1.SetActive(false);
            magicSecond_Fire_2.SetActive(false);
            return;
        }
        magicSecond_Fire_1.SetActive(true);
        StartCoroutine(CreatSecondMagic2());

    }

    //中阶魔法阵第二部分

    private IEnumerator CreatSecondMagic2()
    {
        yield return new WaitForSeconds(0.3f);
        magicSecond_Fire_2.SetActive(true);
    }


    //高阶火系魔法
    private void CreateFire3()
    {
        if (magicThirdAnim_Fire.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            magicThird_Fire3.SetActive(false);
            magicThird_Fire2.SetActive(false);
            magicThird_Fire1.SetActive(false);
            fire3.SetActive(true);
            currentMp -= 15f;
            EventHandler.CallUpdateStatus(currentHp, maxHp, currentMp, maxMp);
            return;
        }
        magicThird_Fire1.SetActive(true);
        StartCoroutine(CreatThirdMagic2());
        StartCoroutine(CreatThirdMagic3());

    }

    //高阶魔法阵第二部分
    private IEnumerator CreatThirdMagic2()
    {
        yield return new WaitForSeconds(1f);
        magicThird_Fire2.SetActive(true);

    }

    //高阶魔法阵第三部分
    private IEnumerator CreatThirdMagic3()
    {
        yield return new WaitForSeconds(1.3f);
        magicThird_Fire3.SetActive(true);
    }
    #endregion

    #region 水系魔法
    private void CreateWater1()
    {
        magicFirst_Water.SetActive(true);
        if (magicFirstAnim_Water.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Water1 water = water1.GetComponent<Water1>();
            currentMp -= 5f;
            EventHandler.CallUpdateStatus(currentHp, maxHp, currentMp, maxMp);
            water.defenseAmount = 10f;
            water.startTime = Time.time;
            water1.SetActive(true);
            magicFirst_Water.SetActive(false);
        }
    }

    //中阶水系魔法
    private void CreateWater2()
    {
        if (magicSecondAnim_Water.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            PoolManager.GetItem<Water2>(Water2.prefabWays, createPos.position);
            currentMp -= 10f;
            EventHandler.CallUpdateStatus(currentHp, maxHp, currentMp, maxMp);
            magicSecond_Water_1.SetActive(false);
            magicSecond_Water_2.SetActive(false);
            return;
        }
        magicSecond_Water_1.SetActive(true);
        StartCoroutine(CreatSecondMagic2_Water());

    }

    //中阶魔法阵第二部分

    private IEnumerator CreatSecondMagic2_Water()
    {
        yield return new WaitForSeconds(0.3f);
        magicSecond_Water_2.SetActive(true);
    }

    //高阶火系魔法
    private void CreateWater3()
    {
        if (magicThirdAnim_Water.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            magicThird_Water3.SetActive(false);
            magicThird_Water2.SetActive(false);
            magicThird_Water1.SetActive(false);
            PoolManager.GetItem<Water3>(Water3.prefabWays, createPos.position);
            currentMp -= 15f;
            EventHandler.CallUpdateStatus(currentHp, maxHp, currentMp, maxMp);
            return;
        }
        magicThird_Water1.SetActive(true);
        StartCoroutine(CreatThirdMagic2_Water());
        StartCoroutine(CreatThirdMagic3_Water());

    }

    //高阶魔法阵第二部分
    private IEnumerator CreatThirdMagic2_Water()
    {
        yield return new WaitForSeconds(1f);
        magicThird_Water2.SetActive(true);

    }

    //高阶魔法阵第三部分
    private IEnumerator CreatThirdMagic3_Water()
    {
        yield return new WaitForSeconds(1.3f);
        magicThird_Water3.SetActive(true);
    }
    #endregion

    #region 冰系魔法
    private void CreateIce1()
    {
        magicFirst_Ice.SetActive(true);
        if (magicFirstAnim_Ice.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            PoolManager.GetItem<Ice1>(Ice1.prefabWays, createPos.position);
            currentMp -= 5f;
            EventHandler.CallUpdateStatus(currentHp, maxHp, currentMp, maxMp);
            magicFirst_Ice.SetActive(false);
        }
    }

    //中阶冰系魔法
    private void CreateIce2()
    {
        if (magicSecondAnim_Ice.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            PoolManager.GetItem<Ice2>(Ice2.prefabWays, createPos.position);
            currentMp -= 10f;
            EventHandler.CallUpdateStatus(currentHp, maxHp, currentMp, maxMp);
            magicSecond_Ice_1.SetActive(false);
            magicSecond_Ice_2.SetActive(false);
            return;
        }
        magicSecond_Ice_1.SetActive(true);
        StartCoroutine(CreatSecondMagic2_Ice());

    }

    //中阶魔法阵第二部分

    private IEnumerator CreatSecondMagic2_Ice()
    {
        yield return new WaitForSeconds(0.3f);
        magicSecond_Ice_2.SetActive(true);
    }


    //高阶冰系魔法
    private void CreateIce3()
    {
        if (magicThirdAnim_Ice.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            magicThird_Ice3.SetActive(false);
            magicThird_Ice2.SetActive(false);
            magicThird_Ice1.SetActive(false);
            //生成冰墙
            StartCoroutine(CreatIceWall());
            currentMp -= 15f;
            EventHandler.CallUpdateStatus(currentHp, maxHp, currentMp, maxMp);
            return;
        }
        magicThird_Ice1.SetActive(true);
        StartCoroutine(CreatThirdMagic2_Ice());
        StartCoroutine(CreatThirdMagic3_Ice());

    }

    //高阶魔法阵第二部分
    private IEnumerator CreatThirdMagic2_Ice()
    {
        yield return new WaitForSeconds(1f);
        magicThird_Ice2.SetActive(true);

    }

    //高阶魔法阵第三部分
    private IEnumerator CreatThirdMagic3_Ice()
    {
        yield return new WaitForSeconds(1.3f);
        magicThird_Ice3.SetActive(true);
    }

    //生成冰墙

    private IEnumerator CreatIceWall()
    {
        for (int x = 0; x < 2; x++)
        {
            yield return new WaitForMouseRightClick();
            if (pos1_Ice == Vector3.zero)
            {
                pos1_Ice = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (pos2_Ice == Vector3.zero)
            {
                pos2_Ice = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            }

            yield return null;
        }
        float distance = Vector3.Distance(pos1_Ice, pos2_Ice);
        iceQuantity = (int)(distance / Ice3.ice3Wide);
        for (int x = 0; x < iceQuantity; x++)
        {
            float t = x / (float)(iceQuantity - 1); //计算差值因子，相当于给每个生成的物体分配一个点
            Vector3 position = Vector3.Lerp(pos1_Ice, pos2_Ice, t);
            PoolManager.GetItem<Ice3>(Ice3.prefabWays, position);
        }
        pos1_Ice = Vector3.zero;
        pos2_Ice = Vector3.zero;
    }

    private class WaitForMouseRightClick : CustomYieldInstruction//创建一个类，继承至抽象基类，用于自定义协程的等待条件，因为继承至CustomYieldInstruction所以可以用作携程的等待条件
    {
        public override bool keepWaiting
        {
            get { return !Input.GetMouseButtonDown(1); }
        }
    }
    #endregion

    #region 光系魔法

    private void CreateHoly1()
    {
        magicFirst_Holy.SetActive(true);
        if (magicFirstAnim_Holy.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            PoolManager.GetItem<Holy1>(Holy1.prefabWays, createPos.position);
            magicFirst_Holy.SetActive(false);
            currentMp -= 5f;
            EventHandler.CallUpdateStatus(currentHp, maxHp, currentMp, maxMp);
        }
    }
    //中阶光系魔法
    private void CreateHoly2()
    {
        if (magicSecondAnim_Holy.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            for (int x = 0; x < Holy2_PosList.Length; x++)
            {
                Holy2 holy2 = PoolManager.GetItem<Holy2>(Holy2.prefabWays, Holy2_PosList[x].position);
                holy2.transform.SetParent(Holy2_PosList[x]);
            }
            magicSecond_Holy_1.SetActive(false);
            magicSecond_Holy_2.SetActive(false);
            currentMp -= 10f;
            EventHandler.CallUpdateStatus(currentHp, maxHp, currentMp, maxMp);
            return;
        }
        magicSecond_Holy_1.SetActive(true);
        StartCoroutine(CreatSecondMagic2_Holy());

    }

    //中阶魔法阵第二部分

    private IEnumerator CreatSecondMagic2_Holy()
    {
        yield return new WaitForSeconds(0.3f);
        magicSecond_Holy_2.SetActive(true);
    }

    //高阶光系魔法
    private void CreateHoly3()
    {
        if (magicThirdAnim_Holy.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            magicThird_Holy3.SetActive(false);
            magicThird_Holy2.SetActive(false);
            magicThird_Holy1.SetActive(false);
            PoolManager.GetItem<Holy3>(Holy3.prefabWays, createPos.position);
            currentMp -= 15f;
            EventHandler.CallUpdateStatus(currentHp, maxHp, currentMp, maxMp);
            return;
        }
        magicThird_Holy1.SetActive(true);
        StartCoroutine(CreatThirdMagic2_Holy());
        StartCoroutine(CreatThirdMagic3_Holy());

    }

    //高阶魔法阵第二部分
    private IEnumerator CreatThirdMagic2_Holy()
    {
        yield return new WaitForSeconds(1f);
        magicThird_Holy2.SetActive(true);

    }

    //高阶魔法阵第三部分
    private IEnumerator CreatThirdMagic3_Holy()
    {
        yield return new WaitForSeconds(1.3f);
        magicThird_Holy3.SetActive(true);
    }
    #endregion

    #region 雷系魔法
    private void CreateThunder1()
    {
        magicFirst_Thunder.SetActive(true);
        if (magicFirstAnim_Thunder.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            PoolManager.GetItem<Thunder1>(Thunder1.prefabWays, createPos.position);
            magicFirst_Thunder.SetActive(false);
            currentMp -= 5f;
            EventHandler.CallUpdateStatus(currentHp, maxHp, currentMp, maxMp);
        }
    }

    //中阶雷系魔法
    private void CreateThunder2()
    {
        if (magicSecondAnim_Thunder.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            for (int x = 0; x < Thunder2_PosList.Length; x++)
            {
                Thunder2 thunder2 = PoolManager.GetItem<Thunder2>(Thunder2.prefabWays, Thunder2_PosList[x].position);
                thunder2.transform.SetParent(Thunder2_PosList[x]);
                thunder2.parent = Thunder2_PosList[x];
            }
            currentMp -= 10f;
            EventHandler.CallUpdateStatus(currentHp, maxHp, currentMp, maxMp);
            magicSecond_Thunder_1.SetActive(false);
            magicSecond_Thunder_2.SetActive(false);
            return;
        }
        magicSecond_Thunder_1.SetActive(true);
        StartCoroutine(CreatSecondMagic2_Thunder());

    }

    //中阶魔法阵第二部分

    private IEnumerator CreatSecondMagic2_Thunder()
    {
        yield return new WaitForSeconds(0.3f);
        magicSecond_Thunder_2.SetActive(true);
    }

    //高阶雷系魔法
    private void CreateThunder3()
    {
        if (magicThirdAnim_Thunder.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            magicThird_Thunder3.SetActive(false);
            magicThird_Thunder2.SetActive(false);
            magicThird_Thunder1.SetActive(false);
            PoolManager.GetItem<Thunder3>(Thunder3.prefabWays, mainCamera.ScreenToWorldPoint(Input.mousePosition));
            currentMp -= 15f;
            EventHandler.CallUpdateStatus(currentHp, maxHp, currentMp, maxMp);
            return;
        }
        magicThird_Thunder1.SetActive(true);
        StartCoroutine(CreatThirdMagic2_Thunder());
        StartCoroutine(CreatThirdMagic3_Thunder());

    }

    //高阶魔法阵第二部分
    private IEnumerator CreatThirdMagic2_Thunder()
    {
        yield return new WaitForSeconds(1f);
        magicThird_Thunder2.SetActive(true);

    }

    //高阶魔法阵第三部分
    private IEnumerator CreatThirdMagic3_Thunder()
    {
        yield return new WaitForSeconds(1.3f);
        magicThird_Thunder3.SetActive(true);
    }
    #endregion
}
