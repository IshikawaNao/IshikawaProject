using UnityEngine;

public class PauseState : IPlayerState
{
    PlayerStatecontroller state;
    Animator anim;
    Rigidbody rb;
    MainUIManager mainUIManager;

    Vector3 velocity;

    private float currentAnimationTime = 0;
    int openAnimationKey = Animator.StringToHash("Open");

    public PlayerState State => PlayerState.Pause;
    public void Entry() { Pause(); }
    public void Update(){ Play(); }
    public void FixedUpdate() { }
    public void Exit() {  }

    public PauseState(PlayerStatecontroller _state, Rigidbody _rb, Animator _anim, MainUIManager _pause)
    {
        state = _state;
        rb = _rb;
        anim = _anim;
        mainUIManager = _pause;
    }

    
    public void Play()
    {
        // ポーズ画面が開いている間は返す
        if (mainUIManager.IsPauseOpen)　{ return; }

        anim.enabled = true;
        anim.Play(openAnimationKey, 0, currentAnimationTime);
        rb.velocity = velocity;
        state.BackState();
    }

    public void Pause()
    {
        velocity = rb.velocity;
        rb.velocity = Vector3.zero;
        anim.enabled = false;
        currentAnimationTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
