using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Tools;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

namespace Game.Scripts.AudioManagement
{
    public class AudioHandler : Singleton<AudioHandler>
    {
        private const string ErrorMessage = "This song does not exist or cannot be found!";

        public Sound[] sounds;

        private Dictionary<string, Sound> _soundDict;

        #region Unity_Event_Functions

        protected override void Awake()
        {
            base.Awake();

            _soundDict = new Dictionary<string, Sound>();

            //loops through each sound in the sound array 
            foreach (var sound in sounds)
            {
                //each sound needs to contain an audio source component!
                sound.source = gameObject.AddComponent<AudioSource>();

                //all sound variables 
                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;
                sound.source.outputAudioMixerGroup = sound.group;

                _soundDict.Add(sound.name, sound);
            }
        }

        #endregion

        #region Sound_Functions

        /// <summary>
        ///     Plays a sound
        /// </summary>
        /// <param name="soundName"> name of the sound to play </param>
        public void Play(string soundName)
        {
            StopAll();

            if (!_soundDict.TryGetValue(soundName, out var sound))
            {
                return;
            }

            //plays the sound that has the name given as parameter 
            sound.source.Play();
        }

        /// <summary>
        ///     Gets the current song that is playing in the game
        /// </summary>
        /// <returns> current playing song </returns>
        public Sound GetCurrentSong()
        {
            return sounds.FirstOrDefault(sound => sound.source.isPlaying);
        }

        /// <summary>
        ///     Stops playing a song
        /// </summary>
        /// <param name="soundName"> name of the song to stop </param>
        public void Stop(string soundName)
        {
            if (!_soundDict.TryGetValue(soundName, out var sound))
            {
                Debug.Log(ErrorMessage);
                return;
            }

            //stops the song that has the same name given as a parameter 
            sound.source.Stop();
        }

        /// <summary>
        ///     Stops playing each song in the current dictionary
        /// </summary>
        private void StopAll()
        {
            foreach (var soundName in _soundDict) soundName.Value.source.Stop();
        }

        #endregion

        private void OnApplicationQuit()
        {
            var deathAnalytics = Analytics.CustomEvent("Running Game Time", 
                new Dictionary<string, object> {
                    {
                        "Running Time", Time.realtimeSinceStartup
                    }
                });
            
            Debug.Log(deathAnalytics);
        }
    }
}