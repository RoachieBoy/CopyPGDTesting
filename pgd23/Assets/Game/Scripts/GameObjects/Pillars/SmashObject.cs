using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.GameObjects.Pillars
{
    public class SmashObject : MonoBehaviour
    {
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
