using Game.Scripts.AbilitiesSystem.AbilityHandler;
using UnityEngine;
using static Game.Scripts.GameObjects.Obstacles.States.StatesConveyor;

namespace Game.Scripts.GameObjects.Obstacles
{
    public class ConveyorBelt : MonoBehaviour
    {
        [SerializeField] public States.StatesConveyor cs; 
        [SerializeField] private float speed = 1f;

        private void OnCollisionStay2D(Collision2D collision)
        {
            var otherRB = collision.gameObject.GetComponent<Rigidbody2D>();
            // If the object is the player, add the force
            if (collision.gameObject.GetComponent<PlayerController>())
            {
                if (cs == Left) otherRB.velocity += new Vector2(-speed * Time.deltaTime, 0);
                if (cs == Right) otherRB.velocity += new Vector2(speed * Time.deltaTime, 0);
            }
            // If the object is not the player, set the speed
            else
            {
                if (cs == Left) otherRB.velocity = new Vector2(-speed, 0);
                if (cs == Right) otherRB.velocity = new Vector2(speed, 0);
            }
        }
    }
}
