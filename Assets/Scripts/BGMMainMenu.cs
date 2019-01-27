using Home.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Home.UI
{
    public class BGMMainMenu : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            AudioManager.instance.Play("BGMMainMenu");
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDestroy()
        {
            AudioManager.instance.Stop("BGMMainMenu");
        }

    }
}
