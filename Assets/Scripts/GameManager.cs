using Home.Sandbox;
using Home.UI;
using Home.HideAndSeek;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

namespace Home.Core
{
    public static class GameManager
    {
        public static NetworkManager NetworkManager { get; private set; }
        public static PlayerController LocalPlayer { get; set; }
        public static GameMode GameMode { get; set; } = GameMode.Sandbox;
        public static GameModePopup GameModePopup { get; set; }
        public static HUD Hud { get; set; }
        public static List<int> PlayerControllerViewIds { get; } = new List<int>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            NetworkManager = new NetworkManager();
        }

        public static void UpdateGameMode(GameMode gameMode)
        {
            GameMode = gameMode;
            SandboxObjectInteraction[] interactionObjects = (SandboxObjectInteraction[])Resources.FindObjectsOfTypeAll(typeof(SandboxObjectInteraction));
            bool isSandbox = GameMode == GameMode.Sandbox;
            foreach (SandboxObjectInteraction interactionObject in interactionObjects)
            {
                interactionObject.gameObject.SetActive(isSandbox);
            }

            if (!isSandbox)
            {
                GameModePopup?.Display(GameMode);
            }
            else
            {
                SetupGameMode();
            }
        }

        public static void SetupGameMode()
        {
            // Set all pawn modes
            foreach (int viewId in PlayerControllerViewIds)
            {
                PhotonNetwork.GetPhotonView(viewId).GetComponent<PlayerController>().SetGameModePawn(GameMode);
            }
            // Set mode specific state
            if (GameMode == GameMode.Hns)
            {
                foreach (int viewId in PlayerControllerViewIds)
                {
                    PhotonView view = PhotonNetwork.GetPhotonView(viewId);
                    ((HideAndSeekPawn)view.GetComponent<PlayerController>().ControlledPawn).IsSeeker = false;
                }
                if (PhotonNetwork.IsMasterClient)
                {
                    LocalPlayer.TimerCanvas.Initialize(300, SetToSandbox);
                    ((HideAndSeekPawn)LocalPlayer.ControlledPawn).PickSeeker();
                }
                else
                {
                    LocalPlayer.TimerCanvas.Initialize(300);
                }
            }
            else if (GameMode == GameMode.Fps)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    LocalPlayer.TimerCanvas.Initialize(300, SetToSandbox);
                }
                else
                {
                    LocalPlayer.TimerCanvas.Initialize(300);
                }
            }
        }

        private static void SetToSandbox()
        {
            UpdateGameMode(GameMode.Sandbox);
        }
    }
}
