using UnityEngine;

namespace Home.Fps
{
    public class FpsMoveComponent
    {
        private Camera myCamera;
        private Rigidbody myPawnBody;
        private float moveSpeed;

        private float moveX;
        private float moveY;

        public FpsMoveComponent(Camera myCamera, Rigidbody myPawnBody, float moveSpeed = 1)
        {
            this.myCamera = myCamera;
            this.myPawnBody = myPawnBody;
            this.moveSpeed = moveSpeed;
        }

        public void Update()
        {
            moveX = Input.GetAxis("Horizontal");
            moveY = Input.GetAxis("Vertical");
        }
        
        public void FixedUpdate()
        {
            Vector3 forward = new Vector3(myCamera.transform.forward.x, 0, myCamera.transform.forward.z).normalized;
            Vector3 right = Vector3.Cross(Vector3.up, forward);
            Vector3 direction = (forward * moveY) + (right * moveX);
            myPawnBody.MovePosition(myPawnBody.position + (direction.normalized * Time.fixedDeltaTime * moveSpeed));
        }
    }
}
