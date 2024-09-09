using AnhSenPai;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

namespace AnhSenPai.Music
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] musicSounds, sfxSounds;
        public AudioSource musicSource, sfxSource;
        public static AudioManager instance;
    
        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
        public void PlayMusic(string name)
        {
            Sound s = Array.Find(musicSounds, x => x.name == name);
            if (s == null)
            {
                Debug.Log("Not found Sounds");
            }
            else if (s != null)
            {
                musicSource.clip = s.sound;
                musicSource.Play();
               
            }
        }
        private void Start()
        {
            PlayMusic(musicSounds[0].name);
        }
        public void PlaySFX(string name)
        {
            Sound s = Array.Find(sfxSounds, x => x.name == name);
            if (s != null)
            {
                Debug.Log("Not found Sounds");
            }
            else
            {
               sfxSource.PlayOneShot(s.sound);
            }
        }
    }

}
