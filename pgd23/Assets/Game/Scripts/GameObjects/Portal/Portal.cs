using Game.Scripts.AbilitiesSystem.AbilityHandler;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 1.25f;
    [SerializeField] private ParticleSystem _useParticle;

    public void UsePortal(GameObject obj)
    {
        var objectRigidBody = obj.GetComponent<Rigidbody2D>();
        // save player speed
        var mag = objectRigidBody.velocity.magnitude;
        // Set new position of player
        obj.transform.position = transform.position + transform.right * 2;
        // Reset player velocity to match portal direction
        objectRigidBody.velocity = transform.right * mag * speedMultiplier;

        obj.GetComponent<PlayerController>()?.UpXLimit();
    }

    public void CollideWithPortal()
    {
        if (_useParticle != null && !_useParticle.isPlaying)
        {
            _useParticle.Play();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 2);
    }
}
