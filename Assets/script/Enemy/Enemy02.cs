using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02 : Enemy
{
    [SerializeField] private GameObject attackDisplay;
    public override void SwitchToAttack()
    {
        base.SwitchToAttack();
        attackDisplay.SetActive(true);
    }
    public void DestoryAttackDisplay()
    {
        attackDisplay.SetActive(false);
    }

    public void CreateAttack()
    {
        PoolManager.GetItem<Enemy02_Attack>(Enemy02_Attack.prefabWays, attackDisplay.transform.position);
    }
}
