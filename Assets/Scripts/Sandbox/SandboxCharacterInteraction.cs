using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Home.Sandbox
{
    public class SandboxCharacterInteraction : MonoBehaviour
    {
        // TODO script for SandboxObjectInteract
        public SandboxObjectInteraction interactableTarget;

        // Start is called before the first frame update
        void Start()
        {
            //if(!character.tag.equals("Player"))
            //{
            //  Debug.LogError("SandboxInteract.Start(): Wrong place to attached, from " + gameObject.name);
        }

        // Update is called once per frame
        void Update()
        {
            if (interactableTarget != null
            && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Try " + interactableTarget.gameObject.name);
                interactableTarget.StartInteraction();
            }
        }

        void onTriggerEnter(Collision collision)
        {
            if (collision.gameObject.name == "InteractableObject")
            {
                interactableTarget = collision.gameObject.GetComponent<SandboxObjectInteraction>();
            }
        }
    }
}