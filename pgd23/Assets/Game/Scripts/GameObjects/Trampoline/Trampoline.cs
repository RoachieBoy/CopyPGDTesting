using Game.Scripts.Core_LevelManagement.EventManagement;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Game.Scripts.GameObjects.Trampoline
{
    public class Trampoline : MonoBehaviour
    {
        [SerializeField] private float force = 500;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var rb = collision.collider.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * force);

            //prevents the player from moving 
            EventManager.Instance.OnCanMove();
        }

        private void OnDrawGizmos()
        {
            Debug.DrawLine(transform.position, transform.position + transform.right, Color.red);
        }
    }
}
