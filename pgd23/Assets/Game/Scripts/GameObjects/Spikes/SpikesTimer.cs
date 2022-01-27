using UnityEngine;

namespace Game.Scripts.Obstacles.Spikes
{
    public class SpikesTimer : MonoBehaviour
    {
        Rigidbody2D rb;
        BoxCollider2D boxCollider2D;
        public float distance;
        bool isRising = false;
        public float timeLeft;
        bool startTimer = false;

        // Start is called before the first frame update
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            boxCollider2D = GetComponent<BoxCollider2D>();
            boxCollider2D.enabled = false;
        }

        // Update is called once per frame
        private void Update()
        {
        
            // Physics2D.queriesStartInColliders = false;
            if (isRising == false)
            {
                
                RaycastHit2D[] hits;
                hits = Physics2D.RaycastAll(transform.position, Vector2.up, distance);
                //RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0f, 0.2f, 0f), Vector2.up, distance);
                Debug.DrawRay(transform.position + new Vector3(0f,0.2f,0f), Vector2.up * distance, Color.green);

                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.transform.tag != "Player") continue;
                    startTimer = true;
                    break;
                }           
            }
            if (timeLeft <= 0)
            {
                startTimer = false;
                isRising = true;
                boxCollider2D.enabled = true;
                Vector2 newPosition = transform.position;
                newPosition.y += 0.25F;
                transform.position = newPosition;
                timeLeft = 2;
                
                gameObject.tag = "Obstacle";
            }
        }

        private void FixedUpdate()
        {
            if (startTimer == true)
            {
                timeLeft -= Time.deltaTime;
            }     
        }

        //private void OnCollisionEnter2D(Collision2D other)
        //{
        //    if (other.gameObject.tag == "Player")
        //    {
        //        Destroy(gameObject);
        //    }
        //    else
        //    {
        //        rb.gravityScale = 0;
        //        boxCollider2D.enabled = false;
        //    }
        //}
    }
}
