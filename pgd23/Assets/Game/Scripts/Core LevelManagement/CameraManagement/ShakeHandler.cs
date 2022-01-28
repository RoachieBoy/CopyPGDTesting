using Game.Scripts.Core_LevelManagement.EventManagement;
using UnityEngine;

namespace Game.Scripts.Core_LevelManagement.CameraManagement
{
    public class ShakeHandler : MonoBehaviour
    {
        [SerializeField] private Transform movingObject;
        [SerializeField] private float shakeDuration;
        [SerializeField] private float shakeMagnitude;
        [SerializeField] private float shakeMagnitudeThreshold = .15f;
        [SerializeField] private float maxFeelDistance = 10;

        /// <summary>
        ///     Shakes the screen a certain amount
        /// </summary>
        public void TriggerShake()
        {
            var distanceBetweenObjects = Vector2.Distance(movingObject.transform.position, CameraMovement.Current.objectToFollow.position);
            if (distanceBetweenObjects > maxFeelDistance) return;

            var shakeMag = shakeMagnitude * (1 - (distanceBetweenObjects/maxFeelDistance));
            var shakeDur = shakeDuration;

            if (shakeMag < shakeMagnitudeThreshold) return; 
            EventManager.Instance.OnTriggerShake(shakeDur, shakeMag);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(movingObject.position, maxFeelDistance);
        }
    }
}