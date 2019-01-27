using Home.Core;
using UnityEngine;

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

        public override void Initialize()
        {
            Camera myCamera = Camera.main;
            Rigidbody myBody = GetComponent<Rigidbody>();
            FpsCameraComponent cameraComponent = new FpsCameraComponent(myCamera, transform, rotationSpeed);
            UpdateActions += cameraComponent.Update;

            FpsMoveComponent moveComponent = new FpsMoveComponent(myCamera, myBody, moveSpeed);
            UpdateActions += moveComponent.Update;
            FixedUpdateActions += moveComponent.FixedUpdate;

            DartPool dartPool = new DartPool(dartPrefab, dartCount);
            dartPool.Start();

            FpsGunComponent gunComponent = new FpsGunComponent(myPlayerController, dartPool, dartSpawn);
            UpdateActions += gunComponent.Update;
        }
    }
}
