using System;
using UnityEngine;
namespace Game.Scripts.VisualEffects.TextEffects
{

    public class TextManipulation : MonoBehaviour
    {
        [SerializeField] private TextManipulationType type;
        private const string ErrorMessage = "This text manipulation effect doesn't exist"; 

        private void Start()
        {
           DetermineEffect();
        }

        /// <summary>
        ///     Determines which text effect should be selected based on an enum type
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"> error when not correct effect </exception>
        private void DetermineEffect()
        {
            switch (type)
            {
                case TextManipulationType.VertexWobble:
                    gameObject.AddComponent<VertexWobble>();
                    break;
                case TextManipulationType.WordWobble:
                    gameObject.AddComponent<WordWobble>();
                    break;
                case TextManipulationType.CharacterWobble:
                    gameObject.AddComponent<CharacterWobble>();
                    break; 
                default:
                    throw new ArgumentOutOfRangeException(ErrorMessage);
            }
        }
    }
}