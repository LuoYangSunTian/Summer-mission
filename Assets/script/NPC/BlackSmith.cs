using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSmith : MonoBehaviour
{

    private bool canTalk;
    public GameObject symbol;

    private void Update()
    {
        if (canTalk && Input.GetKeyDown(KeyCode.T))
            ShopManager.Instance.UpdateShop();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTalk = true;
            symbol.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTalk = false;
            symbol.SetActive(false);
        }
    }
}
