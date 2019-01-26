namespace MyNamespace
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Dart : MonoBehaviour
    {

        private const float DART_VELOCITY = 30f;

        private Rigidbody rigidBody;
        private bool isStuck;

        // Start is called before the first frame update
        void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            rigidBody.AddForce(transform.up * DART_VELOCITY, ForceMode.Impulse);
            isStuck = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isStuck)
            {
                transform.LookAt(transform.position - Vector3.Cross(rigidBody.velocity, transform.right));
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collided");
        }

        private void OnTriggerEnter(Collider other)
        {

        }


    } 
}
