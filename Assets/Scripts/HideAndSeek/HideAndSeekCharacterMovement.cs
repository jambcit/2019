using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Home.HideAndSeek
{
    public class HideAndSeekCharacterMovement
    {

        private Rigidbody rb;
        private Transform transform;

        public const string player_tag = "Player";

        public static float MOVEMENT_SPEED = 6f;
        public static float ROTATION_SPEED = 1f;

        //Pitch
        [Range(0, 90)]
        public static float PITCH_MAX = 15f;
        public static float PITCH_RATE = 3f;
        private float pitchCurrent = 0f;
        private bool isPitchingForward = false;
        private bool isPitchingHorizontal = false;
        //Boost
        public static float BOOST_MAX_FUEL = 50f;
        public static float BOOST_MULTIPLIER = 3f;
        public static float BOOST_REFUEL_RATE = .1f;
        public static float BOOST_USE_RATE = .1f;
        private float boostFuel;

        //Boost Reset
        public static float BOOST_RESET_TIME_LENGTH = 10f;
        public static float BOOST_RESET_TIMER_RATE = .1f;
        private float boostResetTimer;
        private bool isBoostReseting = false;

        //Overheat
        public static float OVERHEAT_TIME_LENGTH = 10f;
        public static float OVERHEAT_COOLING_RATE = .1f;

        //Mouse
        public static float MOUSE_DEAD_ZONE = 35f;
        public static float MOUSE_SENSITIVITY = 0.008f;
        private float overheatTimer;
        // True if isOverheating because of over use of Boost
        private bool isOverheating = false;

        //It
        //private bool isIt;
        public HideAndSeekCharacterMovement(
            Rigidbody rb
            , Transform transform)
        {
            //Components
            this.rb = rb;
            this.transform = transform;
            //Pitch
            pitchCurrent = 0f;
            isPitchingForward = false;
            isPitchingHorizontal = false;
            //Boost
            boostFuel = BOOST_MAX_FUEL;
            //Boost Reset
            boostResetTimer = BOOST_RESET_TIME_LENGTH;
            isBoostReseting = false;
            // Overheat
            overheatTimer = OVERHEAT_TIME_LENGTH;
            // True if isOverheating because of over use of Boost
            isOverheating = false;
        }

        public void Update()
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
            Move(movement);

            // Rotation
            float mouseX2 = Input.mousePosition.x - (Screen.width / 2.0f);
            Vector3 direction = Vector3.zero;
            if (MOUSE_DEAD_ZONE < Mathf.Abs(mouseX2))
                direction = new Vector3(0f, mouseX2 * MOUSE_SENSITIVITY, 0f);
            Rotate(direction);

            // Boost
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Boost();
            }

            //Reset Rotation
            if (Input.GetKey(KeyCode.Q))
            {
                transform.rotation = Quaternion.identity;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            if (BOOST_RESET_TIME_LENGTH == boostResetTimer && boostFuel < BOOST_MAX_FUEL)
            {
                boostFuel += .1f;
                if (boostFuel > BOOST_MAX_FUEL)
                    boostFuel = BOOST_MAX_FUEL;
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

                movement = movement * Time.deltaTime * MOVEMENT_SPEED;

                // Seperates horizontal and side to side movement from pitch and roll
                Vector3 forward = transform.forward;
                forward.y = 0;
                Vector3 right = transform.right;
                right.y = 0;
                //moves drone relative to drone's rotation
                rb.AddForce((right * movement.x + transform.up * movement.y + forward * movement.z).normalized / 10 * MOVEMENT_SPEED, ForceMode.Impulse);
                //transform.position += right * movement.x + transform.up * movement.y + forward * movement.z;

            }
        }

        private void PitchForward(float pitch)
        {
            //Vector3 rotationY = new Vector3(0, transform.rotation.eulerAngles.y, 0);
            //Debug.Log("Pitch Euler " + transform.rotation.eulerAngles.x);
            isPitchingForward = true;
            if ((transform.rotation.eulerAngles.x <= PITCH_MAX && pitch > 0)
                || (transform.rotation.eulerAngles.x > 0 && transform.rotation.eulerAngles.x < 91 && pitch < 0)
                || (transform.rotation.eulerAngles.x >= 360 - PITCH_MAX && pitch < 0)
                || (transform.rotation.eulerAngles.x >= 269 - PITCH_MAX && pitch > 0))
            {

                //Debug.Log("Pitch " + pitch);
                transform.rotation = transform.rotation * Quaternion.Euler(pitch * PITCH_RATE, 0, 0);
            }
            //Debug.Log(Quaternion.Euler(movement.z * 5f, 0, 0));
            //Debug.Log("Transform Rotation " + transform.rotation);
        }

        private void PitchForwardDampen()
        {

            //Vector3 rotationY = new Vector3(0, transform.rotation.eulerAngles.y, 0);
            //Debug.Log("Pitch Euler " + transform.rotation.eulerAngles.x);
            //Debug.Log("IsPitchingForward " + isPitchingForward);
            if (transform.rotation.eulerAngles.x < 1 || transform.rotation.eulerAngles.x > 359)
            {

            }
            else if (transform.rotation.eulerAngles.x >= 1 && transform.rotation.eulerAngles.x < 91)
            {
                transform.rotation = transform.rotation * Quaternion.Euler(-1f * PITCH_RATE / 2, 0, 0);
            }
            else if
               (transform.rotation.eulerAngles.x > 269 && transform.rotation.eulerAngles.x <= 359)
            {

                //Debug.Log("Pitch " + pitch);
                transform.rotation = transform.rotation * Quaternion.Euler(1f * PITCH_RATE / 2, 0, 0);
            }
        }

        private void PitchHorizontal(float pitch)
        {
            isPitchingHorizontal = true;
            if ((transform.rotation.eulerAngles.z <= PITCH_MAX && -pitch > 0)
                || (transform.rotation.eulerAngles.z > 0 && transform.rotation.eulerAngles.z < 91 && -pitch < 0)
                || (transform.rotation.eulerAngles.z >= 360 - PITCH_MAX && -pitch < 0)
                || (transform.rotation.eulerAngles.z >= 269 - PITCH_MAX && -pitch > 0))
            {

                transform.rotation = transform.rotation * Quaternion.Euler(0, 0, -pitch * PITCH_RATE);
                //Debug.Log("PitchHorizontal " + transform.rotation.eulerAngles.z);
            }
        }

        private void PitchHorizontalDampen()
        {
            //Debug.Log("PitchHorizontalDampen " + transform.rotation.eulerAngles.z);
            if (transform.rotation.eulerAngles.z < 1 || transform.rotation.eulerAngles.z > 359)
            {

            }
            else if (transform.rotation.eulerAngles.z >= 1 && transform.rotation.eulerAngles.z < 91)
            {
                transform.rotation = transform.rotation * Quaternion.Euler(0, 0, -1f * PITCH_RATE / 2);
            }
            else if
               (transform.rotation.eulerAngles.z > 269 && transform.rotation.eulerAngles.z <= 359)
            {

                //Debug.Log("Pitch " + pitch);
                transform.rotation = transform.rotation * Quaternion.Euler(0, 0, +1f * PITCH_RATE / 2);
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
                rb.AddForce(transform.forward * BOOST_MULTIPLIER, ForceMode.Impulse);
                boostFuel -= BOOST_USE_RATE;
                isBoostReseting = true;
                boostResetTimer = BOOST_RESET_TIME_LENGTH;
            }
            else
            {
                isOverheating = true;
                //Debug.Log(this.name + " is overheating");
            }
        }

        private void BoostReset()
        {
            boostResetTimer -= BOOST_RESET_TIMER_RATE;
            if (boostResetTimer <= 0)
            {
                boostResetTimer = BOOST_RESET_TIME_LENGTH;
                isBoostReseting = false;

                //Debug.Log(this.name + "'s boost reset timer has ended");
            }
        }

        private void Overheat()
        {
            overheatTimer -= OVERHEAT_COOLING_RATE;
            //Debug.Log(overheatTimer);
            if (overheatTimer <= 0)
            {
                overheatTimer = OVERHEAT_TIME_LENGTH;
                isOverheating = false;
                //boostFuel = boostMaxFuel;
                //Debug.Log(this.name + " has cooled off");
            }
        }

        public float GetBoostMaxFuel()
        {
            return BOOST_MAX_FUEL;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(player_tag)
                // && isIt
                )
            {
                Debug.Log("Found " + other.gameObject.name);
                //other.gameObject.GetComponent<DroneRole>().isIt = true;
                //++tagCount;
                //Destroy(other.gameObject);
                //collision.gameObject.GetComponent<PhotonView>().RPC("Tag", RpcTarget.All, collision.gameObject.name);
                //PhotonView photonView = PhotonView.Get(this);
                //photonView.RPC("Tag", RpcTarget.All, other.name);
            }
        }

        public void OnCollisionExit(Collision other)
        {
            rb.angularVelocity = Vector3.zero;
        }

        //void Tag(string name)
        //{
        //    Debug.Log("Found " + name);
        //}


        //// Called by the HideAndSeekGameModeManager GameOver()
        //void PostScore()
        //{
        //    if (isClient)
        //    {

        //    }
        //}

    }
}