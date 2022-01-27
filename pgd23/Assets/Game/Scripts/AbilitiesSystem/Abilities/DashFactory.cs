using Game.Scripts.KeyBindings;
using UnityEngine;

namespace Game.Scripts.AbilitiesSystem.Abilities
{
    public class DashFactory: MonoBehaviour
    {
        [Header("Dash Settings")]
        [SerializeField, Range(1, 10)] private float dashSpeed;
        [SerializeField, Range(.1f, 1f)] private float tapSpeed;
        [SerializeField, Range(.1f, 1f)] private float dashDuration = .2f;
        [SerializeField, Range(.1f, 1f)] private float dashCooldown = .1f; 

        #region Dash_Types

        /// <summary>
        ///     Creates an instance of the dash class to dash right 
        /// </summary>
        /// <param name="player"> player object </param>
        /// <returns> an instance of the dash class </returns>
        public Dash DashRight(GameObject player)
        {
            var dashRight = player.AddComponent<Dash>();
            CreateDash(dashRight, Vector2.right, KeyBindingActions.WalkRightKey);

            return dashRight;
        }

        /// <summary>
        ///     Creates an instance of the dash class to dash left 
        /// </summary>
        /// <param name="player"> player object </param>
        /// <returns> an instance of the dash class </returns>
        public Dash DashLeft(GameObject player)
        {
            var dashLeft = player.AddComponent<Dash>();
            CreateDash(dashLeft, Vector2.left, KeyBindingActions.WalkLeftKey);

            return dashLeft;
        }
        
        #endregion
        
        #region CreateDash 
        /// <summary>
        ///     Creates a version of a dash 
        /// </summary>
        /// <param name="dash"> type of dash </param>
        /// <param name="dir"> direction of the dash </param>
        /// <param name="key"> which key to dash </param>
        private void CreateDash(Dash dash, Vector2 dir, KeyBindingActions key)
        {
            dash.Duration = dashDuration; 
            dash.DashSpeed = dashSpeed;
            dash.Direction = dir;
            dash.CooldownAmount = dashCooldown; 
            dash.TapSpeed = tapSpeed;
            dash.DashKey = key;
        }
        
        #endregion
    }
}