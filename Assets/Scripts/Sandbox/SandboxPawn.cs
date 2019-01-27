using Home.Core;
using UnityEngine;

namespace Home.Sandbox
{
    public class SandboxPawn : Pawn
    {
        public override void Initialize()
        {
            UpdateActions += MoveMe;
            // LateUpdateActions += YourLateFunc;
        }

        private void MoveMe()
        {
            transform.Translate(Vector3.forward);
        }
    }
}
