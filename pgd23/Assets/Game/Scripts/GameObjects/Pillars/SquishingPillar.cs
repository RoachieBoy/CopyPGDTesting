using Game.Scripts.Core_LevelManagement.EventManagement;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.GameObjects.Obstacles
{
    public class SquishingPillar : MonoBehaviour
    {       
        private const float DistanceFromEnd = 0.15f;
        private const int SecondsCorrectionAmount = 1000; 

        [Header("Speed settings")]
        [SerializeField] private float acceleration;
        [SerializeField] private float liftSpeed;
        
        [Header("Transform settings")]
        [SerializeField] private Transform startPositionTransform;
        [SerializeField] private Transform endPositionTransform;
        
        [Header("Timing settings")]
        [SerializeField] private float durationOfWaitTop, durationOfWaitBottom;
        [SerializeField] private float StartWait;
        [SerializeField] private bool InSequence = false;
        [SerializeField] private int numberInSequence;
        [SerializeField] private float sequenceInterval;
        [SerializeField] private bool directStart = true;

        //time between each object in a sequence of objects 
        private readonly Stopwatch _intervalPause = new Stopwatch();

        //time between the platform starting and moving 
        private readonly Stopwatch _pauseStopwatch = new Stopwatch();

        // Whether the object waits to start or not
        private bool _hasStarted;

        //speed at which the object moves
        private float _movementSpeed;

        //saves start, end and next positions
        private Vector3 _startPos, _endPos, _nextPos;

        private void Start()
        {
            if(directStart) Activate();
            if (!directStart) EventManager.Instance.onTrigger2 += Activate;
            if (!InSequence) return;
            var parent = transform.parent;
            
            for(var i = 0; i < parent.childCount; i++)
            {
                if (parent.GetChild(i).gameObject != gameObject) continue;
                numberInSequence = i;
                return;
            }
        }

        private void OnDisable()
        {
            if (!directStart) EventManager.Instance.onTrigger2 -= Activate;
        }

        private void Activate()
        {
            _startPos = startPositionTransform.localPosition;
            _endPos = endPositionTransform.localPosition;
            _nextPos = _endPos;
            _intervalPause.Start();
        } 


        /// <summary>
        ///     Before starting the movement, checks if the sequence interval has elapsed
        /// </summary>
        private void WaitBeforeStart()
        {
            if (!(_intervalPause.ElapsedMilliseconds > (StartWait + numberInSequence * sequenceInterval) 
                    * SecondsCorrectionAmount)) return;
            
            _hasStarted = true;
            _intervalPause.Stop();
            _pauseStopwatch.Start();
        }
        
        private void Update()
        {
            if (!_hasStarted)
            {
                WaitBeforeStart();
                return; 
            }
            Move();
        }

        /// <summary>
        ///     Moves the obstacle to the correct position
        /// </summary>
        private void Move()
        {
            //if the object is heading to the end destination, move the object with an acceleration towards that destination
            if (_nextPos == _endPos)
            {
                if (_pauseStopwatch.ElapsedMilliseconds < durationOfWaitTop * SecondsCorrectionAmount) return;
                _movementSpeed += acceleration * Time.deltaTime;
                startPositionTransform.localPosition = Vector3.MoveTowards(startPositionTransform.localPosition,
                    _nextPos, _movementSpeed * Time.deltaTime);
            }
            else
            {
                //if the object is heading upwards, move it up with a lift speed 
                if (_pauseStopwatch.ElapsedMilliseconds < durationOfWaitBottom * SecondsCorrectionAmount) return;
                _movementSpeed = 0;
                startPositionTransform.localPosition = Vector3.MoveTowards(startPositionTransform.localPosition,
                    _nextPos, liftSpeed * Time.deltaTime);
            }

            CheckPosition();

            if (!(Vector3.Distance(startPositionTransform.localPosition, _nextPos) <= DistanceFromEnd)) return;
            ChangeTarget();
            //restarts the timer once the object has changed its target 
            _pauseStopwatch.Restart();
        }

        /// <summary>
        ///     Checks the position of the obstacle to determine the screen shake
        /// </summary>
        private void CheckPosition()
        {
            if (Vector2.Distance(startPositionTransform.localPosition, _endPos) <
                DistanceFromEnd && _nextPos == _endPos) GetComponent<ShakeHandler>().TriggerShake();
        }

        /// <summary>
        ///     Changes the target destination of the obstacle
        /// </summary>
        private void ChangeTarget()
        {
            //sets the next position equals to the either startPos or endPos depending on where the moving obstacle is
            _nextPos = _nextPos != _startPos ? _startPos : _endPos;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(endPositionTransform.position, startPositionTransform.localScale);
        }
    }
}