using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Home.Sandbox
{
    public class SandboxCharacterMovement
    {
        //TODO definition of character id?
        float moveSpeed;            // Player's speed when walking.
        float rotationSpeed;
        float jumpHeight;         // How high Player jumps
        
        Vector3 moveDirection;
        public Vector3 lookAtValue;

        Rigidbody rb;
        public float horizontal;
        public float vertical;
        public Vector3 inputDir;
        
        private Camera myCam;
        private Transform myTransform;
        
        public bool onGround;

        //start position, not implemented yet
        Vector3 startingPosition;


        public SandboxCharacterMovement(Transform myTransform, Camera myCam, Vector3 startingPosition, Rigidbody rb,
                                        float moveSpeed = 6f, float rotationSpeed = 6f, float jumpHeight = 300f)
        {
            this.myTransform = myTransform;
            this.myCam = myCam;
            this.startingPosition = startingPosition;
            this.moveSpeed = moveSpeed;
            this.rotationSpeed = rotationSpeed;
            this.jumpHeight = jumpHeight;
            this.rb = rb;
        }
        
        public void Update()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }

        public void FixedUpdate()
        {
            Move();
            if (onGround && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        
        void Move()
        {
            Vector3 forward = new Vector3(myTransform.position.x - myCam.transform.position.x, 0, myTransform.position.z - myCam.transform.position.z).normalized;
            Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;

            inputDir = (right * horizontal) + (forward * vertical);
            Vector3 lookAtTarget = myTransform.position + inputDir;

            if (inputDir != Vector3.zero)
            {
                myTransform.LookAt(lookAtTarget);
                myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
            }
        }

        void Jump()
        {
            rb.AddForce(new Vector3(0.0f, jumpHeight, 0.0f));
        }

        public void OnCollisionEnter(Collision other)
        {
            onGround = other.gameObject.CompareTag("Ground");
        }

        public void OnCollisionExit(Collision other)
        {
            onGround = !other.gameObject.CompareTag("Ground");
        }
    }


}

