using UnityEngine;

namespace Home.Fps
{
    public class Dart : MonoBehaviour
    {

        [SerializeField] private float initialVelocity = 30f;
        private Rigidbody rigidBody;
        private bool isStuck;

        public void Shoot()
        {
            rigidBody = GetComponent<Rigidbody>();
            rigidBody.velocity = Vector3.zero;
            rigidBody.AddForce(transform.forward * initialVelocity, ForceMode.Impulse);
            isStuck = false;
        }
        
        private void Update()
        {
            if (!isStuck)
            {
                transform.LookAt(transform.position + rigidBody.velocity);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            isStuck = true;
        }
    }
}
