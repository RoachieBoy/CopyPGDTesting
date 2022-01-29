using Game.Scripts.Core_LevelManagement.EventManagement;
using UnityEngine;

namespace Game.Scripts.GameObjects.MusicNotes
{
    public class MusicNote : MonoBehaviour
    {
        // Add movement based on music or whatsoever

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return; 
            // Spawn Particle System
            
            UnityAnalyticsManager.PickedUpNote();

            // Destroy gameObject
            Destroy(gameObject);
        }
    }
}
