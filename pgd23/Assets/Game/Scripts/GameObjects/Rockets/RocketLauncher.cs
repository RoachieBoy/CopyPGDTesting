using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.GameObjects.Rockets
{
    public class RocketLauncher : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float viewDistance;
        
        public List<HeatSeekingRocket> rockets = new List<HeatSeekingRocket>();
        
        private bool _targetInDistance;
        
        private void FixedUpdate()
        {
            TargetDetection();

            if (_targetInDistance)
            {
                AttackState();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void TargetDetection()
        {
            Vector2 targetDirection = target.position - transform.position;
            var targetDistance = (targetDirection.x * targetDirection.x) + (targetDirection.y * targetDirection.y);
            Mathf.Sqrt(targetDistance);

            _targetInDistance = targetDistance < viewDistance;
        }

        private void AttackState()
        {
            var rocket = GetComponent<HeatSeekingRocket>();
            //rocket.Initialize(transform.position, target);
            rockets.Add(rocket);
        }
    }
}
