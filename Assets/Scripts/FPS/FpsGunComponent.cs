using Home.Core;
using UnityEngine;

namespace Home.Fps
{
    public class FpsGunComponent
    {
        private PlayerController myPlayerController;

        public FpsGunComponent(PlayerController myPlayerController)
        {
            this.myPlayerController = myPlayerController;
        }

        public void Update()
        {
            if (myPlayerController.GetAxisPressed("Fire1"))
            {

            }
        }
    }
}
