using Home.Core;
using UnityEngine;

namespace Home.HideAndSeek
{
    public class HideAndSeekPawn : Pawn
    {
        HideAndSeekCharacterMovement myHideAndSeekCharacterMovement;

        public override void Initialize()
        {
            myHideAndSeekCharacterMovement = new HideAndSeekCharacterMovement(this.GetComponent<Rigidbody>(), transform);

            UpdateActions += myHideAndSeekCharacterMovement.Update;

            OnTriggerEnterActions += myHideAndSeekCharacterMovement.OnTriggerEnter;
            OnCollisionExitActions += myHideAndSeekCharacterMovement.OnCollisionExit;
        }

        public override void InitializeRemote()
        {
            // Nothing shared required
        }
    }
}
