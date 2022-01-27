using Game.Scripts.KeyBindings;
using UnityEngine;

namespace Game.Scripts.AbilitiesSystem.Abilities
{
    public class WalkFactory: MonoBehaviour
    {
        [Header("Walk Settings")] 
        [SerializeField, Range(1, 10)] private float speed;

        #region Walk Types

        /// <summary>
        ///     Creates an instance of the walk component so the player can walk right
        /// </summary>
        /// <param name="player"> the player object </param>
        /// <returns> an instance of the walk class </returns>
        public Walk WalkToRight(GameObject player)
        {
            var walkRight = player.AddComponent<Walk>();
            CreateWalk(walkRight, Vector2.right, KeyBindingActions.WalkRightKey);
            return walkRight;
        }

        /// <summary>
        ///     Creates an instance of the walk component so the player can walk right 
        /// </summary>
        /// <param name="player"> the player object </param>
        /// <returns> an instance of the walk class </returns>
        public Walk WalkToLeft(GameObject player)
        {
            var walkLeft = player.AddComponent<Walk>();
            CreateWalk(walkLeft, Vector2.left, KeyBindingActions.WalkLeftKey);
            return walkLeft;
        }
        
        #endregion

        /// <summary>
        ///     Creates a type of walk 
        /// </summary>
        /// <param name="walk"> walk type </param>
        /// <param name="dir"> direction of the walk </param>
        /// <param name="key"> key for the walk </param>
        private void CreateWalk(Walk walk, Vector2 dir, KeyBindingActions key)
        {
            walk.Speed = speed;
            walk.Direction = dir;
            walk.ActionKey = key;
        }
    }
}