using Home.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Home.Core
{
    public class BGMGame : MonoBehaviour
    {

        public string currentBGM;
        // Start is called before the first frame update
        void Start()
        {
            if (currentBGM == null)
            {
                AudioManager.instance.Play("BGMSandbox");
                currentBGM = "BGMSandbox";
            }
            else
            {
                AudioManager.instance.Play(currentBGM);
            }
        }


        private string GetBGMName(GameMode mode)
        {
            switch (mode)
            {
                case GameMode.Sandbox:
                    return "BGMSandbox";
                case GameMode.Fps:
                    return "BGMFps";
                case GameMode.Hns:
                    return "BGMHns";
                default:
                    return "BGMSandbox";
            }
        }

        public void PlayBGMByGameMode(GameMode mode)
        {
            AudioManager.instance.Stop(currentBGM);
            currentBGM = GetBGMName(mode);
            AudioManager.instance.Play(currentBGM);
        }

        void OnDisable()
        {
            AudioManager.instance.Stop(currentBGM);
        }

        void OnDestroy()
        {
            AudioManager.instance.Stop(currentBGM);
        }
    }
}