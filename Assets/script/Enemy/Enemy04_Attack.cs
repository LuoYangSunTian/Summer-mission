using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy04_Attack : MonoBehaviour, Recycleable
{
    [SerializeField] private float time;
    public static string prefabWays = "Prefabs/Enemy/Enemy04_Attack";
    private Transform player => GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    private float targetLenth = 300f;
    private float startTime;
    private Transform currentTransform => GetComponent<Transform>();

    private void OnEnable()
    {
        startTime = Time.time;
    }
    private void Update()
    {

        if (Time.time >= startTime + time)
            PoolManager.Recycle<Enemy04_Attack>(this, prefabWays);
        currentTransform.localScale = new Vector3(Mathf.MoveTowards(currentTransform.localScale.x, targetLenth, (targetLenth - currentTransform.localScale.x) / time * Time.deltaTime), currentTransform.localScale.y, currentTransform.localScale.z);
    }
    public void AfterGet()
    {
        Vector2 rec = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(rec.y, rec.x) * Mathf.Rad2Deg;//计算角度
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public void AfterRecycle()
    {
        transform.localScale = new Vector3(2f, currentTransform.localScale.y, currentTransform.localScale.z);
    }

    public void BeforeGet()
    {

    }

    public void BeforeRecycle()
    {

    }
}
