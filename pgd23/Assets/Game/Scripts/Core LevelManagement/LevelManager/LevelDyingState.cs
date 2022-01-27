public class LevelDyingState : LevelBaseState
{
    public override LevelStateManager Level { get; set; }

    public void Start()
    {
        Level = LevelStateManager.current;
    }

    public override void EnterState()
    {
    }

    public override void LeaveState()
    {
    }

    public override void UpdateState()
    {
    }
}