using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.AbilitiesSystem.Actions
{
    public class Resetter : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}