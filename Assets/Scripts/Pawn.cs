using UnityEngine;

namespace Home.Core
{
    public delegate void UpdateActionDelegate();
    public delegate void LateUpdateActionDelegate();
    public delegate void FixedUpdateActionDelegate();
    public delegate void OnTriggerEnterActionDelegate(Collider other);
    public delegate void OnTriggerExitActionDelegate(Collider other);
    public delegate void OnCollisionEnterActionDelegate(Collision other);
    public delegate void OnCollisionExitActionDelegate(Collision other);

    public abstract class Pawn : MonoBehaviour
    {
        protected UpdateActionDelegate UpdateActions;
        protected LateUpdateActionDelegate LateUpdateActions;
        protected FixedUpdateActionDelegate FixedUpdateActions;
        protected OnTriggerEnterActionDelegate OnTriggerEnterActions;
        protected OnTriggerExitActionDelegate OnTriggerExitActions;
        protected OnCollisionEnterActionDelegate OnCollisionEnterActions;
        protected OnCollisionExitActionDelegate OnCollisionExitActions;
        protected PlayerController myPlayerController;
        private bool inputEnabled = false;

        public abstract void Initialize();

        private void Update()
        {
            if (this.inputEnabled && UpdateActions != null)
            {
                UpdateActions.Invoke();
            }
        }

        private void LateUpdate()
        {
            if (this.inputEnabled && LateUpdateActions != null)
            {
                LateUpdateActions.Invoke();
            }
        }

        private void FixedUpdate()
        {
            if (this.inputEnabled && FixedUpdateActions != null)
            {
                FixedUpdateActions.Invoke();
            }
        }

        public void Attach(PlayerController myPlayerController)
        {
            this.myPlayerController = myPlayerController;
            this.inputEnabled = true;
        }

        public void Detach()
        {
            this.myPlayerController = null;
            this.inputEnabled = false;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (this.inputEnabled && OnCollisionEnterActions != null)
            {
                OnCollisionEnterActions.Invoke(other);
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (this.inputEnabled && OnCollisionExitActions != null)
            {
                OnCollisionExitActions.Invoke(other);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (this.inputEnabled && OnTriggerEnterActions != null)
            {
                OnTriggerEnterActions.Invoke(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (this.inputEnabled && OnTriggerExitActions != null)
            {
                OnTriggerExitActions.Invoke(other);
            }
        }
    }
}
