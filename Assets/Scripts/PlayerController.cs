using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Home.Core
{
    public class PlayerController : MonoBehaviourPun, IPunInstantiateMagicCallback
    {
        [SerializeField] private List<Pawn> myPawns;
        [SerializeField] private List<string> registeredAxes;
        public Pawn ControlledPawn { get; private set; }
        private Dictionary<string, bool> lastFrame = new Dictionary<string, bool>();
        private Dictionary<string, bool> currentFrame = new Dictionary<string, bool>();

        private void Awake()
        {
            foreach (string axis in registeredAxes)
            {
                lastFrame.Add(axis, false);
                currentFrame.Add(axis, false);
            }
        }

        public void AttachPawn(Pawn pawn)
        {
            if (ControlledPawn != null)
            {
                ControlledPawn.Detach();
                pawn.transform.position = ControlledPawn.transform.position;
                pawn.transform.rotation = ControlledPawn.transform.rotation;
            }
            ControlledPawn = pawn;
            pawn.Attach(this);
        }

        private void Update()
        {
            UpdateAxesState();
        }

        private void UpdateAxesState()
        {
            Dictionary<string, bool> temp = lastFrame;
            lastFrame = currentFrame;
            currentFrame = temp;

            foreach (string axis in registeredAxes)
            {
                currentFrame[axis] = Input.GetAxis(axis) != 0;
            }
        }

        public bool GetAxisPressed(string axisName)
        {
            return !lastFrame[axisName] && currentFrame[axisName];
        }

        public bool GetAxisReleased(string axisName)
        {
            return lastFrame[axisName] && !currentFrame[axisName];
        }

        public void SetGameModePawn(GameMode gameMode)
        {
            AttachPawn(myPawns[(int)gameMode]);
        }

        public void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            int score = (int)info.Sender.CustomProperties["score"];
            GameManager.Hud.AddPlayer(info.Sender.UserId, info.Sender.NickName, score);
            if (info.photonView.IsMine)
            {
                foreach (Pawn pawn in myPawns)
                {
                    pawn.Initialize(this);
                }
                
                info.Sender.SetCustomProperties(new ExitGames.Client.Photon.Hashtable {
                    { "id", info.photonView.ViewID }
                });
            }
            else
            {
                foreach (Pawn pawn in myPawns)
                {
                    pawn.InitializeRemote();
                }
            }
            SetGameModePawn(GameManager.GameMode);
            GameManager.PlayerControllerViewIds.Add(info.photonView.ViewID);
        }
    }
}
