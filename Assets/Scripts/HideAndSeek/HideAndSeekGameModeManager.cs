using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Home.Core;
namespace Home.HNS
{
    public class HideAndSeekGameModeManager : MonoBehaviour
    {
        public bool isBGMOn;

        private bool isBGMPlaying = false;

        // Start is called before the first frame update
        void Start()
        {
            InitGame();
        }

        // Update is called once per frame
        void Update()
        {
            if (isBGMOn)
            {
                BGMOn();
            }
            else
            {
                FindObjectOfType<AudioManager>().Stop("Test");
                isBGMPlaying = false;
            }
        }

        void InitGame()
        {
            Debug.Log("InitGame");
        }

        private void BGMOn()
        {
            if (!isBGMPlaying)
            {
                FindObjectOfType<AudioManager>().Play("Test");
            }
        }
    }
}
