using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Home.Core
{
    public class AnimationAudioTrigger : MonoBehaviour
    {
        private HashSet<string> audioNames;

        public void Awake()
        {
            audioNames = new HashSet<string>();
        }

        public void TriggerAudio(string audioName)
        {
            Debug.Log(audioName);
            AudioManager.instance.Play(audioName);
        }

        public void StartAudio(string audioName)
        {
            AudioManager.instance.Play(audioName);
            audioNames.Add(audioName);
        }

        public void StopAudio(string audioName)
        {
            AudioManager.instance.Stop(audioName);
        }

        void OnDestroy()
        {
            foreach (string audioName in audioNames)
            {
                StopAudio(audioName);
            }
        }
    }
}
