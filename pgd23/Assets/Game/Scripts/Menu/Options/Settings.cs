using Game.Scripts.Tools;
using Game.Scripts.VisualEffects;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Menu.Options
{
    public class Settings : Singleton<Settings>
    {
        [SerializeField] private AudioMixer audioMixer;

        /// <summary>
        ///     Sets the volume of the game 
        /// </summary>
        /// <param name="volume"></param>
        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("MasterVolume", volume);
        }

        /// <summary>
        ///     Resets the game progress 
        /// </summary>
        public void ResetProgress()
        {
            for (var count = 0; count < 10; count++)
            {
                if (PlayerPrefs.HasKey("completed-" + count))
                {
                    PlayerPrefs.DeleteKey("completed-" + count);
                }
            }

            for (var i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
            
            Time.timeScale = 1f;
            
            SceneManager.LoadScene("StartScreen");
        }

        /// <summary>
        ///     Resets the game progress 
        /// </summary>
        public void ToggleColorBlindMode()
        {
            if (!PlayerPrefs.HasKey("setting-colorblind") || PlayerPrefs.GetInt("setting-colorblind") == 0)
            {
                PlayerPrefs.SetInt("setting-colorblind", 1);
                
                GetComponent<PauseMenu>().ToggleColorBlindUI(true);
            }
            else
            {
                PlayerPrefs.SetInt("setting-colorblind", 0);
                
                GetComponent<PauseMenu>().ToggleColorBlindUI(false);
            }
            
            ColorManager manager = FindObjectOfType<ColorManager>();

            if (manager != null)
            {
                manager.ColorBlind = PlayerPrefs.GetInt("setting-colorblind") != 0;
            }
        }
    }
}