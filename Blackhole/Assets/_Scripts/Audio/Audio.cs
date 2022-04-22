using UnityEngine;

namespace Sound
{
    [System.Serializable]
    public class Audio
    {
        [Header("Audio")]
        public string name;
        public AudioClip clip;

        [Header("Settings")]
        [Range(0f, 1f)]
        public float volume;
        [Range(0.1f, 3f)]
        public float pitch;
        public bool loop;

        [HideInInspector]
        public AudioSource source;
    }
}