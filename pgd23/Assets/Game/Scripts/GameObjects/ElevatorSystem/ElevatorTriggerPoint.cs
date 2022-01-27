using Game.Scripts.Core_LevelManagement.EventManagement;
using UnityEngine;

namespace Game.Scripts.GameObjects.ElevatorSystem
{
    public class ElevatorTriggerPoint: MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.tag.Equals("Player")) return; 
            
            //sets the player as child object of the elevator 
            other.transform.SetParent(transform);
            EventManager.Instance.OnElevatorEnter();
            
            //fade screen 
            EventManager.Instance.OnFadeOut();
            EventManager.Instance.OnRemoveAbilities();
            
            //sets player velocity to zero
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}