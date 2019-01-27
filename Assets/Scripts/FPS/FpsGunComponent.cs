using Home.Core;
using UnityEngine;

namespace Home.Fps
{
    public class FpsGunComponent
    {
        private PlayerController myPlayerController;
        private FpsPawn fpsPawn;
        private DartPool dartPool;
        private Transform dartSpawn;

        public FpsGunComponent(PlayerController myPlayerController, FpsPawn fpsPawn, DartPool dartPool, Transform dartSpawn)
        {
            this.myPlayerController = myPlayerController;
            this.fpsPawn = fpsPawn;
            this.dartPool = dartPool;
            this.dartSpawn = dartSpawn;
        }

        public void Update()
        {
            if (myPlayerController.GetAxisPressed("Fire1"))
            {
                //dartPool.ShootNextDart(dartSpawn.position, dartSpawn.rotation);
                fpsPawn.photonView.RPC("ShootRpc", Photon.Pun.RpcTarget.All, dartSpawn.position, dartSpawn.rotation);
            }
        }
    }
}
