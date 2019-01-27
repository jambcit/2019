using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Home.Sandbox
{
    public class SandboxCharacterMovement : SandboxPawn
    {
        //TODO definition of character id?

        float moveSpeed = 6f;            // Player's speed when walking.
        float rotationSpeed = 6f;
        float jumpHeight = 1000f;         // How high Player jumps

        [Range(0, Mathf.Infinity)] public float cameraDistance = 10;
        private Vector3 cameraStart;
        Vector3 moveDirection;
        public Vector3 lookAtValue;

        Rigidbody rb;
        public float mouseX;
        public float mouseY;

        public float horizontal;
        public float vertical;
        public Vector3 inputDir;
        public bool onGround;

        private Camera myCam;
        // Using the Awake function to set the references
        void Awake()
        {
            myCam = Camera.main;
            rb = GetComponent<Rigidbody>();
            cameraStart = new Vector3(0, 0, -cameraDistance);
        }

        void Update()
        {
            mouseX += Input.GetAxis("Mouse X");
            mouseY -= Input.GetAxis("Mouse Y");
            mouseY = Mathf.Clamp(mouseY, -60, 60);

            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            
        }

        void FixedUpdate()
        {
            Move();
            if (onGround && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        void LateUpdate()
        {
            RotateCamera();
        }
        
        void Move()
        {
            Vector3 forward = new Vector3(transform.position.x - myCam.transform.position.x, 0, transform.position.z - myCam.transform.position.z).normalized;
            Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;

            inputDir = (right * horizontal) + (forward * vertical);
            Vector3 lookAtTarget = transform.position + inputDir;

            if (inputDir != Vector3.zero)
            {
                transform.LookAt(lookAtTarget);
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
        }

        void Jump()
        {
            rb.AddForce(new Vector3(0.0f, jumpHeight, 0.0f));
        }

        void RotateCamera()
        {
            Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0.0f);

            myCam.transform.position = transform.position + rotation * cameraStart;
            myCam.transform.LookAt(transform);
        }


        private void OnCollisionEnter(Collision collision)
        {
            onGround = collision.gameObject.name.Equals("Ground");
        }

        private void OnCollisionExit(Collision collision)
        {
            onGround = !collision.gameObject.name.Equals("Ground");
        }
    }


}

