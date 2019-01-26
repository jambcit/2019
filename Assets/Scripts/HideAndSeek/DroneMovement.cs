using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Home.HNS
{
    public class DroneMovement : MonoBehaviour
    {
        public Camera cam;

        public float movementSpeed = 1f;
        public float rotationSpeed = 1f;

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
                movement = movement * Time.deltaTime * movementSpeed;

                //moves drone relative to drone's rotation
                transform.position += transform.right * movement.x + transform.up * movement.y + transform.forward * movement.z;
            }
        }

        public void Rotate(Vector3 rotation)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation * rotationSpeed));
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