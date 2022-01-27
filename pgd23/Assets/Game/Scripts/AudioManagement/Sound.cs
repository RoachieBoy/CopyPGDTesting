using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Game.Scripts.AudioManagement
{
    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f,1f)]
        public float volume;
        [Range(0f,3f)]
        public float pitch;
        //should the song play on loop 
        public bool loop;
        //which group should the audio be sorted into 
        public AudioMixerGroup group;
        
        [HideInInspector] public AudioSource source;
    }
}