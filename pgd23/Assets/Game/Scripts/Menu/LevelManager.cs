using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager
{
    public static void CompleteLevel(int levelId)
    {
        PlayerPrefs.SetInt("completed-" + levelId, 1);
        //SceneManager.LoadScene("LevelSelector");
    }
}