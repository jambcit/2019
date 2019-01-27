using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Home.HideAndSeek
{
    public class HideAndSeekCameraComponent
    {
        private Camera myCamera;
        private Transform myTarget;

        public HideAndSeekCameraComponent(Camera myCamera, Transform myTarget)
        {
            this.myCamera = myCamera;
            this.myTarget = myTarget;
        }

        public void LateUpdate()
        {
            myCamera.transform.position = myTarget.position;
            myCamera.transform.rotation = myTarget.rotation;
        }
    }
}
