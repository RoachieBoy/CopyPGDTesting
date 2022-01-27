using Game.Scripts.Tools;
using UnityEngine;

namespace Game.Scripts.VisualEffects.AudioVisuals
{
    public class AudioSpectrum : MonoBehaviour
    {
        private float[] _audioSpectrum;

        #region Public_Fields

        [HelpBox("These settings are used to manipulate the audio spectrum data, hover over them for tips",
            HelpBoxMessageType.Info)]
        
        [Tooltip("FFT window types determine the waveform that is collected from an audio sample")]
        [SerializeField] private FFTWindow fFtWindow = FFTWindow.BlackmanHarris;

        [Tooltip("Needs to be a value that is a power of 2")] 
        [SerializeField] private int spectrumValueAmount = 64;

        #endregion

        /// <summary>
        ///     Stores the spectrum data value 
        /// </summary>
        public static float SpectrumValue { get; private set; }

        /// <summary>
        ///     Determines the multiplier for the spectrum data value -- arbitrary value so it can be changed!
        /// </summary>
        private const int SpectrumDataMultiplier = 100;

        #region Unity_Event_Functions 

        private void Start()
        {
            _audioSpectrum = new float[spectrumValueAmount];
        }

        private void Update()
        {
            AudioListener.GetSpectrumData(_audioSpectrum, 0, fFtWindow);

            if (_audioSpectrum != null && _audioSpectrum.Length > 0) SpectrumValue = _audioSpectrum[0] * SpectrumDataMultiplier;
        }

        #endregion
    }
}