using UnityEngine;

namespace Home.Core
{
    public delegate void UpdateActionDelegate();
    public delegate void LateUpdateActionDelegate();
    public delegate void FixedUpdateActionDelegate();
    public abstract class Pawn : MonoBehaviour
    {
        protected UpdateActionDelegate UpdateActions;
        protected LateUpdateActionDelegate LateUpdateActions;
        protected FixedUpdateActionDelegate FixedUpdateActions;
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
    }
}
