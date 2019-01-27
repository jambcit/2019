using Home.Core;
using UnityEngine;

namespace Home.Sandbox
{
    public class SandboxPawn : Pawn
    {
        SandboxCharacterInteraction mySandboxCharacterInteraction;

        public SandboxCharacterInteraction MySandboxCharacterInteraction {
            get {
                return mySandboxCharacterInteraction;
            }   
        }

        SandboxCharacterMovement mySandboxCharacterMovement;

        public override void Initialize()
        {
            mySandboxCharacterMovement = new SandboxCharacterMovement(transform, Camera.main, new Vector3(0,0,0), this.GetComponent<Rigidbody>());
            mySandboxCharacterInteraction = new SandboxCharacterInteraction();

            SandboxCharacterCamera mySandBoxCamera = new SandboxCharacterCamera(Camera.main, transform, 10);
            
            UpdateActions += mySandboxCharacterMovement.Update;
            FixedUpdateActions += mySandboxCharacterMovement.FixedUpdate;

            UpdateActions += mySandboxCharacterInteraction.Update;

            UpdateActions += mySandBoxCamera.Update;
            LateUpdateActions += mySandBoxCamera.LateUpdate;

            OnCollisionEnterActions += mySandboxCharacterMovement.OnCollisionEnter;
            OnCollisionExitActions += mySandboxCharacterMovement.OnCollisionExit;
        }        

    }
}
