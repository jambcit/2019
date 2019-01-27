using Home.Core;
using UnityEngine;
using Photon.Pun;

namespace Home.Fps
{
    public class FpsPawn : Pawn
    {
        [Header("General")]
        [SerializeField] private Transform cameraView;
        [Header("Darts")]
        [SerializeField] private GameObject dartPrefab;
        [Range(0, 999)] [SerializeField] private int dartCount = 20;
        [SerializeField] private Transform dartSpawn;
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 6;
        [SerializeField] private float rotationSpeed = 1;
        [SerializeField] private int pointsPerHit = 10;

        private DartPool dartPool;

        public override void Initialize()
        {
            Camera myCamera = Camera.main;
            //myCamera.transform.position = cameraView.position;
            Rigidbody myBody = GetComponent<Rigidbody>();
            FpsCameraComponent cameraComponent = new FpsCameraComponent(myCamera, transform, cameraView, rotationSpeed);
            UpdateActions += cameraComponent.Update;

            FpsMoveComponent moveComponent = new FpsMoveComponent(myCamera, myBody, moveSpeed);
            UpdateActions += moveComponent.Update;
            FixedUpdateActions += moveComponent.FixedUpdate;

            InitializeRemote();

            FpsGunComponent gunComponent = new FpsGunComponent(myPlayerController, this, dartPool, dartSpawn);
            UpdateActions += gunComponent.Update;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }

        public override void InitializeRemote()
        {
            dartPool = new DartPool(dartPrefab, dartCount);
            dartPool.Start();
        }

        [PunRPC]
        public void ShootRpc(int viewId, Vector3 position, Quaternion rotation)
        {
            dartPool.ShootNextDart(viewId, position, rotation);
        }

        public void HitTarget()
        {
            Debug.Log("Increasing users points");
        }
    }
}
