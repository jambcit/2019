using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Home.HNS
{
    public class DroneHUD : MonoBehaviour
    {
        public Slider boostSlider;

        private DroneMovement droneMovement;
        // Start is called before the first frame update
        void Start()
        {
            droneMovement = GetComponent<DroneMovement>();
            boostSlider.maxValue = droneMovement.GetBoostMaxFuel();
            boostSlider.value = droneMovement.boostFuel;
        }

        // Update is called once per frame
        void Update()
        {

            boostSlider.value = droneMovement.boostFuel;

        }
    }
}
