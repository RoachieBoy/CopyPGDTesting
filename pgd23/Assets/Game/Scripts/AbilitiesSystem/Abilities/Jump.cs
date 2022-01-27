using System.Diagnostics.CodeAnalysis;
using Game.Scripts.AbilitiesSystem.AbilityHandler;
using Game.Scripts.Core_LevelManagement.EventManagement;
using Game.Scripts.GameObjects.Obstacles;
using Game.Scripts.KeyBindings;
using UnityEngine;

namespace Game.Scripts.AbilitiesSystem.Abilities
{
    public class Jump : AbilityClass
    {
        private const float JumpMultiplier = 1000f;

        private const int DoubleJumpAmount = 2; 

        [Header("Jump Settings")]
        [SerializeField] [Range(1, 20)] private float jumpHeight = 5f;

        [Tooltip("The double jump should give a small extra boost to the player")] 
        [SerializeField] private float extraKick = 1.2f;

        private bool _canDoubleJump;
        private PlayerController _player;

        #region Logic

        private void Start()
        {
            _player = PlayerObject.GetComponent<PlayerController>();
        }

        private void Update()
        {
            //if the player is not pressing the jump key, don't jump 
            if (!InputManager.Instance.GetKeyDown(KeyBindingActions.JumpKey)) return;

            if (_player.IsGrounded())
            {
                DoAJump(jumpHeight);
                _canDoubleJump = true;
            }
            else
            {
                if (!_canDoubleJump || AmountOfTriggers != DoubleJumpAmount) return;
                _canDoubleJump = false;
                
                DoAJump(jumpHeight * extraKick);
            }
        }

        /// <summary>
        ///     Adds force to the player so it can jump upwards 
        /// </summary>
        /// <param name="yForce"> force at which the player moves upwards </param>
        private void DoAJump(float yForce)
        {
            EventManager.Instance.OnMovingPlatformCorrection(_player.gameObject);

            _player.Rigidbody.AddForce(new Vector2(0f, yForce * JumpMultiplier));
            
            EventManager.Instance.OnActivateDust();
        }

        #endregion
    }
}