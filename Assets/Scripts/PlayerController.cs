using UnityEngine;

namespace Home.Core
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Pawn controlledPawn;

        private void Start()
        {
            if (controlledPawn != null)
            {
                controlledPawn.Attach(this);
            }
        }

        public void AttachPawn(Pawn pawn)
        {
            if (controlledPawn != null)
            {
                controlledPawn.Detach();
            }
            controlledPawn = pawn;
            pawn.Attach(this);
        }
    }
}
