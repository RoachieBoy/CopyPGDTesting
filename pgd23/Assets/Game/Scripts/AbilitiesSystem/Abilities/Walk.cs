using Game.Scripts.AbilitiesSystem.AbilityHandler;
using Game.Scripts.KeyBindings;
using UnityEngine;

namespace Game.Scripts.AbilitiesSystem.Abilities
{
    public class Walk : AbilityClass
    {
        private Rigidbody2D _playerRigidbody2D;
        private const float SpeedMultiplier = 1000;

        #region PublicFields

        /// <summary>
        ///     Acceleration value when the player is walking
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        ///     Which direction the player should move in
        /// </summary>
        public Vector2 Direction { get; set; }

        /// <summary>
        ///     Key to press to get the player to walk
        /// </summary>
        public KeyBindingActions ActionKey { get; set; }

        #endregion

        #region Logic

        private void Start() => _playerRigidbody2D = PlayerObject.GetComponent<Rigidbody2D>();
        
        public void FixedUpdate() => Move();
        
        /// <summary>
        ///     Moves the player in a right direction
        /// </summary>
        private void Move()
        {
            if (!InputManager.Instance.GetKey(ActionKey)) return;

            _playerRigidbody2D.AddForce(transform.right * Direction * Time.deltaTime * Speed * SpeedMultiplier);
        }

        #endregion
    }
}