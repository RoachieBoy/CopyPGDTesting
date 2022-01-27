using UnityEngine;

namespace Game.Scripts.Tools
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        /// <summary>
        ///     Stores an instance of the object you wish to apply the singleton pattern to 
        /// </summary>
        public static T Instance
        {
            get
            {
                //checks that there is not an existing instance of the component 
                if (_instance != null) return _instance;
                
                //check for an instance of the component at runtime 
                _instance = FindObjectOfType<T>();
                
                if (_instance != null) return _instance;

                //if it doesn't exist,create an instance 
                var gameObject = new GameObject
                {
                    name = typeof(T).Name
                };

                _instance = gameObject.AddComponent<T>();

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}