using Game.Scripts.Core_LevelManagement.EventManagement;
using UnityEngine;

namespace Game.Scripts.KeyBindings
{

    public class AllKeyUnlock : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            EventManager.Instance.OnRemoveAbilities();
            Destroy(gameObject);
        }
    }
}