using System;
using Game.Scripts.AbilitiesSystem.AbilityHandler;
using UnityEngine;

namespace Game.Scripts.KeyBindings
{
    [CreateAssetMenu(fileName = "Keybindings", menuName = "Keybindings")]
    public class KeyBinder : ScriptableObject
    {
        [Serializable]
        public class KeybindingCheck
        {
            public KeyBindingActions keyBindingAction;
            public KeyCode keyCode;
            public AbilityType type; 
        }

        public KeybindingCheck[] keybindingChecks;
    }
}