using System.Collections;
using Game.Scripts.AbilitiesSystem.AbilityHandler;
using Game.Scripts.Core_LevelManagement.EventManagement;
using Game.Scripts.KeyBindings;
using UnityEngine;

namespace Game.Scripts.AbilitiesSystem.Abilities
{
    public class Dash : AbilityClass
    {
        private const float SpeedMultiplier = 100f;
        private IEnumerator _dashCoroutine;
        private bool _isDashing, _canDash = true;
        private float _gravity;
        private float _lastTapTime;
        private PlayerController _player;
       

        #region Dash Properties

        /// <summary>
        ///     Speed of the dash
        /// </summary>
        public float DashSpeed { get; set; }

        /// <summary>
        ///     Direction of the dash represented as a vector2
        /// </summary>
        public Vector2 Direction { get; set; }

        /// <summary>
        ///     Key to dash
        /// </summary>
        public KeyBindingActions DashKey { get; set; }

        /// <summary>
        ///     Determines how fast someone has to double tap to dash the player
        /// </summary>
        public float TapSpeed { get; set; }

        /// <summary>
        ///     Duration of the dash 
        /// </summary>
        public float Duration { get; set; }

        /// <summary>
        ///     How long of c cooldown should the dash have
        /// </summary>
        public float CooldownAmount { get; set; }

        #endregion

        #region Unity Events

        private void Start()
        {
            _player = PlayerObject.GetComponent<PlayerController>();
            _gravity = _player.Rigidbody.gravityScale; 
        }

        private void Update() => DoubleClicked();

        private void FixedUpdate() => IsDashing();

        #endregion

        #region Extra Logic

        /// <summary>
        ///     Behaviour of the player when they are dashing 
        /// </summary>
        private void IsDashing()
        {
            //saves previous velocity value of the player 
            var vel = _player.Rigidbody.velocity;

            if (_isDashing)
            {
                //sets the player velocity to zero right before dashing 
                _player.Rigidbody.velocity = Vector2.zero;
                _player.Rigidbody.AddForce(new Vector2(Direction.x, 0) * DashSpeed * SpeedMultiplier);
            }

            //resets velocity to the original saved value 
            _player.Rigidbody.velocity = vel;
        }

        private IEnumerator Dashing(float dashDuration, float dashCooldown)
        {
            DashingState();
            yield return new WaitForSeconds(dashDuration);
            CooldownState();
            yield return new WaitForSeconds(dashCooldown);
            ReadyState();
        }

        /// <summary>
        ///     Measures if a player has double tapped a key within a certain time span
        /// </summary>
        private void DoubleClicked()
        {
            if (!InputManager.Instance.GetKeyDown(DashKey)) return;

            if (Time.time - _lastTapTime < TapSpeed)
                if (_canDash)
                {
                    if (_dashCoroutine != null) StopCoroutine(_dashCoroutine);
                    _dashCoroutine = Dashing(Duration, CooldownAmount);
                    StartCoroutine(_dashCoroutine);
                }

            //resets the double clicked time 
            _lastTapTime = Time.time;
        }

        #endregion

        #region Dash States

        /// <summary>
        ///     Used to determine behaviour whilst dashing 
        /// </summary>
        private void DashingState()
        {
            _isDashing = true;
            EventManager.Instance.OnActivateDashParticle();
            _player.isDashing = true;
            _player.Rigidbody.gravityScale = 0;
            _canDash = false;
        }

        /// <summary>
        ///     Used to determine behaviour whilst in the cooldown state
        /// </summary>
        private void CooldownState()
        {
            _player.isDashing = false;
            _isDashing = false;
            _player.Rigidbody.gravityScale = _gravity; 
        }

        /// <summary>
        ///     Player is ready and able to dash 
        /// </summary>
        private void ReadyState() => _canDash = true;

        #endregion
    }
}