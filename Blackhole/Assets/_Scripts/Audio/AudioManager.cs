using System;
using UnityEngine;

namespace Sound
{
    public class AudioManager : MonoBehaviour
    {
        /// <summary>
        /// audio manager is here just to make sure our game music doesn't stop through levels
        /// opted to use an audio manager because the game is small and the sounds can be easily
        /// be customizeable in one area
        /// </summary>

        public static AudioManager Instance { get; private set; }

        [SerializeField] private Audio[] _audios;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
            DontDestroyOnLoad(gameObject);

            foreach (Audio a in _audios)
            {
                a.source = gameObject.AddComponent<AudioSource>();
                a.source.clip = a.clip;

                a.source.volume = a.volume;
                a.source.pitch = a.pitch;
                a.source.loop = a.loop;
            }
        }

        private void Start()
        {
            Play("BGM");
        }

        public void Play(string name)
        {
            Audio a = Array.Find(_audios, audio => audio.name == name);
            if (a == null)
            {
                Debug.LogWarning("Audio: " + name + " not found!");
                return;
            }

            a.source.Play();
        }

        public void Stop(string name)
        {
            Audio a = Array.Find(_audios, audio => audio.name == name);
            if (a == null)
            {
                Debug.LogWarning("Audio: " + name + " not found!");
                return;
            }

            a.source.Stop();
        }
    }
}