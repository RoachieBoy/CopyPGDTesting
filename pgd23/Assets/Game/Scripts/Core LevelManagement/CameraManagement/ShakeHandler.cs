using Game.Scripts.Core_LevelManagement.EventManagement;
using UnityEngine;

namespace Game.Scripts
{
    public class ShakeHandler : MonoBehaviour
    {
        [SerializeField] private Transform movingObject;
        [SerializeField] private float shakeDuration;
        [SerializeField] private float shakeMagnitude;
        [SerializeField] private float ShakeMagnitudeTreshold = .15f;
        [SerializeField] private float maxFeelDistance = 10;

        /// <summary>
        ///     Shakes the screen a certain amount
        /// </summary>
        public void TriggerShake()
        {
            var distanceBetweenObjects = Vector2.Distance(movingObject.transform.position, CameraMovement.current.objectToFollow.position);
            if (distanceBetweenObjects > maxFeelDistance) return;

            var _shakeMag = shakeMagnitude * (1 - (distanceBetweenObjects/maxFeelDistance));
            var _shakeDur = shakeDuration;

            if (_shakeMag < ShakeMagnitudeTreshold) return; 
            EventManager.Instance.OnTriggerShake(_shakeDur, _shakeMag);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(movingObject.position, maxFeelDistance);
        }
    }
}