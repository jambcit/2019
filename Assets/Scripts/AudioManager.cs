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

                s.source.spatialBlend = s.spatialBlend;
            }
        }

        public void Update()
        {
            //Used for testing audio
            ///*
            if (Input.GetKeyDown(KeyCode.Alpha3)) FindObjectOfType<AudioManager>().Play("DartFire");
            if (Input.GetKeyDown(KeyCode.Alpha4)) FindObjectOfType<AudioManager>().Play("DartLand");
            if (Input.GetKeyDown(KeyCode.Alpha5)) FindObjectOfType<AudioManager>().Play("DroneHover");
            if (Input.GetKeyDown(KeyCode.Alpha6)) FindObjectOfType<AudioManager>().Play("GameLoose");
            if (Input.GetKeyDown(KeyCode.Alpha7)) FindObjectOfType<AudioManager>().Play("GameWin");
            if (Input.GetKeyDown(KeyCode.Alpha8)) FindObjectOfType<AudioManager>().Play("PlayerCrouchWalk");
            if (Input.GetKeyDown(KeyCode.Alpha9)) FindObjectOfType<AudioManager>().Play("PlayerJumpGrunt");
            if (Input.GetKeyDown(KeyCode.Alpha0)) FindObjectOfType<AudioManager>().Play("PlayerJumpLand");
            if (Input.GetKeyDown(KeyCode.Y)) FindObjectOfType<AudioManager>().Play("PlayerRun");
            if (Input.GetKeyDown(KeyCode.U)) FindObjectOfType<AudioManager>().Play("PlayerWalk");
            if (Input.GetKeyDown(KeyCode.I)) FindObjectOfType<AudioManager>().Play("PunchLand");
            if (Input.GetKeyDown(KeyCode.O)) FindObjectOfType<AudioManager>().Play("PunchMiddle");
            if (Input.GetKeyDown(KeyCode.P)) FindObjectOfType<AudioManager>().Play("PlayerInteract");
            //*/
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