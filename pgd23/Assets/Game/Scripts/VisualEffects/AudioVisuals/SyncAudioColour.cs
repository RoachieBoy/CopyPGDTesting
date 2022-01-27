using System.Collections;
using UnityEngine;

namespace Game.Scripts.VisualEffects.AudioVisuals
{
    public class SyncAudioColour : AudioSyncer
    {
        public Color[] beatColors;
        public Color restColor;
        
        private SpriteRenderer _spriteRenderer; 
        private int _randIndex;

        private const string ColourChangeCoroutine = "MoveToColour"; 

        private void Start()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); 
        }

        /// <summary>
        ///     Interpolates a colour to a new colour value within a certain amount of time 
        /// </summary>
        /// <param name="target"> target colour </param>
        private IEnumerator MoveToColor(Color target)
        {
            var curr = _spriteRenderer.color;
            var initial = curr;
            float timer = 0;

            while (curr != target)
            {
                curr = Color.Lerp(initial, target, timer / timeToBeat);

                timer += Time.deltaTime;

                _spriteRenderer.color = curr;

                yield return null;
            }

            ToTheBeat = false;
        }
        
        /// <summary>
        ///     Generates a random colour 
        /// </summary>
        /// <returns> random colour </returns>
        private Color RandomColor()
        {
            if (beatColors == null || beatColors.Length == 0) return Color.white;

            _randIndex = Random.Range(0, beatColors.Length);
            
            return beatColors[_randIndex];
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            if (ToTheBeat) return;
            
            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, restColor, restSmoothTime * Time.deltaTime);
        }


        protected override void OnBeat()
        {
            base.OnBeat();

            var c = RandomColor();

            StopCoroutine(ColourChangeCoroutine);
            StartCoroutine(nameof(MoveToColor), c);
        }
    }
}
