using System.Linq;
using Game.Scripts.AbilitiesSystem.AbilityHandler;
using Game.Scripts.Tools;
using UnityEngine;

namespace Game.Scripts.KeyBindings
{
    public class InputManager : Singleton<InputManager>
    {
        [SerializeField] private KeyBinder keybindings;

        /// <summary>
        ///     Gets and returns the correct action key 
        /// </summary>
        /// <param name="key"> the key that is needed </param>
        /// <returns> the key code that is used </returns>
        public KeyCode GetActionKey(KeyBindingActions key)
        {
            return (from check in keybindings.keybindingChecks
                where check.keyBindingAction == key
                //returns the correct key code or none if none is found 
                select check.keyCode).FirstOrDefault();
        }

        /// <summary>
        ///     Gets and returns a key binding action related to an ability type
        /// </summary>
        /// <param name="type"> what type of ability is it </param>
        /// <returns> the correct key binding or nothing </returns>
        public KeyBindingActions GetBindingAction(AbilityType type)
        {
            return (from check in keybindings.keybindingChecks
                where check.type == type
                //returns the correct key code or none if none is found 
                select check.keyBindingAction).FirstOrDefault();
        }

        /// <summary>
        ///     Gets the right key and returns the input get key down method with that key
        /// </summary>
        /// <param name="key"> key to be used </param>
        /// <returns> Input.GetKeyDown method or none </returns>
        public bool GetKeyDown(KeyBindingActions key)
        {
            return (from check in keybindings.keybindingChecks
                where check.keyBindingAction == key
                select Input.GetKeyDown(check.keyCode)).FirstOrDefault();
        }

        /// <summary>
        ///     Gets the correct key and returns the input get key method with that key
        /// </summary>
        /// <param name="key"> key to be used </param>
        /// <returns> Input.GetKeyUp method or none </returns>
        public bool GetKey(KeyBindingActions key)
        {
            return (from check in keybindings.keybindingChecks
                where check.keyBindingAction == key
                select Input.GetKey(check.keyCode)).FirstOrDefault();
        }

        /// <summary>
        ///     Gets the correct key and returns the input get key up method with that key
        /// </summary>
        /// <param name="key"> key to be used </param>
        /// <returns> Input.GetKeyUp method or none </returns>
        public bool GetKeyUp(KeyBindingActions key)
        {
            return (from check in keybindings.keybindingChecks
                where check.keyBindingAction == key
                select Input.GetKeyUp(check.keyCode)).FirstOrDefault();
        }
    }
}