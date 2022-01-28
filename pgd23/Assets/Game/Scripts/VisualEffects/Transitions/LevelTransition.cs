using System.Collections;
using System.Collections.Generic;
using Game.Scripts.AudioManagement;
using Game.Scripts.Core_LevelManagement.EventManagement;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

namespace Game.Scripts.VisualEffects.Transitions
{
    public class LevelTransition : MonoBehaviour
    {
        [Header("Transition Settings")]
        [SerializeField] private Animator transition;
        [SerializeField] private float transitionTime = 2f;
        [SerializeField] private string songToPlay;
        [SerializeField] private string songToStop; 
        private static readonly int TransitionFade = Animator.StringToHash("Start");
        private Coroutine _fade; 

        public void Start()
        {
            EventManager.Instance.onFadeOut += LoadNextLevel;

            AudioHandler.Instance.Play(songToPlay); 
            AudioHandler.Instance.Stop(songToStop);
        }

        private void OnDisable()
        {
            EventManager.Instance.onFadeOut -= LoadNextLevel;
        }

        /// <summary>
        ///     Loads the next level with a fade transition
        /// </summary>
        private void LoadNextLevel()
        {
            if (SceneManager.GetActiveScene().buildIndex > SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                _fade ??= StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
            }
        }

        /// <summary>
        ///     Coroutine that waits a few seconds before transitioning to a new scene
        /// </summary>
        /// <param name="index"> scene to transition to </param>
        /// <returns> amount of seconds to transition </returns>
        private IEnumerator LoadLevel(int index)
        {
            PlayerPrefs.SetInt("completed-" + (index - 1), 1);
            transition.SetTrigger(TransitionFade);
            
            yield return new WaitForSeconds(transitionTime);
            
            SceneManager.LoadScene(index);

            if (AudioHandler.Instance.GetCurrentSong() == null) yield break;
            
            var levelCompletion = Analytics.CustomEvent("Audio Check");
            
            Debug.Log(levelCompletion);
        }
    }
}