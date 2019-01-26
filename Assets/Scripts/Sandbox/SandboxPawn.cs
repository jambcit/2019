using Home.Core;
using UnityEngine;

namespace Home.Sandbox
{
    public class SandboxPawn : Pawn
    {
        void Start()
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
