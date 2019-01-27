using UnityEngine;
using Photon.Pun;
using Home.Core;

namespace Home.Fps
{
    public class Dart : MonoBehaviour
    {

        [SerializeField] private float initialVelocity = 30f;
        private Rigidbody rigidBody;
        private bool isStuck;
        private bool isDead;
        private int numHits;
        private int sourceViewId;

        public void Shoot(int viewId)
        {
            rigidBody = GetComponent<Rigidbody>();
            rigidBody.velocity = Vector3.zero;
            rigidBody.AddForce(transform.forward * initialVelocity, ForceMode.Impulse);
            isStuck = false;
            isDead = false;
            numHits = 0;
            sourceViewId = viewId;
        }
        
        private void Update()
        {
            if (!isStuck)
            {
                transform.LookAt(transform.position + rigidBody.velocity);
            }
            if (!isDead)
            {
                if(rigidBody.velocity.magnitude < 0.5f)
                {
                    isDead = true;
                    Debug.Log("Dart hit " + numHits + " times");
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            isStuck = true;
            if (!isDead)
            {
                if (other.GetComponent<Shrapnel>() != null)
                {
                    numHits++;
                    if (PhotonNetwork.GetPhotonView(sourceViewId).IsMine)
                    {
                        //GameManager.LocalPlayer
                    }
                }
            }
        }
    }
}
