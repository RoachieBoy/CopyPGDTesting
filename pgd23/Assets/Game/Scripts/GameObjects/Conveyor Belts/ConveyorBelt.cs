using System;
using Game.Scripts.AbilitiesSystem.AbilityHandler;
using UnityEngine;
using static Game.Scripts.GameObjects.Conveyor_Belts.States.StatesConveyor;

namespace Game.Scripts.GameObjects.Conveyor_Belts
{
    public class ConveyorBelt : MonoBehaviour
    {
        [SerializeField] public States.StatesConveyor cs; 
        [SerializeField] private float speed = 1f;

        private void OnCollisionStay2D(Collision2D collision)
        {
            var otherRb = collision.gameObject.GetComponent<Rigidbody2D>();
            // If the object is the player, add the force
            if (collision.gameObject.GetComponent<PlayerController>())
            {
                otherRb.velocity += cs switch
                {
                    Left => new Vector2(-speed * Time.deltaTime, 0),
                    Right => new Vector2(speed * Time.deltaTime, 0),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            // If the object is not the player, set the speed
            else
            {
                otherRb.velocity = cs switch
                {
                    Left => new Vector2(-speed, 0),
                    Right => new Vector2(speed, 0),
                    _ => otherRb.velocity
                };
            }
        }
    }
}
