using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Home.HNS
{
    public class HideAndSeekInputManager : MonoBehaviour
    {
        public DroneMovement drone;
        public float mouseDeadZone = 25f;
        public float mouseSensitivity = 1f;
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // Movement
            float movementY = 0f;
            if (Input.GetKey(KeyCode.LeftControl))
            {
                movementY -= 1f;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                movementY += 1f;
            }

            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), movementY, Input.GetAxis("Vertical"));
            drone.Move(movement);

            // Rotation
            float mouseX2 = Input.mousePosition.x - (Screen.width / 2.0f);
            Vector3 direction = Vector3.zero;
            if (mouseDeadZone < Mathf.Abs(mouseX2))
                direction = new Vector3(0f, mouseX2 * mouseSensitivity, 0f);
            drone.Rotate(direction);

            // Boost
            if (Input.GetKey(KeyCode.LeftShift))
            {
                drone.Boost();
            }

            //Reset Rotation
            if (Input.GetKey(KeyCode.Q))
            {
                drone.transform.rotation = Quaternion.identity;
                drone.GetComponent<Rigidbody>().velocity = Vector3.zero;
                drone.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
        }
    }
}
