using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holy2_EnemyCheck : MonoBehaviour
{
    public List<EnemyHurtCheck> enemyList = new List<EnemyHurtCheck>();

    private void Update()
    {
        transform.localPosition = Vector3.zero;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        EnemyHurtCheck enemy = other.GetComponent<EnemyHurtCheck>();
        if (enemy != null)
        {
            enemyList.Add(enemy);

        }
    }

}
