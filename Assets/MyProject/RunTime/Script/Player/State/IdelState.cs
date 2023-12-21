using UnityEngine;

public class IdelState : IPlayerState
{
    PlayerStatecontroller state;
    KeyInput input;
    RayHitDetection rayHitDetection;
    StageManager sm;
    Animator anim;
    MainUIManager mainUIManager;

    public PlayerState State => PlayerState.Idle;
    public void Entry() { anim.SetInteger("MovementState", 0); }
    public void Update() 
    {
        StartAnimation();
    }
    public void FixedUpdate() { }
    public void Exit() { /*...*/ }

    public IdelState(PlayerStatecontroller _state, KeyInput _input, RayHitDetection _rayHitDetection, StageManager _sm, Animator _anim, MainUIManager _mainUIManager) 
    {
        state = _state;
        input = _input;
        rayHitDetection = _rayHitDetection;
        sm = _sm;
        anim = _anim;
        mainUIManager = _mainUIManager;
    }

    private void StartAnimation()
    {
        if(!sm.IsTimeLine) { return;}

        if (input.InputMove != Vector2.zero) { state.Move(); }
        if (rayHitDetection.IsTeleport() && input.InputTeleport) { state.Teleport(); }
        if (rayHitDetection.IsBeamRotate && input.BeamRotateAction) { rayHitDetection.BeamRotate(); }
        if (rayHitDetection.CanPush() && input.PushAction) { state.Push(); }
        if (rayHitDetection.ClimbCheck() && input.ClimbAction) { state.Climb(); }
        if (!rayHitDetection.IsGround()) { state.Gliding(); }
        if (input.SonarAction) { state.Sonar();  }
        if (mainUIManager.IsPauseOpen) { state.Pause(); }
    }
}
