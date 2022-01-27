using UnityEngine;

namespace Game.Scripts.Obstacles.Spikes
{
    public class FallingSpikes : MonoBehaviour
    {
        Rigidbody2D rb;
        BoxCollider2D boxCollider2D;
        public float distance;
        bool isFalling = false;

        // Start is called before the first frame update
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            boxCollider2D = GetComponent<BoxCollider2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            Physics2D.queriesStartInColliders = false;
            if(isFalling == false)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance);

                Debug.DrawRay(transform.position,Vector2.down * distance,Color.green);

                if(hit.transform != null)
                {
                    if(hit.transform.tag == "Player")
                    {
                        rb.gravityScale = 5;
                        isFalling = true;
                    }
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                Destroy(gameObject);
            }
            else
            {
                rb.gravityScale = 0;
                boxCollider2D.enabled = false;
            }
        }

    }
}
