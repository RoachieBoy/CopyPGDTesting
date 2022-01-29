using Game.Scripts.Core_LevelManagement.EventManagement;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scripts.Menu
{
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField] private int _levelId;
        [SerializeField] private bool _locked;
        [SerializeField] private string newText;

        public int LevelId
        {
            get => _levelId;
            private set => _levelId = value;
        }

        public bool Locked
        {
            get => _locked;
            private set => _locked = value;
        }
        
        void Start()
        {
            var unlockRequirement = "completed-" + (_levelId - 1);

            if (_locked && PlayerPrefs.HasKey(unlockRequirement) && PlayerPrefs.GetInt(unlockRequirement) == 1)
            {
                UnityAnalyticsManager.LevelUnlocked();
                
                Unlock();
            }
            else if (_locked)
            {
                Lock();
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                int count = 1;
                
                while (PlayerPrefs.HasKey("completed-" + count))
                {
                    PlayerPrefs.DeleteKey("completed-" + count);
                    count++;
                }

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                LockLevels();

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        public static void LockLevels()
        {
            for (int i = 1; i < 10; i++)
            {
                PlayerPrefs.SetInt("completed-" + i, 1);
            }
        }

        public void Lock()
        {
            _locked = true;
            ChangeOpacity(0.5f);
        }

        private void Unlock()
        {
            _locked = false;
            ChangeOpacity(1f);
            ChangeText();
        }

        private void ChangeOpacity(float alpha)
        {
            Color newColor;

            Image image = GetComponent<Image>();

            newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            TextMeshProUGUI[] childrenText = GetComponentsInChildren<TextMeshProUGUI>();

            foreach (TextMeshProUGUI child in childrenText)
            {
                newColor = child.color;
                newColor.a = alpha;
                child.color = newColor;
            }
        }

        private void ChangeText()
        {
            TextMeshProUGUI[] childrenText = GetComponentsInChildren<TextMeshProUGUI>();

            foreach (TextMeshProUGUI child in childrenText)
            {
                child.text = newText;
            }
        }
    }
}