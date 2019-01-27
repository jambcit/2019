using UnityEngine;

namespace Home.Core
{
    public delegate void StartActionDelegate();
    public delegate void UpdateActionDelegate();
    public delegate void LateUpdateActionDelegate();
    public delegate void FixedUpdateActionDelegate();
    public class Pawn : MonoBehaviour
    {
        protected StartActionDelegate StartActions;
        protected UpdateActionDelegate UpdateActions;
        protected LateUpdateActionDelegate LateUpdateActions;
        protected FixedUpdateActionDelegate FixedUpdateActions;
        private PlayerController controller;
        private bool inputEnabled = false;

        private void Start()
        {
            if (StartActions != null)
            {
                StartActions.Invoke();
            }
        }

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

        public void Attach(PlayerController controller)
        {
            this.controller = controller;
            this.inputEnabled = true;
        }

        public void Detach()
        {
            this.controller = null;
            this.inputEnabled = false;
        }
    }
}
