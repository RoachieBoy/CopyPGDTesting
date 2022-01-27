using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.VisualEffects.AudioVisuals
{
    public class AudioSyncScale : AudioSyncer
    {
        public Vector3 restScale;
        [SerializeField] private Vector3 minSize;
        [SerializeField] private Vector3 maxSize;

        private const string ScaleCoroutineName = "MoveToScale"; 

        /// <summary>
        ///     Moves an object from an initial scale to a desired scale 
        /// </summary>
        /// <param name="target"> target scale the object should reach </param>
        /// <returns></returns>
        private IEnumerator MoveToScale(Vector3 target)
        {
            var curr = transform.localScale;
            var initial = curr;
            
            //how far are we between the initial and the current scale 
            float timer = 0;

            //when the current scale value has not yet reached its target, transform the scale
            while (curr != target)
            {
                curr = Vector3.Lerp(initial, target, timer / timeToBeat);
                timer += Time.deltaTime;

                transform.localScale = curr;

                yield return null;
            }

            ToTheBeat = false;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            //if the scale is already being manipulated, dont do it again
            if (ToTheBeat) return;

            transform.localScale = Vector3.Lerp(transform.localScale, restScale, restSmoothTime * Time.deltaTime);
        }

        protected override void OnBeat()
        {
            base.OnBeat();

            var newScale = new Vector3(Random.Range(minSize.x, minSize.x), Random.Range(minSize.y, maxSize.y)); 

            StopCoroutine(ScaleCoroutineName);
            StartCoroutine(nameof(MoveToScale), newScale);
        }
    }
}