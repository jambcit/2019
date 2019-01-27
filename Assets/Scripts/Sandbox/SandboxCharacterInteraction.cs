using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Home.Sandbox
{
    public class SandboxCharacterInteraction : MonoBehaviour
    {
        public SandboxObjectInteraction interactableTarget;

        void Update()
        {
            if (interactableTarget != null
                && Input.GetKey(KeyCode.E))
            {
                Debug.Log("Try " + interactableTarget.gameObject.name);

                interactableTarget.StartInteraction();
                //TODO holding
            }

            if (interactableTarget != null
                && Input.GetKeyUp(KeyCode.E))
            {
                interactableTarget.ResetGuage();
            }
        }

        public void updateInteractableObject(SandboxObjectInteraction interactableTarget)
        {
            this.interactableTarget = interactableTarget;
        }

    }
}