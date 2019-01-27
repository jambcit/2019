using Home.Core;
using Photon.Pun;
using UnityEngine;

namespace Home.HideAndSeek
{
    public class HideAndSeekPawn : Pawn
    {
        HideAndSeekCharacterMovement myHideAndSeekCharacterMovement;
        public Transform myTarget;
        public bool IsSeeker { get; set; }

        public override void Initialize(PlayerController myPlayerController)
        {
            this.myPlayerController = myPlayerController;
            myHideAndSeekCharacterMovement = new HideAndSeekCharacterMovement(this.GetComponent<Rigidbody>(), transform, this);

            UpdateActions += myHideAndSeekCharacterMovement.Update;
            OnTriggerEnterActions += myHideAndSeekCharacterMovement.OnTriggerEnter;
            OnCollisionExitActions += myHideAndSeekCharacterMovement.OnCollisionExit;

            LateUpdateActions += new HideAndSeekCameraComponent(Camera.main, myTarget).LateUpdate;
        }

        public override void InitializeRemote()
        {
            // Nothing shared required
        }

        public void PickSeeker()
        {
            int playerIndex = Mathf.FloorToInt(GameManager.PlayerControllerViewIds.Count * Random.value);
            photonView.RPC("SetSeeker", RpcTarget.All, GameManager.PlayerControllerViewIds[playerIndex]);
        }

        [PunRPC]
        public void SetSeeker(int viewId)
        {
            PhotonView view = PhotonNetwork.GetPhotonView(viewId);
            Pawn controlledPawn = view.GetComponent<PlayerController>().ControlledPawn;
            if (controlledPawn.GetType() == typeof(HideAndSeekPawn)) {
                ((HideAndSeekPawn)controlledPawn).IsSeeker = true;

                if (AreAllSeekers() && PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
                        {
                            { "gm", GameMode.Sandbox }
                        });
                }
            }
        }

        private bool AreAllSeekers()
        {
            bool allSeekers = true;
            foreach (int viewId in GameManager.PlayerControllerViewIds)
            {
                PhotonView photonView = PhotonNetwork.GetPhotonView(viewId);
                if (!((HideAndSeekPawn)photonView.GetComponent<PlayerController>().ControlledPawn).IsSeeker)
                {
                    allSeekers = false;
                    return allSeekers;
                }
            }
            return allSeekers;
        }
    }
}
