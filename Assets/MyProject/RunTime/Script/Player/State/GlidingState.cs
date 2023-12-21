using UnityEngine;

public class GlidingState : IPlayerState
{
    PlayerStatecontroller state;
    Animator anim;
    RayHitDetection rayHitDetection;

    public PlayerState State => PlayerState.Move;
    public void Entry() { anim.SetInteger("MovementState", 3); }
    public void Update() { SwitchState(); }
    public void FixedUpdate() { }
    public void Exit() { /*rb.velocity = Vector3.zero; */}

    public GlidingState(PlayerStatecontroller _state, Animator _anim, RayHitDetection _rayHitDetection)
    {
        state = _state;
        anim = _anim;
        rayHitDetection = _rayHitDetection;
    }

    private void SwitchState()
    {
        if (rayHitDetection.IsGround()) { anim.SetTrigger("landing"); state.Idle(); }
    }
}
