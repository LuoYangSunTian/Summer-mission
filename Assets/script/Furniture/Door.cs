using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool canOpen;
    [SerializeField] private SpriteRenderer sprite;
    private Animator anim => GetComponentInParent<Animator>();
    public BoxCollider2D coll;

    private void Update()
    {
        if (canOpen && Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("isOpen", true);
            coll.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = false;
            anim.SetBool("isOpen", false);
            coll.enabled = true;
        }
    }
}
