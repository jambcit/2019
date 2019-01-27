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
        [Range(0, 90)]
        public float pitchMax = 1f;
        public float pitchCurrent = 0f;
        public float pitchRate = 5f;
        public bool isPitchingForward = false;
        public bool isPitchingHorizontal = false;
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
            if (!isPitchingForward)
            {
                PitchForwardDampen();
            }
            if (!isPitchingHorizontal)
            {
                PitchHorizontalDampen();
            }
        }
        public void Move(Vector3 movement)
        {
            if (!isOverheating)
            {
                //Debug.Log("Movement Z " + movement.z);
                if (movement.x == 0)
                    isPitchingHorizontal = false;
                else
                    PitchHorizontal(movement.x);
                if (movement.z == 0)
                    isPitchingForward = false;
                else
                    PitchForward(movement.z);

                movement = movement * Time.deltaTime * movementSpeed;

                // Seperates horizontal and side to side movement from pitch and roll
                Vector3 forward = transform.forward;
                forward.y = 0;
                Vector3 right = transform.right;
                right.y = 0;
                //moves drone relative to drone's rotation
                rb.AddForce((right * movement.x + transform.up * movement.y + forward * movement.z).normalized, ForceMode.Impulse);
                //transform.position += right * movement.x + transform.up * movement.y + forward * movement.z;

            }
        }

        public void PitchForward(float pitch)
        {
            //Vector3 rotationY = new Vector3(0, transform.rotation.eulerAngles.y, 0);
            //Debug.Log("Pitch Euler " + transform.rotation.eulerAngles.x);
            isPitchingForward = true;
            if ((transform.rotation.eulerAngles.x <= pitchMax && pitch > 0)
                || (transform.rotation.eulerAngles.x > 0 && transform.rotation.eulerAngles.x < 91 && pitch < 0)
                || (transform.rotation.eulerAngles.x >= 360 - pitchMax && pitch < 0)
                || (transform.rotation.eulerAngles.x >= 269 - pitchMax && pitch > 0))
            {

                //Debug.Log("Pitch " + pitch);
                transform.rotation = transform.rotation * Quaternion.Euler(pitch * pitchRate, 0, 0);
            }
            //Debug.Log(Quaternion.Euler(movement.z * 5f, 0, 0));
            //Debug.Log("Transform Rotation " + transform.rotation);
        }

        public void PitchForwardDampen()
        {

            //Vector3 rotationY = new Vector3(0, transform.rotation.eulerAngles.y, 0);
            //Debug.Log("Pitch Euler " + transform.rotation.eulerAngles.x);
            //Debug.Log("IsPitchingForward " + isPitchingForward);
            if (transform.rotation.eulerAngles.x < 1 || transform.rotation.eulerAngles.x > 359)
            {

            }
            else if (transform.rotation.eulerAngles.x >= 1 && transform.rotation.eulerAngles.x < 91)
            {
                transform.rotation = transform.rotation * Quaternion.Euler(-1f * pitchRate / 2, 0, 0);
            }
            else if
               (transform.rotation.eulerAngles.x > 269 && transform.rotation.eulerAngles.x <= 359)
            {

                //Debug.Log("Pitch " + pitch);
                transform.rotation = transform.rotation * Quaternion.Euler(1f * pitchRate / 2, 0, 0);
            }
        }

        public void PitchHorizontal(float pitch)
        {
            isPitchingHorizontal = true;
            if ((transform.rotation.eulerAngles.z <= pitchMax && -pitch > 0)
                || (transform.rotation.eulerAngles.z > 0 && transform.rotation.eulerAngles.z < 91 && -pitch < 0)
                || (transform.rotation.eulerAngles.z >= 360 - pitchMax && -pitch < 0)
                || (transform.rotation.eulerAngles.z >= 269 - pitchMax && -pitch > 0))
            {

                transform.rotation = transform.rotation * Quaternion.Euler(0, 0, -pitch * pitchRate);
                Debug.Log("PitchHorizontal " + transform.rotation.eulerAngles.z);
            }
        }

        public void PitchHorizontalDampen()
        {
            //Debug.Log("PitchHorizontalDampen " + transform.rotation.eulerAngles.z);
            if (transform.rotation.eulerAngles.z < 1 || transform.rotation.eulerAngles.z > 359)
            {

            }
            else if (transform.rotation.eulerAngles.z >= 1 && transform.rotation.eulerAngles.z < 91)
            {
                transform.rotation = transform.rotation * Quaternion.Euler(0, 0, -1f * pitchRate / 2);
            }
            else if
               (transform.rotation.eulerAngles.z > 269 && transform.rotation.eulerAngles.z <= 359)
            {

                //Debug.Log("Pitch " + pitch);
                transform.rotation = transform.rotation * Quaternion.Euler(0, 0, +1f * pitchRate / 2);
            }
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
                //transform.position += transform.forward * boostMultiplier / 10;
                rb.AddForce(transform.forward * boostMultiplier, ForceMode.Impulse);
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