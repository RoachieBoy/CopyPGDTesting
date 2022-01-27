using UnityEngine;

public class hangingPlatform : MonoBehaviour
{
    [SerializeField] private HingeJoint2D joint;
    [SerializeField] private Transform player;

    [SerializeField] private Vector2 offset;
    
    [SerializeField] private float distanceToTrigger;
    private void Update()
    {
        if (Vector2.Distance(player.position, transform.position + (Vector3)offset) < distanceToTrigger) Release();
    }

    private void Release()
    {
        joint.enabled = false;
        joint.transform.parent = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)offset, distanceToTrigger);
    }
}
