using System.Collections.Generic;
using Game.Scripts.AbilitiesSystem.AbilityHandler;
using Game.Scripts.Core_LevelManagement.EventManagement;
using UnityEngine;

namespace Game.Scripts.AbilitiesSystem.Actions
{
    public class UnlockAbility : MonoBehaviour
    {
        [SerializeField] private List<AbilityType> abilities; 
        private const int StandardTriggerAmount = 1, DoubleJumpTriggerAmount = 2;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.name.Equals("Player")) return;
            UnlockAbilities();
            UnityAnalyticsManager.PickedUpAbility();
            Destroy(gameObject);
        }

        /// <summary>
        ///     Unlocks certain abilities from a list 
        /// </summary>
        private void UnlockAbilities()
        {
            foreach (var a in abilities)
            {
                if (a != AbilityType.DoubleJump) AbilityEventManager.OnAbilityPickUp(a, StandardTriggerAmount);
                if (a == AbilityType.DoubleJump) AbilityEventManager.OnAbilityPickUp(a, DoubleJumpTriggerAmount);
            }
        }
    }
}