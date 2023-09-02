using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy07 : Enemy
{
    [SerializeField] private Transform createPos;
    [SerializeField] private Transform[] targetDirec;

    public void CreateAttack()
    {
        PoolManager.GetItem<Enemy07_Attack>(Enemy07_Attack.prefabWays, createPos.position);
        PoolManager.GetItem<Enemy07_Attack1>(Enemy07_Attack1.prefabWays, createPos.position);
        PoolManager.GetItem<Enemy07_Attack2>(Enemy07_Attack2.prefabWays, createPos.position);
        PoolManager.GetItem<Enemy07_Attack3>(Enemy07_Attack3.prefabWays, createPos.position);
    }
}
