using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class weapon : MonoBehaviour
{
    public Camera cam;
    private Vector3 mousePos;
    public Vector2 weaponDirec;
    private Animator anim;
    [SerializeField] private float consume;
    [SerializeField] private Transform BulletCreatPos;
    [SerializeField] private GameObject Arows;
    [Header("蓄力条")]
    [SerializeField] private float currentStrip;
    [SerializeField] private float maxStrip;
    [SerializeField] private Image strip;
    private player gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    private bool canDraw = true;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        strip.fillAmount = currentStrip / maxStrip;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);//将屏幕坐标转化为世界坐标后获取当前鼠标的位置
        mousePos.z = 0f;
        //Vector3 pos = new Vector3(transform.position.x + 0.18f, transform.position.y - 0.5f, transform.position.z);
        weaponDirec = (mousePos - transform.position).normalized;//鼠标位置减去武器位置再变成单位向量
        float angle = Mathf.Atan2(weaponDirec.y, weaponDirec.x) * Mathf.Rad2Deg;//计算角度
        transform.eulerAngles = new Vector3(0, 0, angle + 44.134f);
        if (Input.GetKeyDown(KeyCode.Mouse0) && !InteractUI())
        {
            anim.SetTrigger("isShot");
            Arows.SetActive(true);

        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && !InteractUI() && strip.fillAmount >= 0.3f)
        {
            anim.SetTrigger("noShot");
            Arows.SetActive(false);
            if (gamePlayer.currentMp >= consume)
            {
                CreatBullet();
                AndioManager.Instance.SwitchPlayerMusic("shoot");
                gamePlayer.currentMp -= consume;
                EventHandler.CallUpdateStatus(gamePlayer.currentHp, gamePlayer.maxHp, gamePlayer.currentMp, gamePlayer.maxMp);
            }
            canDraw = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && !InteractUI() && strip.fillAmount < 0.3f)
        {
            currentStrip = 0f;
            anim.SetTrigger("noShot");
            Arows.SetActive(false);
        }

        if (Input.GetMouseButton(0) && canDraw && !InteractUI())
        {
            currentStrip += 0.02f;
        }

    }

    public void CreatBullet()
    {
        currentStrip = 0f;
        Bullet bullet = PoolManager.GetItem<Bullet>(Bullet.BULLETS["Arrow"], BulletCreatPos.position);

        if (strip.fillAmount >= 0.9f)
            bullet.attackNum += 10f;
    }

    /// <summary>
    /// 判断鼠标是否与UI有交互
    /// </summary>
    /// <returns></returns>
    private bool InteractUI()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return true;
        else
            return false;
    }
}
