using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdelState : IPlayerState
{
    PlayerStatecontroller state;
    KeyInput input;
    PushObject push;
    PlayerClimb climb;
    StageManager sm;
    public PlayerState State => PlayerState.Idle;
    public IdelState(PlayerStatecontroller _state, KeyInput _input,PushObject _push, PlayerClimb _climb, StageManager _sm) 
    {
        state = _state;
        input = _input; 
        push = _push;
        climb = _climb;
        sm = _sm;
    }
    public void Entry() { /*...*/ }
    public void Update() { StartAnimation(); }
    public void FixedUpdate() { }
    public void Exit() { /*...*/ }

    private void StartAnimation()
    {
        if(!sm.IsTimeLine){ Debug.Log("a"); return;  }

        if(input.InputMove != Vector2.zero)
        {
            state.Move();
        }
        if(push.IsPush)
        {
            state.Push();
        }
        if(climb.ClimbCheck() && input.ClimbAction)
        {
            state.Climb();
        }
    }
}
