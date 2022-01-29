using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

namespace Game.Scripts.Core_LevelManagement.EventManagement
{
    public static class UnityAnalyticsManager
    {
        private static int _deaths;
        private static int _amountOfAbilities;
        private static int _amountLevels;
        private static int _notes;
        private static bool _isPlayingSong;
        private static bool _levelLoaded;
        private static string _level;

        public static void PickedUpAbility()
        {
            _amountOfAbilities++;
        }

        /// <summary>
        ///     Adds amount of notes picked up 
        /// </summary>
        public static void PickedUpNote()
        {
            _notes++;
        }

        /// <summary>
        ///     Adds amount of player deaths 
        /// </summary>
        public static void PlayerDied()
        {
            _deaths++;
        }

        /// <summary>
        ///     Check which level is currently being played 
        /// </summary>
        /// <param name="level"></param>
        public static void SetCurrentLevel(string level)
        {
            _level = level;
        }

        /// <summary>
        ///     Checks if a song is currently playing 
        /// </summary>
        public static void SongPlaying()
        {
            _isPlayingSong = true;
        }

        /// <summary>
        ///     Checks if a level loads from level selector 
        /// </summary>
        public static void LevelLoaded()
        {
            _levelLoaded = true;
        }

        /// <summary>
        ///     Saves all data on level complete 
        /// </summary>
        public static void LevelComplete()
        {
            _amountLevels++;

            var analyticsData = new Dictionary<string, object>
            {
                { "Level", _level },
                { "Deaths", _deaths },
                { "Notes", _notes },
                { "Abilities", _amountOfAbilities }
            };

            var aa = Analytics.CustomEvent("Level Complete", analyticsData);

            Debug.Log($"Level complete data: {aa}");

            LevelLoadChecks();
            Resets();
        }

        private static void Resets()
        {
            _deaths = 0;
            _notes = 0;
            _amountOfAbilities = 0;
            _isPlayingSong = false;
            _levelLoaded = false;
        }

        /// <summary>
        ///     Method used to check if a song is currently playing or not 
        /// </summary>
        private static void LevelLoadChecks()
        {
            var songPlaying = Analytics.CustomEvent("AudioCheck", new Dictionary<string, object>
            {
                { "Audio Check", _isPlayingSong },
                { "Level Loads", _levelLoaded }
            });
            
            Debug.Log($"Sound playing data: {songPlaying}");
        }

        public static void LevelUnlocked()
        {
            var levelUnlocks = Analytics.CustomEvent("Level Unlocks");
            
            Debug.Log($"Level unlocked data: {levelUnlocks}");
        }


        /// <summary>
        ///     Custom event that saves the total amount of completed levels when a player quits the game
        /// </summary>
        public static void SavesNumberOfLevels()
        {
            var levelsCompleted = Analytics.CustomEvent("Levels Completed",
                new Dictionary<string, object>
                {
                    {
                        "Levels Completed", _amountLevels
                    }
                });
            
            Debug.Log($"Number of levels completed data: {levelsCompleted}");
        }
    }
}