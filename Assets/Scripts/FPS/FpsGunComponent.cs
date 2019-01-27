using Home.Core;
using UnityEngine;

namespace Home.Fps
{
    public class FpsGunComponent
    {
        private PlayerController myPlayerController;
        private DartPool dartPool;
        private Transform dartSpawn;

        public FpsGunComponent(PlayerController myPlayerController, DartPool dartPool, Transform dartSpawn)
        {
            this.myPlayerController = myPlayerController;
            this.dartPool = dartPool;
            this.dartSpawn = dartSpawn;
        }

        public void Update()
        {
            if (myPlayerController.GetAxisPressed("Fire1"))
            {
                dartPool.ShootNextDart(dartSpawn.position, dartSpawn.rotation);
            }
        }
    }
}
