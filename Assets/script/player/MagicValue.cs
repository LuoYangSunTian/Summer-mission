using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicValue : MonoBehaviour, Recycleable
{
    private Transform playerTransform => GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    [SerializeField] private float speed;
    public static string prefabWays = "Prefabs/MagicValue";
    public float magicValue;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

    public void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
    }

    public void BeforeRecycle()
    {

    }

    public void AfterRecycle()
    {

    }

    public void BeforeGet()
    {

    }

    public void AfterGet()
    {

    }
}
