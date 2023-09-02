using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearch : MonoBehaviour
{
    HeroSword sword => GetComponentInParent<HeroSword>();

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!sword.enemyTransforms.Contains(other.transform))
            {
                sword.enemyTransforms.Add(other.transform);
            }
        }
    }
}
