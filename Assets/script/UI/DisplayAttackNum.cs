using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DisplayAttackNum : MonoBehaviour, Recycleable
{
    [SerializeField] public TextMesh attackNum;
    private string attackNumway = "Prefabs/EnemyHurtDisplay";
    private float startTime;
    [SerializeField] private float displayTime;
    private Transform pool => GameObject.FindGameObjectWithTag("PoolManager").GetComponent<Transform>();

    public void AfterGet()
    {
        transform.SetParent(pool);
    }

    public void AfterRecycle()
    {
        transform.SetParent(pool);
    }

    public void BeforeGet()
    {

    }

    public void BeforeRecycle()
    {

    }

    private void OnEnable()
    {
        startTime = 0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        attackNum = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        startTime += Time.deltaTime;
        if (startTime >= displayTime)
            PoolManager.Recycle<DisplayAttackNum>(this, attackNumway);
    }
}
