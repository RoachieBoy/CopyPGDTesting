using UnityEngine;

namespace Game.Scripts.VisualEffects
{
    public class HideAndShow : MonoBehaviour
    {
        [SerializeField] private float range;
        [SerializeField] private GameObject objectToHideFrom;

        private SpriteRenderer _renderer;


        private void Start()
        {
            _renderer = gameObject.GetComponent<SpriteRenderer>();
        }

        /// <summary>
        ///     Remaps a series of values from one range to another 
        /// </summary>
        /// <param name="value"> the value of the new range </param>
        /// <param name="from1"> starting value </param>
        /// <param name="to1"> ending value </param>
        /// <param name="from2"> second starting value </param>
        /// <param name="to2"> second ending value </param>
        /// <returns></returns>
        private static float Remap(float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }


        private void Update()
        {
            var distance = Vector2.Distance(transform.position, objectToHideFrom.transform.position);
            var alpha = Remap(distance, 0, range, 1, 0);
            alpha = Mathf.Clamp(alpha, 0, 1);
            
            var clr = _renderer.material.color;
            _renderer.material.color = new Color(clr.r, clr.g, clr.b, alpha);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}