using Game.Scripts.Tools;
using UnityEngine;

namespace Game.Scripts.VisualEffects.AudioVisuals
{
    public abstract class AudioSyncer : MonoBehaviour
    {
        protected bool ToTheBeat;
        private float _audioValue;
        private float _previousAudioValue;
        private float _timer;

        #region Public_Fields

        [HelpBox("These settings are used to manipulate the audio visual effect, hover on them for tips",
            HelpBoxMessageType.Info)]
        
        [Tooltip("Which spectrum value will determine a beat behaviour")]
        public float bias;

        [Tooltip("Minimal interval between each beat")]
        public float timeStep;

        [Tooltip("How long does the on beat behaviour actually take")]
        public float timeToBeat;

        [Tooltip("This determines how long the rest time in-between on beat behaviour should be")]
        public float restSmoothTime;

        #endregion

        #region Logic

        /// <summary>
        ///     Determines the on beat behaviour for the game object
        /// </summary>
        protected virtual void OnBeat()
        {
            _timer = 0;
            ToTheBeat = true;
        }

        /// <summary>
        ///     Determines on update behaviour for all audio syncer children 
        /// </summary>
        protected virtual void OnUpdate()
        {
            //assign the previous and current audio values based on the audio spectrum value 
            _previousAudioValue = _audioValue;
            _audioValue = AudioSpectrum.SpectrumValue;

            
            if (_previousAudioValue > bias && _audioValue <= bias)
            {
                if (_timer > timeStep) OnBeat();
            }

            if (_previousAudioValue <= bias && _audioValue > bias)
            {
                if (_timer > timeStep) OnBeat();
            }

            //increases timer for the syncing behaviour 
            _timer += Time.deltaTime;
        }

        private void Update() => OnUpdate();
        
        #endregion
    }
}