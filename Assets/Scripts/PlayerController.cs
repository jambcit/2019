using System.Collections.Generic;
using UnityEngine;

namespace Home.Core
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Pawn controlledPawn;
        [SerializeField] private List<string> registeredAxes;
        private Dictionary<string, bool> lastFrame = new Dictionary<string, bool>();
        private Dictionary<string, bool> currentFrame = new Dictionary<string, bool>();

        private void Start()
        {
            if (controlledPawn != null)
            {
                controlledPawn.Attach(this);
            }

            foreach (string axis in registeredAxes)
            {
                lastFrame.Add(axis, false);
                currentFrame.Add(axis, false);
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

        private void Update()
        {
            UpdateAxesState();
        }

        private void UpdateAxesState()
        {
            Dictionary<string, bool> temp = lastFrame;
            lastFrame = currentFrame;
            currentFrame = temp;

            foreach (string axis in registeredAxes)
            {
                currentFrame[axis] = Input.GetAxis(axis) != 0;
            }
        }

        public bool GetAxisPressed(string axisName)
        {
            return !lastFrame[axisName] && currentFrame[axisName];
        }

        public bool GetAxisReleased(string axisName)
        {
            return lastFrame[axisName] && !currentFrame[axisName];
        }
    }
}
