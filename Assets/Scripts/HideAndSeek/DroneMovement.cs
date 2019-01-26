using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Home.HNS
{
    public class DroneMovement : MonoBehaviour
    {
        public float movementSpeed = 1f;
        public float rotationSpeed = 1f;

        //Pitch
        public float pitchMax = 1f;
        public float pitchCurrent = 0f;
        public float pitchRate = 5f;
        public bool isPitchingForward = false;
        //Boost
        public float boostFuel;
        public float boostMaxFuel = 10f;
        public float boostMultiplier = 1f;
        public float boostRefuelRate = .1f;
        public float boostUseRate = .1f;

        //Boost Reset
        public float boostResetTimeLength = 1f;
        public float boostResetTimer;
        public float boostResetTimerRate = 1f;
        public bool isBoostReseting = false;

        //Overheat
        public float overheatTimeLength = 1f;
        public float overheatCoolingRate = 1f;

        private float overheatTimer;
        // True if isOverheating because of over use of Boost
        public bool isOverheating = false;

        private Rigidbody rb;
        public void Start()
        {
            rb = GetComponent<Rigidbody>();
            boostFuel = boostMaxFuel;
            overheatTimer = overheatTimeLength;
            boostResetTimer = boostResetTimeLength;
        }

        public void Update()
        {
            if (boostResetTimeLength == boostResetTimer && boostFuel < boostMaxFuel)
            {
                boostFuel += .1f;
                if (boostFuel > boostMaxFuel)
                    boostFuel = boostMaxFuel;
            }

            if (isBoostReseting)
            {
                BoostReset();
            }

            if (isOverheating)
            {
                Overheat();
            }
        }
        public void Move(Vector3 movement)
        {
            if (!isOverheating)
            {
                PitchForward(movement.z);
                movement = movement * Time.deltaTime * movementSpeed;

                // Seperates horizontal and side to side movement from pitch and roll
                Vector3 forward = transform.forward;
                forward.y = 0;
                Vector3 right = transform.right;
                right.y = 0;
                //moves drone relative to drone's rotation

                transform.position += right * movement.x + transform.up * movement.y + forward * movement.z;
                /*Vector3 rotationY = new Vector3(0, transform.rotation.eulerAngles.y, 0);

                transform.rotation = Quaternion * Quaternion.Euler(movement.z * 5f, 0, 0);
                Debug.Log(Quaternion.Euler(movement.z * 5f, 0, 0));
                Debug.Log("Transform Rotation " + transform.rotation);
                */
            }
        }

        public void PitchForward(float pitch)
        {
            isPitchingForward = true;
            //Vector3 rotationY = new Vector3(0, transform.rotation.eulerAngles.y, 0);
            Debug.Log("Pitch Euler " + transform.rotation.eulerAngles.x);

            if ((transform.rotation.eulerAngles.x <= pitchMax && pitch > 0)
                || (transform.rotation.eulerAngles.x > 0 && transform.rotation.eulerAngles.x < 90 && pitch < 0)
                || (transform.rotation.eulerAngles.x >= 360 - pitchMax && pitch < 0)
                || (transform.rotation.eulerAngles.x >= 270 - pitchMax && pitch > 0))
            {

                //Debug.Log("Pitch " + pitch);
                transform.rotation = transform.rotation * Quaternion.Euler(pitch * pitchRate, 0, 0);
            }
            //Debug.Log(Quaternion.Euler(movement.z * 5f, 0, 0));
            //Debug.Log("Transform Rotation " + transform.rotation);
        }

        public void Rotate(Vector3 rotation)
        {

            //transform.rotation = transform.rotation * Quaternion.Euler(rotation * rotationSpeed);
            transform.RotateAround(transform.position, Vector3.up, rotation.y);

            //transform.RotateAround(transform.position, Vector3.up, rotation.y);
        }

        public void Boost()
        {
            if (boostFuel > 0 && !isOverheating)
            {
                transform.position += transform.forward * boostMultiplier;
                boostFuel -= boostUseRate;
                isBoostReseting = true;
                boostResetTimer = boostResetTimeLength;
            }
            else
            {
                isOverheating = true;
                Debug.Log(this.name + " is overheating");
            }
        }

        private void BoostReset()
        {
            boostResetTimer -= boostResetTimerRate;
            if (boostResetTimer <= 0)
            {
                boostResetTimer = boostResetTimeLength;
                isBoostReseting = false;

                Debug.Log(this.name + "'s boost reset timer has ended");
            }
        }

        private void Overheat()
        {
            overheatTimer -= overheatCoolingRate;
            //Debug.Log(overheatTimer);
            if (overheatTimer <= 0)
            {
                overheatTimer = overheatTimeLength;
                isOverheating = false;
                //boostFuel = boostMaxFuel;
                Debug.Log(this.name + " has cooled off");
            }
        }

        public float GetBoostMaxFuel()
        {
            return boostMaxFuel;
        }
    }
}