﻿using Home.Core;
using UnityEngine;
using Photon.Pun;

namespace Home.Sandbox
{
    // interactable gamemode
    // if gamemode show the popup
    
    public class SandboxObjectInteraction : MonoBehaviour
    {
        public Transform HUDInteractText;
        public Transform ObjectGuage;

        public float curGuage = 0.000f;

        public static float INC_GUAGE = 0.0001f;
        public static float MAX_GUAGE = 0.005f;

        public bool isInteractable;

        [SerializeField]
        GameMode gameMode;
        
        private void Start()
        {
            isInteractable = true;

            if (HUDInteractText != null)
            {
                HUDInteractText.gameObject.SetActive(false);
            }
        }

        public void StartInteraction()
        {
            if (curGuage < MAX_GUAGE)
            {
                curGuage += INC_GUAGE;
                UpdateGaugageColor();
            }
            else if (curGuage >= MAX_GUAGE)
            {
                ResetGuage();
                Photon.Pun.PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "gm", gameMode } });
            }
        }

        public void ResetGuage()
        {
            curGuage = 0.0f;
            UpdateGaugageColor();
        }

        public void UpdateGaugageColor()
        {
            MeshRenderer guageMeshRenderer = ObjectGuage.GetComponent<MeshRenderer>();
            if (guageMeshRenderer != null)
            {
                guageMeshRenderer.material.SetColor("_Color", new Color(255, 200, 0, curGuage));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && isInteractable)
            {
                if (other.GetComponent<PhotonView>().IsMine)
                {
                    MeshRenderer myMeshRenderer = GetComponent<MeshRenderer>();
                    myMeshRenderer.material.color = Color.red;

                    HUDInteractText.gameObject.SetActive(true);

                    other.GetComponentInChildren<SandboxPawn>().MySandboxCharacterInteraction.updateInteractableObject(this);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && isInteractable)
            {
                if (other.GetComponent<PhotonView>().IsMine)
                {
                    MeshRenderer myMeshRenderer = GetComponent<MeshRenderer>();
                    myMeshRenderer.material.color = Color.white;

                    HUDInteractText.gameObject.SetActive(false);

                    other.GetComponentInChildren<SandboxPawn>().MySandboxCharacterInteraction.updateInteractableObject(null);
                    ResetGuage();
                }
            }
        }

    }
}