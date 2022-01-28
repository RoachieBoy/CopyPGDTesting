using UnityEngine;

namespace Game.Scripts.Menu
{
    public static class LevelManager
    {
        public static void CompleteLevel(int levelId)
        {
            PlayerPrefs.SetInt("completed-" + levelId, 1);
            //SceneManager.LoadScene("LevelSelector");
        }
    }
}