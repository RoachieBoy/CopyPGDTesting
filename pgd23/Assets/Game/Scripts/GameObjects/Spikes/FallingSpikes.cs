using UnityEngine;

namespace Game.Scripts.GameObjects.Spikes
{
    public class FallingSpikes : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private BoxCollider2D _boxCollider2D;
        public float distance;
        private bool _isFalling;

        // Start is called before the first frame update
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            Physics2D.queriesStartInColliders = false;
            if (_isFalling != false) return;
            var hit = Physics2D.Raycast(transform.position, Vector2.down, distance);

            Debug.DrawRay(transform.position,Vector2.down * distance,Color.green);

            if (hit.transform == null) return;
            if (!hit.transform.CompareTag("Player")) return;
            
            _rb.gravityScale = 5;
            _isFalling = true;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
            else
            {
                _rb.gravityScale = 0;
                _boxCollider2D.enabled = false;
            }
        }

    }
}
