using System.Collections;
using UnityEngine;

namespace Game.Scripts.Tools
{
    public class RotationTool : MonoBehaviour
    {
        [SerializeField] private float rotateDuration;

        private void Update()
        {
            StartCoroutine(Rotate(rotateDuration));
        }

        /// <summary>
        ///     Rotates an object using euler angles
        /// </summary>
        /// <param name="duration"> how long does one rotation take </param>
        /// <returns> IEnumerator which returns null </returns>
        private IEnumerator Rotate(float duration)
        {
            var startRotation = transform.eulerAngles.y;
            var endRotation = startRotation + 360.0f;
            var t = 0.0f;
            while (t < duration)
            {
                t += Time.deltaTime;
                var yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
                yield return null;
            }
        }
    }
}