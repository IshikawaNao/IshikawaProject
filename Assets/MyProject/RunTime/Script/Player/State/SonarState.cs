using UnityEngine;

public class SonarState : IPlayerState
{
    PlayerStatecontroller state;
    SonarEffect sonar;
    Rigidbody rb;
    Animator anim;

    public PlayerState State => PlayerState.Sonar;
    public void Entry() 
    {
        sonar.Sonar();  
        rb.velocity = Vector3.zero; 
        anim.SetFloat("Speed", 0); 
        anim.SetInteger("MovementState", 0); 
    }
    public void Update() { Play(); }
    public void FixedUpdate() { }
    public void Exit() { }

    public SonarState(PlayerStatecontroller _state, SonarEffect _sonar,Rigidbody _rb, Animator _anim) 
    {
        state = _state;
        sonar = _sonar;
        rb = _rb;
        anim = _anim;
    }

    private void Play() 
    {
        if (sonar.IsOnSonar)
        {
            return;
        }
         state.Idle();
    }
}
