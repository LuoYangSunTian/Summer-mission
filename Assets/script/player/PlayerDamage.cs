using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public float playerDamage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && other.GetComponent<Rigidbody2D>() != null && gameObject.activeSelf)
        {
            StartCoroutine(CancelForce(other.GetComponentInParent<Rigidbody2D>()));
        }
    }
    IEnumerator CancelForce(Rigidbody2D enemy)
    {
        yield return new WaitForSeconds(1f);
        if (enemy != null)
            enemy.velocity = Vector3.zero;
    }
}
