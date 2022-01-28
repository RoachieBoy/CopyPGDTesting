using Game.Scripts.Core_LevelManagement.EventManagement;
using Game.Scripts.Tools;
using UnityEngine;

namespace Game.Scripts.GameObjects.Doors
{
    public class DoorHandler : MonoBehaviour
    {
        [Header("Doors")]
        [SerializeField] private MoveObject upperDoor;
        [SerializeField] private MoveObject lowerDoor;

        private Vector3 _upperDoorPosition;
        private Vector3 _lowerDoorPosition;
        private float _timeLeft;

        [SerializeField] private float waitTime;
        [SerializeField] private bool opened;
        [SerializeField] private bool listenToTrigger;
        [SerializeField] private Vector3 upperDoorMove;
        [SerializeField] private Vector3 lowerDoorMove;
    
        [SerializeField, Range(0f, 1f)] private float timeToComplete;

        private void Start()
        {
            _upperDoorPosition = upperDoor.transform.position;
            _lowerDoorPosition = lowerDoor.transform.position;
            if(listenToTrigger) EventManager.Instance.onTrigger2 += Move;
        }

        private void OnDisable()
        {
            if(listenToTrigger) EventManager.Instance.onTrigger2 -= Move;
        }

        private void Update()
        {
            _timeLeft -= Time.deltaTime;
        }

        private void Move()
        {
            if (_timeLeft > 0) return;
            if (!opened)
            {
                upperDoor.Move(upperDoorMove, timeToComplete, waitTime);
                lowerDoor.Move(lowerDoorMove, timeToComplete, waitTime);
                _timeLeft = timeToComplete;
            }
            else
            {
                var distance = Vector3.Distance(_upperDoorPosition, _upperDoorPosition + upperDoorMove);
                var timing = distance / upperDoorMove.magnitude * timeToComplete;
                upperDoor.Move(_upperDoorPosition - upperDoor.transform.position, timing);
                lowerDoor.Move(_lowerDoorPosition - lowerDoor.transform.position, timing);
                _timeLeft = timing;
            }
            opened = !opened;
        }
    }
}
