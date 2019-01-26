using UnityEngine;

namespace Home.Core
{
    public delegate void UpdateActionDelegate();
    public delegate void LateUpdateActionDelegate();
    public class Pawn : MonoBehaviour
    {
        protected UpdateActionDelegate UpdateActions;
        protected LateUpdateActionDelegate LateUpdateActions;
        private PlayerController controller;
        private bool inputEnabled = false;

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
