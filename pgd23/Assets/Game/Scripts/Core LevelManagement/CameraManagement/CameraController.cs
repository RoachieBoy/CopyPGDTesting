using System.Collections;
using Game.Scripts.Core_LevelManagement.EventManagement;
using UnityEngine;

namespace Game.Scripts.Core_LevelManagement.CameraManagement
{
    public class CameraController : MonoBehaviour
    {
        private Camera _camera;

        // Screenshake variables
        private Coroutine _screenShake;
        private Coroutine _scale;

        private void Awake() 
            => _camera = GetComponent<Camera>();

        private void Start() 
            => EventManager.Instance.onTriggerShake += TriggerShake;

        private void OnDisable() 
            => EventManager.Instance.onTriggerShake -= TriggerShake;

        /// <summary>
        ///     Shakes the screen a certain amount
        /// </summary>
        private void TriggerShake(float duration, float magnitude)
        {
            if (_screenShake != null) StopCoroutine(_screenShake);
            _screenShake = StartCoroutine(ScreenShake(duration, magnitude));
        }

        private IEnumerator ScreenShake(float duration, float magnitude)
        {

            float elapsed = 0;

            var magnitudeSteps = magnitude / duration;

            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;
                float z = -10;

                transform.localPosition = new Vector3(x, y, z);

                elapsed += Time.deltaTime;

                magnitude -= magnitudeSteps * Time.deltaTime;

                yield return null;
            }
        }

    
        // Scaler functions
        public void ScaleToDesiredSize(float desiredSize, float smoothSpeed)
        {
            if (_scale != null) StopCoroutine(_scale);
            _scale = StartCoroutine(Scale(desiredSize, smoothSpeed));
        }

        private IEnumerator Scale(float desiredSize, float smoothSpeed)
        {
            while(Mathf.Abs(_camera.orthographicSize - desiredSize) > .1f)
            {
                ScaleTo(desiredSize, smoothSpeed);
                yield return null;
            }
            _camera.orthographicSize = desiredSize;
        }

        private void ScaleTo(float desiredSize, float smoothSpeed)
            => _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, desiredSize, smoothSpeed);
    }
}
