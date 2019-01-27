using Home.Core;
using UnityEngine;

namespace Home.Sandbox
{
    public class SandboxPawn : Pawn
    {
        public SandboxCharacterInteraction MySandboxCharacterInteraction {
            get; private set;
        }

        SandboxCharacterMovement mySandboxCharacterMovement;

        public override void Initialize(PlayerController myPlayerController)
        {
            this.myPlayerController = myPlayerController;
            mySandboxCharacterMovement = new SandboxCharacterMovement(transform, Camera.main, new Vector3(0,0,0), this.GetComponent<Rigidbody>());
            MySandboxCharacterInteraction = new SandboxCharacterInteraction();

            SandboxCharacterCamera mySandBoxCamera = new SandboxCharacterCamera(Camera.main, transform, 10);
            
            UpdateActions += mySandboxCharacterMovement.Update;
            FixedUpdateActions += mySandboxCharacterMovement.FixedUpdate;

            UpdateActions += MySandboxCharacterInteraction.Update;

            UpdateActions += mySandBoxCamera.Update;
            LateUpdateActions += mySandBoxCamera.LateUpdate;

            OnCollisionEnterActions += mySandboxCharacterMovement.OnCollisionEnter;
            OnCollisionExitActions += mySandboxCharacterMovement.OnCollisionExit;
        }

        public override void InitializeRemote()
        {
            // Nothing shared required
        }
    }
}
