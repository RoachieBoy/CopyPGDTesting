using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Core_LevelManagement.EventManagement;
using Game.Scripts.GameObjects.Portal;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

namespace Game.Scripts.AbilitiesSystem.AbilityHandler
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        private const float GroundDistance = .025f;

        [Header("General Player Settings")]
        [SerializeField] private LayerMask environmentLayer;

        [Header("Movement Settings")]
        [SerializeField] private float maxHorizontalSpeed;
        [SerializeField] private float maxHorizontSpeedBoost;
        [SerializeField] private float maxVerticalSpeed;
        [SerializeField] private float drag;
        
        [Header("Particles")]
        [SerializeField] private ParticleSystem Dust;
        [SerializeField] private ParticleSystem DashEffect;

        private float MaxHorSpeed;

        public bool grounded; 

        #region Public_Fields
        
        /// <summary>
        ///     Stores the previous velocity value 
        /// </summary>
        public Vector2 PrevVelocity { get; private set; }
        
        /// <summary>
        ///     Checks whether a player is currently dashing or not 
        /// </summary>
        public bool isDashing;
        /// <summary>
        ///     Gets the players Rigidbody component
        /// </summary>
        public Rigidbody2D Rigidbody { get; private set; }

        #endregion

        #region Logic

        private void Start()
        {
            Rigidbody = gameObject.GetComponent<Rigidbody2D>();

            //ensures particles aren't playing on start 
            Dust.Stop();
            DashEffect.Stop();
            
            EventManager.Instance.onActivateDust += ActivateDustParticles;
            EventManager.Instance.onActivateDashParticle += ActivateDashParticles;
        }

        private void OnDisable()
        {
            EventManager.Instance.onActivateDust -= ActivateDustParticles;
            EventManager.Instance.onActivateDashParticle -= ActivateDashParticles;
        }

        private void Update()
        {
            if (IsGrounded()) MaxHorSpeed = maxHorizontalSpeed;

            ApplyHorizontalDrag();
            
            grounded = IsGrounded(); 

            LimitMovement();
        }

        /// <summary>
        ///     This function sets the previousVelocity. This is used to keep de velocity while
        ///     colliding with a breakable wall
        /// </summary>
        private void FixedUpdate()
        {
            PrevVelocity = Rigidbody.velocity;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Portal>()) PortalManager.Current.UsePortal(collision.GetComponent<Portal>(), gameObject);
            
            if (!collision.gameObject.CompareTag("Obstacle")) return;
            
            UnityAnalyticsManager.KilledPlayer(collision.gameObject.name);
                
            Die();
        }

        /// <summary>
        ///     Checks if the player is grounded using ray-casting
        /// </summary>
        /// <returns> true or false value </returns>
        public bool IsGrounded()
        {
            var transform1 = transform;
            var localScale = transform1.localScale;
            var position = transform1.position;

            var startPos1 = new Vector2(position.x + localScale.x / 2f,
                position.y - localScale.y / 2f);
            var startPos2 = new Vector2(position.x - localScale.x / 2f,
                position.y - localScale.y / 2f);
            // Create two raycasts
            var hit1 = Physics2D.Raycast(startPos1, Vector2.down, GroundDistance, environmentLayer);
            var hit2 = Physics2D.Raycast(startPos2, Vector2.down, GroundDistance, environmentLayer);

            UnityEngine.Debug.DrawRay(startPos1, Vector2.down * GroundDistance);
            // Return whether entity touches ground or not
            return hit1.collider != null || hit2.collider || transform.parent?.gameObject.layer == 6;
        }

        /// <summary>
        ///     Activates a dust type particle effect 
        /// </summary>
        private void ActivateDustParticles()
        {
            var scale = transform.localScale / 3; 
            Dust.Play();
            Dust.transform.position = new Vector2(transform.position.x, transform.position.y - scale.y); 
        }

        private void ActivateDashParticles()
        {
            var scale = transform.localScale / 2;
            DashEffect.Play();
            DashEffect.transform.position = new Vector2(transform.position.x, transform.position.y);
        }

        /// <summary>
        ///     Knock back function for the player 
        /// </summary>
        /// <param name="knockDur"> duration of the knock back </param>
        /// <param name="knockPow"> power of the knock back </param>
        /// <param name="knockDir"> direction to move to </param>
        /// <returns></returns>
        public IEnumerator KnockBack(float knockDur, Vector2 knockPow, Vector2 knockDir)
        {
            float timer = 0;

            while (knockDur > timer)
            {
                timer += Time.deltaTime; 
                
                Rigidbody.AddForce(new Vector2(knockDir.x * knockPow.x, knockPow.y));
            }

            yield return 0; 
        }

        /// <summary>
        ///     Restarts the scene
        /// </summary>
        /// <returns></returns>
        public static void Die()
        {
            UnityAnalyticsManager.PlayerDied();
            Debug.Log(UnityAnalyticsManager.Deaths);
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        /// <summary>
        ///     Limit the velocity of the player to a certain vertical and horizontal amount 
        /// </summary>
        private void LimitMovement()
        {
            var vel = Rigidbody.velocity;
            vel.x = Mathf.Clamp(vel.x, -MaxHorSpeed, MaxHorSpeed);
            vel.y = Mathf.Clamp(vel.y, -maxVerticalSpeed, maxVerticalSpeed);    
            Rigidbody.velocity = vel;
        }

        /// <summary>
        ///     Applies a constant drag to the player 
        /// </summary>
        private void ApplyHorizontalDrag()
        {
            if (!IsGrounded()) return;
            Rigidbody.velocity -= (drag * Time.deltaTime * Rigidbody.velocity);
        }

        public void UpXLimit() => MaxHorSpeed = maxHorizontSpeedBoost;

        #endregion
    }
}