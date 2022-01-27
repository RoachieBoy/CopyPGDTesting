using System.Collections.Generic;
using Game.Scripts.AbilitiesSystem.AbilityHandler;
using UnityEngine;

namespace Game.Scripts.AbilitiesSystem.Actions
{
    public class DisableAbility : MonoBehaviour
    {
        [SerializeField] private List<AbilityType> abilities;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.name.Equals("Player")) return;

            foreach (var a in abilities)
            {
                AbilityEventManager.OnAbilityDisable(a);
            }

            Destroy(gameObject);
        }
    }
}