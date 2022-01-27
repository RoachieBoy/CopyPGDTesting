using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Obstacles.Spikes
{
    public class Spikes : MonoBehaviour
    {

        private GameMaster gm;

        // Start is called before the first frame update
        void Start()
        {
            gm = GameObject.FindGameObjectWithTag("gm").GetComponent<GameMaster>();
            transform.position = gm.lastCheckPointPos;
        }

        // Update is called once per frame
        void Update()
        {

        }


        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}