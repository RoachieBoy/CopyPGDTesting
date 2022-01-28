using UnityEngine;

namespace Game.Scripts.GameObjects.BreakableWall
{
    public class BrokenPiece : MonoBehaviour
    {
        [SerializeField] private float minimumSurvivalTime;

        /// <summary>
        ///     Checks every frame if a broken piece is in the bounds of the camera. If not, then it deletes it
        /// </summary>
        private void Update()
        {
            minimumSurvivalTime -= Time.deltaTime;

            if (minimumSurvivalTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
