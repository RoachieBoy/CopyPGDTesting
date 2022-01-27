using System;
using System.Collections.Generic;
using Game.Scripts.AbilitiesSystem.AbilityHandler;
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