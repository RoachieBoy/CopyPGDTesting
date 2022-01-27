using Game.Scripts.Core_LevelManagement.EventManagement;
using UnityEngine;

namespace Game.Scripts.GameObjects.ElevatorSystem
{
    public class ElevatorController : MonoBehaviour
    {
        #region Serialized_Fields

        [Header("Object Transforms")] 
        [SerializeField] private Transform objectToTransform;

        [SerializeField] private Transform desiredTransform;

        [Header("General Elevator Settings")]
        //speed at which object will move 
        [SerializeField] private float liftSpeed;

        //checks if the elevator is working or not 
        [SerializeField] private bool active;

        #endregion

        //starting and ending positions of the object 
        private Vector3 _endPos, _startPos, _nextPos;

        #region Logic

        private void Start()
        {
            EventManager.Instance.onElevatorEnter += OnElevatorGo;
            EventManager.Instance.onElevatorExit += OnElevatorReturn;

            _endPos = desiredTransform.localPosition;
            _startPos = objectToTransform.localPosition;

            active = false;
        }

        private void Update()
        {
            if (active) Move();
        }

        private void OnDisable()
        {
            EventManager.Instance.onElevatorEnter -= OnElevatorGo;
            EventManager.Instance.onElevatorExit -= OnElevatorReturn;
        }

        /// <summary>
        ///     Makes the elevator trigger its movement
        /// </summary>
        private void OnElevatorGo()
        {
            _nextPos = _endPos;
            active = true;
        }

        /// <summary>
        ///     Makes the elevator return to start position
        /// </summary>
        private void OnElevatorReturn()
        {
            _nextPos = _startPos;
        }

        /// <summary>
        ///     Moves the elevator upwards with a certain lift speed
        /// </summary>
        private void Move()
        {
            objectToTransform.localPosition = Vector3.MoveTowards(objectToTransform.localPosition, _nextPos,
                liftSpeed * Time.deltaTime);
        }

        #endregion
    }
}