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

        Vector3 relativeCameraPosition;
        Vector3 moveDirection;
        public Vector3 lookAtValue;

        Rigidbody rb;
        float mouseX;
        float mouseY;

        public float horizontal;
        public float vertical;
        public Vector3 inputDir;
        public bool onGround;

        private Camera myCam;
        // Using the Awake function to set the references
        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            relativeCameraPosition = new Vector3(0, 0, -10);
            myCam = Camera.main;
        }

        void Update()
        {
            //Depends on the character id do this or other

            mouseX += Input.GetAxis("Mouse X");
            mouseY += Input.GetAxis("Mouse Y");
            mouseY = Mathf.Clamp(mouseY, -60, 60);

            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            Move();
        }

        void FixedUpdate()
        {
            if(onGround && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        void LateUpdate()
        {
            RotateCam(mouseX, mouseY);
        }
        
        void Move()
        {
            Vector3 fakeForward = new Vector3(transform.position.x - myCam.transform.position.x,
                                            0, transform.position.z - myCam.transform.position.z).normalized;
            Vector3 fakeRight = Vector3.Cross(Vector3.up, fakeForward);

            inputDir = (fakeRight * horizontal) + (fakeForward * vertical);
            lookAtValue = transform.position + inputDir;

            if (inputDir != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(inputDir);

                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }

            Debug.DrawLine(transform.position, transform.position + transform.forward, Color.red);
        }

        void Jump()
        {
            rb.AddForce(new Vector3(0.0f, jumpHeight, 0.0f));
        }

        //void FuckLookAt()
        //{
        //    lookAtValue = transform.position + new Vector3(horizontal*moveSpeed, 0, vertical* moveSpeed);

        //    Debug.DrawLine(transform.position, lookAtValue, Color.red);
        //    transform.LookAt(lookAtValue);
        //}

        void RotateCam(float h, float v)
        {
            Quaternion newRotation = Quaternion.Euler(-v, h, 0.0f);

            Camera.main.transform.position = transform.position + newRotation * relativeCameraPosition;
            Camera.main.transform.LookAt(transform);
        }
        

        void OnCollisionEnter(Collision collision)
        {
            onGround = collision.gameObject.name.Equals("Ground");
        }

        void OnCollisionExit(Collision collision)
        {
            onGround = !collision.gameObject.name.Equals("Ground");
        }
    }


}

