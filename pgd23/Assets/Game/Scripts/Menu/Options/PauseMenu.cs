using Game.Scripts.AudioManagement;
using Game.Scripts.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scripts.Menu.Options
{
    /// <inheritdoc />
    public class PauseMenu : Singleton<PauseMenu>
    {
        [Header("Menus")] [SerializeField] private GameObject pauseMenuUI;
        [SerializeField] private GameObject optionsMenu;
        [SerializeField] private GameObject _colorBlind;

        private const int StartTimeValue = 1, PauseTimeValue = 0;
        private bool _gameIsPaused;

        #region Unity Functions

        private void Start()
        {
            if (!PlayerPrefs.HasKey("setting-colorblind") && PlayerPrefs.GetInt("setting-colorblind") == 0)
            {
                ToggleColorBlindUI(false);
            }
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;

            if (_gameIsPaused) Resume();
            else Pause();
        }

        #endregion

        #region Menu Functionalities

        /// <summary>
        ///     Pauses the game and ensures the time scale becomes 0
        /// </summary>
        private void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = PauseTimeValue;
            _gameIsPaused = true;
        }

        /// <summary>
        ///     Resumes the current level and resets the time value 
        /// </summary>
        private void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = StartTimeValue;
            _gameIsPaused = false;
        }

        /// <summary>
        ///     Activates the options menu UI panel and deactivates the pause menu 
        /// </summary>
        public void Options()
        {
            optionsMenu.SetActive(true);
            pauseMenuUI.SetActive(false);
        }

        /// <summary>
        ///     Returns user to the start screen of the game 
        /// </summary>
        public void ReturnToStart()
        {
            SceneManager.LoadScene("StartScreen");
            AudioHandler.Instance.Stop(AudioHandler.Instance.GetCurrentSong().name);
            Resume();
        }

        /// <summary>
        ///     Exits the game when button is clicked 
        /// </summary>
        public void QuitGame() => Application.Quit();

        public void ToggleColorBlindUI(bool on)
        {
            Image image = _colorBlind.GetComponent<Image>();

            Color color = image.color;
            color.a = (on ? 1f : 0.3f);
            image.color = color;

            Text text = _colorBlind.GetComponentInChildren<Text>();

            color = text.color;
            color.a = (on ? 1f : 0.3f);
            text.color = color;
        }

        #endregion
    }
}