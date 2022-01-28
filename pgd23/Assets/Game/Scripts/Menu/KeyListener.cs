using UnityEngine;

namespace Game.Scripts.Menu
{
    public class KeyListener : MonoBehaviour
    {
        [SerializeField] private int _sceneIndex;

        private void Update()
        {
            if (Input.anyKey && !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2) && !Input.GetKey(KeyCode.Escape))
            {
                MenuManager.LoadScene(_sceneIndex);
            }
        }
    }
}