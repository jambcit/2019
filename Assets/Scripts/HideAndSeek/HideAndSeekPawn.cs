using Home.Core;
using UnityEngine;

namespace Home.HideAndSeek
{
    public class HideAndSeekPawn : Pawn
    {
        HideAndSeekCharacterMovement hideAndSeekCharacterMovement;

        public override void Initialize()
        {
            hideAndSeekCharacterMovement = new HideAndSeekCharacterMovement();
        }
    }
}
