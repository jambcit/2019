using UnityEngine;

namespace Home.Fps
{
    public class FpsCameraComponent
    {
        private Camera myCamera;
        private Transform myTarget;
        private float rotationSpeed;

        private float cameraX;
        private float cameraY;
        
        public FpsCameraComponent(Camera myCamera, Transform myTarget, float rotationSpeed = 1)
        {
            this.myCamera = myCamera;
            this.myTarget = myTarget;
            this.rotationSpeed = rotationSpeed;
        }

        public void Update()
        {
            cameraX += Input.GetAxis("Mouse X");
            cameraY -= Input.GetAxis("Mouse Y");
            myCamera.transform.rotation = Quaternion.Euler(cameraY, cameraX, 0);
            myCamera.transform.position = myTarget.position;
            myTarget.rotation = Quaternion.Euler(cameraY, cameraX, 0);
        }
    }
}
