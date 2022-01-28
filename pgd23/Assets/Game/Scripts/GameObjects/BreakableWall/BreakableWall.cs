using Game.Scripts.AbilitiesSystem.AbilityHandler;
using UnityEngine;

namespace Game.Scripts.GameObjects.BreakableWall
{
    public class BreakableWall : MonoBehaviour
    {
        [SerializeField] private GameObject brokenWallPiece;
        [SerializeField] private bool keepPlayerSpeed = true;
        
        private PlayerController _player;

        private void Awake()
        {
            _player = GetComponent<PlayerController>(); 
        }

        /// <summary>
        ///     When the player collides with a wall it will be replaced by stacked broken pieces
        /// </summary>
        private void BreakWall()
        {
            for (var i = 0; i < transform.localScale.y; i++)
            {
                var brokenPiece = Instantiate(brokenWallPiece, Vector2.zero, Quaternion.identity);
                
                brokenPiece.transform.localPosition =
                    (Vector2) transform.position + new Vector2(0, i - transform.localScale.y / 2);
            }

            Destroy(gameObject);
        }

        /// <summary>
        ///     Check if there is a collision with a player to break the wall.
        ///     This function sets the player object and you can set if de player maintains its velocity
        /// </summary>
        /// <param name="other"></param>
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.name.Equals("Player")) return;

            _player = other.gameObject.GetComponent<PlayerController>();

            if (!_player.isDashing) return;
        
            BreakWall();

            if (keepPlayerSpeed)
            {
                _player.Rigidbody.velocity = _player.PrevVelocity;
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (!collision.gameObject.name.Equals("Player")) return;
        
            if(collision.gameObject.GetComponent<PlayerController>().isDashing) BreakWall();
        }
    }
}