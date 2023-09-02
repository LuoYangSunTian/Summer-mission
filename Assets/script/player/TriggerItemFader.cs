using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerItemFader : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        ItemFader faders = collision.GetComponent<ItemFader>();
        if (faders != null)
            faders.Fadeout();


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ItemFader faders = collision.GetComponent<ItemFader>();
        if (faders != null)
            faders.Fadein();
    }
}
