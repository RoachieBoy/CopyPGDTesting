using Game.Scripts.Core_LevelManagement.EventManagement;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    [Header("Doors")]
    [SerializeField] private MoveObject upperDoor;
    [SerializeField] private MoveObject lowerDoor;

    private Vector3 upperDoorPosition;
    private Vector3 lowerDoorPosition;

    private float timeLeft;

    [SerializeField] private float waitTime;

    [SerializeField] private bool opened;
    [SerializeField] private bool listenToTrigger;
    
    [SerializeField] private Vector3 upperDoorMove;
    [SerializeField] private Vector3 lowerDoorMove;
    
    [SerializeField, Range(0f, 1f)] private float timeToComplete;

    private void Start()
    {
        upperDoorPosition = upperDoor.transform.position;
        lowerDoorPosition = lowerDoor.transform.position;
        if(listenToTrigger) EventManager.Instance.onTrigger2 += Move;
    }

    private void OnDisable()
    {
        if(listenToTrigger) EventManager.Instance.onTrigger2 -= Move;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
    }

    public void Move()
    {
        if (timeLeft > 0) return;
        if (!opened)
        {
            upperDoor.Move(upperDoorMove, timeToComplete, waitTime);
            lowerDoor.Move(lowerDoorMove, timeToComplete, waitTime);
            timeLeft = timeToComplete;
        }
        else
        {
            var distance = Vector3.Distance(upperDoorPosition, upperDoorPosition + upperDoorMove);
            var timing = distance / upperDoorMove.magnitude * timeToComplete;
            upperDoor.Move(upperDoorPosition - upperDoor.transform.position, timing);
            lowerDoor.Move(lowerDoorPosition - lowerDoor.transform.position, timing);
            timeLeft = timing;
        }
        opened = !opened;
    }

}
