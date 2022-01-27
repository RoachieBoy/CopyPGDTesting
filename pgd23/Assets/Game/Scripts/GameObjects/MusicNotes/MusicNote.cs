using UnityEngine;

public class MusicNote : MonoBehaviour
{
    // Add movement based on music or whatsoever

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for colission with player
        if (collision.gameObject.tag != "Player") return; 
        // Spawn Particle System

        // Destroy gameObject
        Destroy(gameObject);
    }
}
