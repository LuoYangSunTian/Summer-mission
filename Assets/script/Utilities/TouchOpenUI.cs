using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchOpenUI : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [SerializeField] public GameObject weapon;
    [SerializeField] private BoxCollider2D boxColl;
    public player play;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            weapon = GameObject.FindGameObjectWithTag("Weapon");
            UI.SetActive(true);
            if (weapon != null)
                weapon.SetActive(false);
            play = collision.GetComponent<player>();
            play.canMove = false;
            boxColl.enabled = false;
            //Time.timeScale = 0;
        }
    }
}
