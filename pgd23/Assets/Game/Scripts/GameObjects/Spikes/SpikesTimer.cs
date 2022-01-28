using UnityEngine;

namespace Game.Scripts.GameObjects.Spikes
{
    public class SpikesTimer : MonoBehaviour
    {
        private BoxCollider2D _boxCollider2D;
        public float distance;
        private bool _isRising;
        public float timeLeft;
        private bool _startTimer;

        // Start is called before the first frame update
        private void Start()
        {
            GetComponent<Rigidbody2D>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _boxCollider2D.enabled = false;
        }

        // Update is called once per frame
        private void Update()
        {
            // Physics2D.queriesStartInColliders = false;
            if (_isRising == false)
            {
                var hits = Physics2D.RaycastAll(transform.position, Vector2.up, distance);
                //RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0f, 0.2f, 0f), Vector2.up, distance);
                Debug.DrawRay(transform.position + new Vector3(0f, 0.2f, 0f), Vector2.up * distance, Color.green);

                foreach (var hit in hits)
                {
                    if (!hit.transform.CompareTag("Player")) continue;
                    _startTimer = true;
                    break;
                }
            }

            if (!(timeLeft <= 0)) return;
            _startTimer = false;
            _isRising = true;
            _boxCollider2D.enabled = true;
            Vector2 newPosition = transform.position;
            newPosition.y += 0.25F;
            transform.position = newPosition;
            timeLeft = 2;

            gameObject.tag = "Obstacle";
        }

        private void FixedUpdate()
        {
            if (_startTimer)
            {
                timeLeft -= Time.deltaTime;
            }
        }
    }
}