using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameobject.Skill
{
    public class AweakeningMagic : Singleton<AweakeningMagic>
    {
        [SerializeField] private Symbol symbol;
        [SerializeField] private GameObject Magic1;
        [SerializeField] private GameObject Magic2;
        [SerializeField] private GameObject Magic3;
        public bool isUse;
        public bool isEnter;
        private player gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
        [SerializeField] private GameObject enterCheck;
        private void Update()
        {
            if (isUse)
            {
                EventHandler.CallCancelWeapon();

                gamePlayer.canMove = false;
                symbol.gameObject.SetActive(true);
                Invoke("CreatMagic3", 2f);
                Invoke("CreatMagic2", 2.2f);
                Invoke("CreatMagic1", 2.8f);
                Invoke("AwakeningUI", 4f);
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                isEnter = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                isEnter = false;
                isUse = false;
            }
        }
        private void CreatMagic3()
        {
            Magic3.SetActive(true);
        }

        private void CreatMagic2()
        {
            Magic2.SetActive(true);
        }

        private void CreatMagic1()
        {
            Magic1.SetActive(true);
        }

        private void AwakeningUI()
        {

            EventHandler.CallUpdateAwakeningUI();

        }

        private void OnEnable()
        {
            EventHandler.CloseAwakeningCircle += CloseMagicCircle;
        }

        private void OnDisable()
        {
            EventHandler.CloseAwakeningCircle -= CloseMagicCircle;
        }
        private void CloseMagicCircle()
        {
            symbol.gameObject.SetActive(false);
            symbol.left.SetActive(false);
            symbol.right.SetActive(false);
            symbol.up.SetActive(false);
            symbol.down.SetActive(false);
            Magic1.SetActive(false);
            Magic2.SetActive(false);
            Magic3.SetActive(false);
            gamePlayer.canMove = true;
            enterCheck.SetActive(false);
        }
    }
}

