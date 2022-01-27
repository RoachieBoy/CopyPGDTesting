using System.Collections;
using Game.Scripts.Core_LevelManagement.EventManagement;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera Camera;

    // Screenshake variables
    private Coroutine screenShake;
    private Coroutine scale;

    private void Awake() 
        => Camera = GetComponent<Camera>();

    private void Start() 
        => EventManager.Instance.onTriggerShake += TriggerShake;

    private void OnDisable() 
        => EventManager.Instance.onTriggerShake -= TriggerShake;

    /// <summary>
    ///     Shakes the screen a certain amount
    /// </summary>
    private void TriggerShake(float duration, float magnitude)
    {
        if (screenShake != null) StopCoroutine(screenShake);
        screenShake = StartCoroutine(ScreenShake(duration, magnitude));
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
        if (scale != null) StopCoroutine(scale);
        scale = StartCoroutine(Scale(desiredSize, smoothSpeed));
    }

    private IEnumerator Scale(float desiredSize, float smoothSpeed)
    {
        while(Mathf.Abs(Camera.orthographicSize - desiredSize) > .1f)
        {
            ScaleTo(desiredSize, smoothSpeed);
            yield return null;
        }
        Camera.orthographicSize = desiredSize;
    }

    private void ScaleTo(float desiredSize, float smoothSpeed)
        => Camera.orthographicSize = Mathf.Lerp(Camera.orthographicSize, desiredSize, smoothSpeed);
}
