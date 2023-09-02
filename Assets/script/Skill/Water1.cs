using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water1 : MonoBehaviour, Recycleable
{
    public static string prefabWays = "Prefabs/Water1";
    private Transform gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    public float startTime;
    [SerializeField] private float durationTime;
    [SerializeField] public float defenseAmount;

    private void OnEnable()
    {

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= startTime + durationTime || defenseAmount <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyAttack"))
        {
            EnemyDamage enemyDamage = other.GetComponent<EnemyDamage>();
            defenseAmount -= enemyDamage.enemyDamage;
        }
    }
    public void AfterGet()
    {
        transform.SetParent(gamePlayer);

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
