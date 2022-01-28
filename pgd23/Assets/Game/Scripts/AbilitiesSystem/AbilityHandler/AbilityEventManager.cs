using System;

namespace Game.Scripts.AbilitiesSystem.AbilityHandler
{
    public static class AbilityEventManager 
    {
        /// <summary>
        ///     Event used to store the action on picking up an ability 
        /// </summary>
        public static event Action<AbilityType, int> onAbilityPickUp;

        /// <summary>
        ///     What happens when a player picks up an ability trigger
        /// </summary>
        /// <param name="abilityType"> which type of ability needs to be enabled </param>
        /// <param name="amountOfTriggers"> how many triggers does this ability have </param>
        public static void OnAbilityPickUp(AbilityType abilityType, int amountOfTriggers)
        {
            onAbilityPickUp?.Invoke(abilityType, amountOfTriggers);
            
            
        }

        public static event Action<AbilityType> onAbilityDisable;

        /// <summary>
        ///     Disables a specific ability and its triggers 
        /// </summary>
        /// <param name="abilityType"> type of ability </param>
        public static void OnAbilityDisable(AbilityType abilityType) => onAbilityDisable?.Invoke(abilityType); 
    }
}
