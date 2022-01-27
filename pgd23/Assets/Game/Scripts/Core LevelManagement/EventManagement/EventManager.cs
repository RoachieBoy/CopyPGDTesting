using System;
using Game.Scripts.Tools;
using UnityEngine;
// ReSharper disable All

namespace Game.Scripts.Core_LevelManagement.EventManagement
{
    public class EventManager : Singleton<EventManager>
    {
        #region ELEVATOR

        /// <summary>
        ///     Gets invoked when the player enters the elevator 
        /// </summary>
        public event Action onElevatorEnter;

        /// <summary>
        ///     Determines what happens when the player enters the elevator 
        /// </summary>
        public void OnElevatorEnter() => onElevatorEnter?.Invoke();

        /// <summary>
        ///     Gets invoked when a player exits an elevator 
        /// </summary>
        public event Action onElevatorExit;

        /// <summary>
        ///     Determines what happens when the player exits the elevator 
        /// </summary>
        public void OnElevatorExit() => onElevatorExit?.Invoke();

        #endregion

        #region ABILITIES

        /// <summary>
        ///     Gets invoked when all player abilities should be removed 
        /// </summary>
        public event Action onRemoveAllAbilities;

        /// <summary>
        ///     Determines what should happen when the abilities of the player are being removed
        /// </summary>
        public void OnRemoveAbilities() => onRemoveAllAbilities?.Invoke();

        public event Action<GameObject> onMovingPlatformCorrection;

        /// <summary>
        ///     A correctional method that fixes layering issues regarding moving platforms and the player jump
        /// </summary>
        /// <param name="obj"></param>
        public void OnMovingPlatformCorrection(GameObject obj) => onMovingPlatformCorrection?.Invoke(obj); 

        #endregion

        #region OBSTACLES

        /// <summary>
        ///     Gets invoked when screen shake should occur 
        /// </summary>
        public event Action<float, float> onTriggerShake;
        
        /// <summary>
        ///     Determines what should happen when the screen needs to shake 
        /// </summary>
        public void OnTriggerShake(float duration, float magnitude) => onTriggerShake?.Invoke(duration, magnitude); 

        #endregion

        #region TRANSITIONS

        /// <summary>
        ///     Fades the screen to black on trigger 
        /// </summary>
        public event Action onFadeOut;
        
        /// <summary>
        ///     Fades out of one level to the next 
        /// </summary>
        public void OnFadeOut() => onFadeOut?.Invoke();

        #endregion

        #region LEVELTRIGGERPOINTS

        public event Action onTrigger1;

        public void OnTrigger1() => onTrigger1?.Invoke();


        public event Action onTrigger2;

        public void OnTrigger2() => onTrigger2?.Invoke();

        public event Action onTriggerRocket;

        public void OnTriggerRocket() => onTriggerRocket?.Invoke();

        #endregion

        #region TRAMPOLINE

        /// <summary>
        ///     Action that determines player movement 
        /// </summary>
        public event Action onCanMove;
        
        /// <summary>
        ///     Determines if a player can move or not 
        /// </summary>
        public void OnCanMove() => onCanMove?.Invoke(); 

        #endregion

        #region PARTICLES

        /// <summary>
        ///     Activates a dust effect 
        /// </summary>
        public event Action onActivateDust;

        /// <summary>
        ///     Allows a player to have a dust particle effect at certain game moments 
        /// </summary>
        public void OnActivateDust() => onActivateDust?.Invoke();

        /// <summary>
        ///     Allows for a player to release a small dash particle system 
        /// </summary>
        public event Action onActivateDashParticle;

        /// <summary>
        ///     Activates the dash particle by invoking the correct action 
        /// </summary>
        public void OnActivateDashParticle() => onActivateDashParticle.Invoke();

        #endregion
    }
}