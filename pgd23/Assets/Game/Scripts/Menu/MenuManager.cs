using Game.Scripts.AudioManagement;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Menu
{
    public class MenuManager : MonoBehaviour
    {
        /// <summary>
        ///     This is an important variable that sets when the level scenes start.
        /// </summary>
        [SerializeField] private int _firstLevelBuildIndex;

        [SerializeField] private string songToPlay;

        /// <summary>
        ///     Loads scene by buildIndex. Be careful, because this can change during development.
        ///     This one only loads when you use a mission.
        /// </summary>
        /// <returns></returns>
        public void LoadMission(LevelSelector levelSelector)
        {
            if (levelSelector.Locked) return;
            
            AudioShit();
            LoadScene(levelSelector.LevelId);
            
            var deathAnalytics = Analytics.CustomEvent("Level Loads");
            
            Debug.Log(deathAnalytics);
        }

        /// <summary>
        ///     Adds audio to the game for when a player clicks a button to load a level
        /// </summary>
        private void AudioShit()
        {
            //stops song from playing and ensures that correct song plays 
            AudioHandler.Instance.Stop(AudioHandler.Instance.GetCurrentSong().name);
            AudioHandler.Instance.Play(songToPlay);
        }

        /// <summary>
        ///     Loads scene by buildIndex. Be careful, because this can change during development
        /// </summary>
        /// <returns></returns>
        public static void LoadScene(int sceneBuildIndex)
        {
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }

        /// <summary>
        ///     Gets the current saved data to see if the player has completed any levels. The _firstLevelBuildIndex is
        ///     added to this.
        /// </summary>
        /// <returns></returns>
        public void ResumeLevel()
        {
            int i = _firstLevelBuildIndex;
            while (PlayerPrefs.HasKey("completed-" + i))
            {
                i++;
            }

            if (i > SceneManager.sceneCountInBuildSettings)
            {
                i = _firstLevelBuildIndex;
            }

            LoadScene(i);
        }
    }
}