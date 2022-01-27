using UnityEngine;

namespace Game.Scripts.AbilitiesSystem.AbilityHandler
{
    public abstract class AbilityClass : MonoBehaviour
    {
        /// <summary>
        ///     Player object connected to the ability 
        /// </summary>
        public GameObject PlayerObject { get; set; }

        /// <summary>
        ///     The amount of triggers that an ability has linked to it 
        /// </summary>
        protected int AmountOfTriggers { get; private set; }

        /// <summary>
        ///     Enables the ability triggers
        /// </summary>
        /// <param name="triggers"> amount of triggers the ability possesses </param>
        public void Enable(int triggers) => AmountOfTriggers += triggers;

        /// <summary>
        ///     Disables the ability triggers 
        /// </summary>
        public void Disable() => AmountOfTriggers = 0;
        
    }
}