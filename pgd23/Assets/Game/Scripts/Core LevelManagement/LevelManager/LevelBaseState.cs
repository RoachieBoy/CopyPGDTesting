using UnityEngine;

public abstract class LevelBaseState : MonoBehaviour
{
    public abstract LevelStateManager Level { get; set; }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void LeaveState();
}