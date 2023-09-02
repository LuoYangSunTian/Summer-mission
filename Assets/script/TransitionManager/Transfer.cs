using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Transition
{
    public class Transfer : MonoBehaviour
    {
        public string sceneToGO;
        public Vector3 positionToGO;
        private Animator anim => GetComponentInParent<Animator>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                anim.enabled = true;
                EventHandler.CallBeforeSceneUnloadEvent();
                Invoke("TransferNextMap", 3f);
            }
        }

        public void TransferNextMap()
        {
            EventHandler.CallTransitionEvent(sceneToGO, positionToGO);
        }
    }
}

