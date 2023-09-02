using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holy3 : MonoBehaviour, Recycleable
{
    public static string prefabWays = "Prefabs/Holy3";


    private player gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<player>();

    private float startCureTime = -10f;
    [SerializeField] private float intervalTime;//治疗间隔时间

    private float startTime;
    [SerializeField] private float durationTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0, 2f, 0);
        if (gamePlayer.currentHp < gamePlayer.maxHp)
            Cure();
        if (Time.time > startTime + durationTime)
            PoolManager.Recycle<Holy3>(this, prefabWays);
    }

    //治疗玩家
    private void Cure()
    {
        if (Time.time >= startCureTime + intervalTime)
        {
            PoolManager.GetItem<Holy3_Cure>(Holy3_Cure.curePrefabWays, gamePlayer.transform.position);
            startCureTime = Time.time;
        }
    }
    public void AfterGet()
    {
        transform.SetParent(gamePlayer.transform);
        transform.localPosition = new Vector3(0, 2f, 0);
        startTime = Time.time;
    }

    public void AfterRecycle()
    {

    }

    public void BeforeGet()
    {

    }

    public void BeforeRecycle()
    {

    }
}
