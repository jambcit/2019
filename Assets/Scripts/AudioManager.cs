using System;
using System.Collections.Generic;
using UnityEngine;
namespace Home.Core
{
    public class AudioManager : MonoBehaviour
    {
        public string audioName;
        public Sound[] sounds;

        public static AudioManager instance;
        // Start is called before the first frame update
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;

                s.source.loop = s.loop;
            }
        }

        public void Update()
        {
            //Used for testing audio
            if (Input.GetKeyDown(KeyCode.P))
                FindObjectOfType<AudioManager>().Play(audioName);

        }

        public void Play(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            s.source.Play();
        }

        public void Stop(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            s.source.Stop();
        }
    }
}