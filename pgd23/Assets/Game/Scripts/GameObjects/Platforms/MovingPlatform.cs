using Game.Scripts.Core_LevelManagement.EventManagement;
using UnityEngine;

namespace Game.Scripts.GameObjects.Platforms
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private GameObject[] waypoints;
        [SerializeField] public float speed = 2f;
        public int currentWayPointIndex;

        private void Start()
        {
            EventManager.Instance.onMovingPlatformCorrection += JumpFix; 
        }

        private void OnDisable()
        {
            EventManager.Instance.onMovingPlatformCorrection -= JumpFix; 
        }

        private void FixedUpdate()
        {
            if (Vector2.Distance(waypoints[currentWayPointIndex].transform.position, transform.position) < .1f)
            {
                currentWayPointIndex++;
                if(currentWayPointIndex >= waypoints.Length)
                {
                    currentWayPointIndex = 0;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWayPointIndex].transform.position, Time.deltaTime * speed);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.name == "Player")
            {
                col.transform.SetParent(transform);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.name == "Player")
            {
                other.transform.SetParent(null);
            }
        }

        private void JumpFix(GameObject player)
        {
            var verticalSpeed = 0f;

            if (gameObject.layer == 6) 
                if (currentWayPointIndex == 0) verticalSpeed = speed * 2;

            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, verticalSpeed);
            
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, verticalSpeed);
        }
    }
}
