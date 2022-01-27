using System;
using UnityEngine;

namespace Game.Scripts.Tools
{
    public class SinMovement : MonoBehaviour
    {
        [SerializeField] private float amplitude = .2f;
        [SerializeField] private float speed = 3.3f;
        private Vector2 _startPos;

        private void Start()
        {
            _startPos = transform.position; 
        }

        private void FixedUpdate()
        {
            transform.position = _startPos + new Vector2(0, SinVector());
        }

        /// <summary>
        ///     Creates a vector to move an object using sinus waves 
        /// </summary>
        /// <returns> a sinus vector </returns>
        private float SinVector()
        {
            return amplitude * Mathf.Sin(Time.time * speed);
        }
    }
}