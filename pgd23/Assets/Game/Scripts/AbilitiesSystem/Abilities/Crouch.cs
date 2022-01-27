using System;
using System.Diagnostics.CodeAnalysis;
using Game.Scripts.AbilitiesSystem.AbilityHandler;
using Game.Scripts.KeyBindings;
using UnityEngine;

namespace Game.Scripts.AbilitiesSystem.Abilities
{
    [SuppressMessage("ReSharper", "Unity.InefficientPropertyAccess")]
    public class Crouch : AbilityClass
    {
        [Header("Crouch Settings")]
        [SerializeField] private float crouchPercentage = .5f;
        
        private Vector2 _originalScale;
        private PlayerController _parent;

        #region Logic

        private void Start()
        {
            _parent = PlayerObject.GetComponent<PlayerController>();
            _originalScale = _parent != null ? _parent.transform.localScale : Vector3.zero;
        }

        public void Update()
        {
            var parentTransform = _parent.transform;
            //calculates the difference in size from the original scale and the crouch scale
            var difference = (parentTransform.localScale.y - crouchPercentage) / 2;

            if (difference < 0) BigF();

            if (InputManager.Instance.GetKeyDown(KeyBindingActions.CrouchKey))
            {
                parentTransform.localScale = new Vector2(parentTransform.localScale.x,
                    parentTransform.localScale.y * crouchPercentage);

                //changes position when the scale is changed 
                parentTransform.position =
                    new Vector3(parentTransform.position.x, parentTransform.position.y - difference);
            }
            else if (InputManager.Instance.GetKeyUp(KeyBindingActions.CrouchKey))
            {
                //resets the scale 
                _parent.transform.localScale = _originalScale;

                //resets the position back to its original state 
                parentTransform.position =
                    new Vector3(parentTransform.position.x, parentTransform.position.y + difference);
            }
        }

        /// <summary>
        ///     When the scale isn't correct, this error gets thrown
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"> not accepted condition </exception>
        private static void BigF()
        {
            throw new ArgumentOutOfRangeException("This crouch value isn't acceptable, make the difference" +
                                                  " bigger than 1");
        }

        #endregion
    }
}