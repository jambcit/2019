using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Home.Sandbox
{
    public class SandboxCharacterMovement : SandboxPawn
    {
         float moveSpeed = 6f;            // Player's speed when walking.
        float rotationSpeed = 6f;
        float jumpHeight = 1000f;         // How high Player jumps

        Vector3 relativeCameraPosition;
        Vector3 moveDirection;

        Rigidbody rb;
        float mouseX;
        float mouseY;

        public float horizontal;
        public float vertical;
        public Vector3 inputDir;
        public bool onGround;
        // Using the Awake function to set the references
        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            relativeCameraPosition = new Vector3(0, 0, -10);
        }

        void Update()
        {
            mouseX += Input.GetAxis("Mouse X");
            mouseY += Input.GetAxis("Mouse Y");

            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            mouseY = Mathf.Clamp(mouseY, -60, 60);

            Move();

            if (onGround && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        void FixedUpdate()
        {

        }

        void LateUpdate()
        {

            RotateCam(mouseX, mouseY);
        }

        void Jump()
        {
            rb.AddForce(new Vector3(0.0f, jumpHeight, 0.0f));
        }

        void RotateCam(float h, float v)
        {
            Quaternion newRotation = Quaternion.Euler(-v, h, 0.0f);

            Camera.main.transform.position = transform.position + newRotation * relativeCameraPosition;
            Camera.main.transform.LookAt(transform);
        }
        
        void Move()
        {
            inputDir = Camera.main.transform.right * horizontal + Camera.main.transform.forward * vertical;

            rb.position += inputDir.normalized * moveSpeed * Time.deltaTime;
            //RotateTowardsTo(keyX, keyZ);
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

