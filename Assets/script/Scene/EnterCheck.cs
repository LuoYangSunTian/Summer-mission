using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCheck : MonoBehaviour
{
    private PassManager passManager => GetComponentInParent<PassManager>();
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AndioManager.Instance.SwitchBackMusic("fit");
            passManager.passDoor.SetActive(true);
            passManager.AllEnemy.SetActive(true);
        }
    }
}
