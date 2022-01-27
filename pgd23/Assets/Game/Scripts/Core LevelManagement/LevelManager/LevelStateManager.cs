using System;
using UnityEngine;

public class LevelStateManager : MonoBehaviour
{
    public static LevelStateManager current;

    [Header("Level States")] [SerializeField]
    private GameObject PlayingStates;

    private LevelBaseState CurrentState;
    [NonSerialized] public LevelDyingState DyingState;

    [NonSerialized] public LevelPlayingState PlayingState;
    [NonSerialized] public LevelWinningState WinningState;

    private void Awake()
    {
        current = this;
    }

    public void Start()
    {
        PlayingState = PlayingStates.GetComponent<LevelPlayingState>();
        WinningState = PlayingStates.GetComponent<LevelWinningState>();
        DyingState = PlayingStates.GetComponent<LevelDyingState>();
        //switches to initial state 
        SwitchState(PlayingState);
    }

    private void Update()
    {
        CurrentState.UpdateState();
    }


    // Helper functions
    public void SwitchState(LevelBaseState state)
    {
        if (CurrentState) CurrentState.LeaveState();
        CurrentState = state;
        CurrentState.EnterState();
    }
}