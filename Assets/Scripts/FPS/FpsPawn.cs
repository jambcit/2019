using Home.Core;
using UnityEngine;
using Photon.Pun;

namespace Home.Fps
{
    public class FpsPawn : Pawn
    {
        [Header("Darts")]
        [SerializeField] private GameObject dartPrefab;
        [Range(0, 999)] [SerializeField] private int dartCount = 20;
        [SerializeField] private Transform dartSpawn;
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 6;
        [SerializeField] private float rotationSpeed = 1;

        [SerializeField] private DartPool dartPool;

        public override void Initialize()
        {
            Camera myCamera = Camera.main;
            Rigidbody myBody = GetComponent<Rigidbody>();
            FpsCameraComponent cameraComponent = new FpsCameraComponent(myCamera, transform, rotationSpeed);
            UpdateActions += cameraComponent.Update;

            FpsMoveComponent moveComponent = new FpsMoveComponent(myCamera, myBody, moveSpeed);
            UpdateActions += moveComponent.Update;
            FixedUpdateActions += moveComponent.FixedUpdate;

            InitializeRemote();

            FpsGunComponent gunComponent = new FpsGunComponent(myPlayerController, this, dartPool, dartSpawn);
            UpdateActions += gunComponent.Update;
        }

        public override void InitializeRemote()
        {
            dartPool = new DartPool(dartPrefab, dartCount);
            dartPool.Start();
        }

        [PunRPC]
        public void ShootRpc(Vector3 position, Quaternion rotation)
        {
            dartPool.ShootNextDart(position, rotation);
        }
    }
}
