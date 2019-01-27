using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Home.Core;
using UnityEngine.UI;

namespace Home.HNS
{
    public class HideAndSeekGameModeManager : MonoBehaviour
    {
        public Text time;

        public bool isBGMOn;

        public float hideAndSeekTimeLimit = 10f;
        public float hideAndSeekTimer;


        private bool isBGMPlaying = false;

        // Start is called before the first frame update
        void Start()
        {
            hideAndSeekTimer = hideAndSeekTimeLimit;
        }

        // Update is called once per frame
        void Update()
        {
            hideAndSeekTimer -= Time.deltaTime;
            time.text = Mathf.Round(hideAndSeekTimer) + "s";
            if (hideAndSeekTimer <= 0)
            {
                GameOver();
            }
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

        private void BGMOn()
        {
            if (!isBGMPlaying)
            {
                FindObjectOfType<AudioManager>().Play("Test");
            }
        }

        private void GameOver()
        {
            //Debug.Log("GAME OVER!");
            //Debug.Break();
        }
    }
}
