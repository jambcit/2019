using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Home.Sandbox
{
    public class SandboxCharacterInteraction
    {

        private SandboxObjectInteraction interactableTarget;

        public SandboxCharacterInteraction()
        {
        }

        public void Update()
        {
            if (interactableTarget != null
                && Input.GetKey(KeyCode.E))
            {
                interactableTarget.StartInteraction();
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