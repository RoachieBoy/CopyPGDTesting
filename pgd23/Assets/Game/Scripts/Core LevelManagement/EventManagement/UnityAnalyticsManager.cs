using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

namespace Game.Scripts.Core_LevelManagement.EventManagement
{
    public static class UnityAnalyticsManager
    {
        public static int Deaths;
        private static int _amountOfAbilities; 
        private static int _amountLevels;
        private static int _notes;
        
        private static bool _isPlayingSong;
        private static bool _levelLoaded;

        private static string _level;
        
        private static Dictionary<string, int> _obstacleKills = new Dictionary<string, int>();

        public static void KilledPlayer(string name)
        {
            if (_obstacleKills.ContainsKey(name)) _obstacleKills[name]++;
            else _obstacleKills.Add(name, 1);
        }

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
            Deaths++;
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
                {"Level", _level},
                {"Deaths", Deaths},
                {"Notes", _notes},
                {"Abilities", _amountOfAbilities}
            };

            var aa = Analytics.CustomEvent("LevelComplete_", analyticsData);

            var objectDictionary = new Dictionary<string, object>();

            foreach (var obstacleKill in _obstacleKills.Keys) objectDictionary.Add(obstacleKill, _obstacleKills[obstacleKill]);

            //custom event for checking which objects kill a player 
            var killed = Analytics.CustomEvent("Obstacle Kills", objectDictionary);
            
            LevelLoadChecks();
            
            PrintsKillValues();
            
            Resets();
        }

        private static void Resets()
        {
            Deaths = 0;
            _notes = 0;
            _amountOfAbilities = 0; 
            _isPlayingSong = false;
            _levelLoaded = false; 
            _obstacleKills = new Dictionary<string, int>(); 
        }

        /// <summary>
        ///     Method used to check if a song is currently playing or not 
        /// </summary>
        private static void LevelLoadChecks()
        {
            var songPlaying = Analytics.CustomEvent("AudioCheck", new Dictionary<string, object>
                {
                    {"Audio Check", _isPlayingSong},
                    {"Level Loads", _levelLoaded}
                });
        }

        public static void LevelUnlocked()
        {
            var levelUnlocks = Analytics.CustomEvent("Level Unlocks"); 
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
        }

        /// <summary>
        ///     Prints which objects killed a player to the console
        /// </summary>
        private static void PrintsKillValues()
        {
            foreach (var kill in _obstacleKills) Debug.Log(kill);
        }
    }
}