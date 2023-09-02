using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy04 : Enemy03
{

    private Transform player => GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    public override void Attack()
    {
        PoolManager.GetItem<Enemy04_Attack>(Enemy04_Attack.prefabWays, pos[0].position);
    }
}
