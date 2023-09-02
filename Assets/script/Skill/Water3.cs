using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water3 : MonoBehaviour, Recycleable
{
    public static string prefabWays = "Prefabs/Water3";
    private Transform gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();



    private void OnEnable()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyAttack"))
        {
            Destroy(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DestoryGameObject()
    {
        PoolManager.Recycle<Water3>(this, prefabWays);
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
