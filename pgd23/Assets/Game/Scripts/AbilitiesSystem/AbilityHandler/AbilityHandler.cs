using System.Collections.Generic;
using Game.Scripts.AbilitiesSystem.Abilities;
using Game.Scripts.Core_LevelManagement.EventManagement;
using Game.Scripts.KeyBindings;
using UnityEngine;
using UnityEngine.Analytics;

namespace Game.Scripts.AbilitiesSystem.AbilityHandler
{
    public class AbilityHandler : MonoBehaviour
    {
        [Tooltip("UI for unlocking controls")]
        [SerializeField] private ControlContainer controlContainer;
        
        [Tooltip("This can be found in the essentials section of the hierarchy and contains all ability settings")]
        [SerializeField] private GameObject abilityManager;

        private readonly Dictionary<AbilityType, AbilityClass> _abilities = new Dictionary<AbilityType, AbilityClass>(); 

        #region EventFunctions

        private void Awake()
        {
            AddAllAbilitiesToDictionary();
            DisableAllAbilities();
        }

        private void Start()
        {
            EventManager.Instance.onRemoveAllAbilities += DisableAllAbilities;
            controlContainer = FindObjectOfType<ControlContainer>();
        }

        private void OnEnable()
        {
            AbilityEventManager.onAbilityPickUp += EnableAbility;
            AbilityEventManager.onAbilityDisable += DisableAbility; 
        }

        private void OnDestroy()
        {
            EventManager.Instance.onRemoveAllAbilities -= DisableAllAbilities;
        }

        private void OnDisable()
        {
            AbilityEventManager.onAbilityDisable -= DisableAbility; 
            AbilityEventManager.onAbilityPickUp -= EnableAbility;
        }

        #endregion

        #region Abilities

        /// <summary>
        ///     Adds the abilities to the player
        /// </summary>
        private void AddAllAbilitiesToDictionary()
        {
            _abilities.Add(AbilityType.Crouch, abilityManager.GetComponent<Crouch>());

            _abilities.Add(AbilityType.Jump, abilityManager.GetComponent<Jump>());
            _abilities.Add(AbilityType.DoubleJump, abilityManager.GetComponent<Jump>());

            _abilities.Add(AbilityType.DashRight, abilityManager.GetComponent<DashFactory>().DashRight(gameObject));
            _abilities.Add(AbilityType.DashLeft, abilityManager.GetComponent<DashFactory>().DashLeft(gameObject));

            _abilities.Add(AbilityType.WalkRight, abilityManager.GetComponent<WalkFactory>().WalkToRight(gameObject));
            _abilities.Add(AbilityType.WalkLeft, abilityManager.GetComponent<WalkFactory>().WalkToLeft(gameObject));

            //sets the parent player object to the game object that possesses this script 
            foreach (var pair in _abilities) pair.Value.PlayerObject = gameObject;
        }

        /// <summary>
        ///     Enables an ability
        /// </summary>
        /// <param name="abilityType"> which ability to enable </param>
        /// <param name="amountOfTriggers"> how much triggers does this ability have </param>
        private void EnableAbility(AbilityType abilityType, int amountOfTriggers)
        {
            if (!_abilities.TryGetValue(abilityType, out var ability)) return;
            ability.enabled = true;
            ability.Enable(amountOfTriggers);
            
            var abilityAdded = Analytics.CustomEvent("Ability Enabled");
            
            Debug.Log(abilityAdded);
            
            ActivateAbilityKeyUI(InputManager.Instance.GetBindingAction(abilityType));
        }

        /// <summary>
        ///     Disables a single ability
        /// </summary>
        /// <param name="abilityType"> which ability to disable </param>
        private void DisableAbility(AbilityType abilityType)
        {
            if (!_abilities.TryGetValue(abilityType, out var ability)) return;
            ability.enabled = false;
            ability.Disable();
        }

        /// <summary>
        ///     Removes all added abilities from the player
        /// </summary>
        private void DisableAllAbilities()
        {
            foreach (var pair in _abilities) DisableAbility(pair.Key);
        }

        /// <summary>
        ///     Activates the UI of the abilities on screen
        /// </summary>
        /// <param name="key"></param>
        private void ActivateAbilityKeyUI(KeyBindingActions key)
        {
            if (controlContainer != null) controlContainer.ActivateKey(key);
        }

        #endregion
    }
}