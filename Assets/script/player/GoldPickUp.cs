using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickUp : MonoBehaviour, Recycleable
{
    private player gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    public static string prefabWays = "Prefabs/Gold";
    [SerializeField] private float speed;
    [SerializeField] private int price;
    private Transform pool => GameObject.FindGameObjectWithTag("PoolManager").GetComponent<Transform>();
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, gamePlayer.transform.position, speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EventHandler.CallUpdateMoney(ShopManager.Instance.money + price);
            Destroy(gameObject);
            //PoolManager.Recycle<GoldPickUp>(this, prefabWays);
        }
    }

    public void AfterGet()
    {

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
}
