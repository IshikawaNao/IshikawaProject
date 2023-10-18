using UnityEngine;

public class MoveState : IPlayerState
{
    PlayerStatecontroller state;
    Rigidbody rb;
    Animator anim;
    KeyInput input;
    GameObject player;
    PushObject push;
    PlayerClimb climb;

    const float speed = 15;
    const float MaxSpeed = 15;
    const float force = 7;
    const float rotationSpeed = 0.3f;


    public PlayerState State => PlayerState.Move;
    public void Entry() { /*...*/ }
    public void Update() 
    {
        SwitchState();
    }
    public void FixedUpdate() 
    {
        AnimationCahge();
        Walk();
    }
    public void Exit() { anim.SetFloat("Speed", 0);  rb.velocity = Vector3.zero; }

    public MoveState(PlayerStatecontroller _state,Rigidbody _rb, Animator _anim, KeyInput _input,GameObject _player, PushObject _push, PlayerClimb _climb) 
    { 
        state = _state; 
        rb = _rb;
        anim = _anim; 
        input = _input;
        player = _player;
        push = _push;
        climb = _climb;
    }


    void Walk() 
    {
        //カメラ方向　
        Camera mc = Camera.main;
        var inputValue = input.InputMove;
        Vector3 cameraForward = Vector3.Scale(mc.transform.forward, new Vector3(1, 0, 1));
        Vector3 moveForward = cameraForward * inputValue.y + mc.transform.right * inputValue.x;
        Vector3 moveVector = moveForward.normalized * speed;   //移動速度
        Vector3 vector = new Vector3(rb.velocity.x, 0, rb.velocity.z);              //velocity
        rb.AddForce((moveVector - vector) * force, ForceMode.Acceleration);

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            player.transform.rotation = Quaternion.Slerp(Quaternion.LookRotation(moveVector), player.transform.rotation, rotationSpeed);
        }
        
    }

    void AnimationCahge()
    {
        // 現在の移動速度
        var velocity = rb.velocity.magnitude;
        // アニメーション
        anim.SetFloat("Speed", velocity / MaxSpeed); 
    }

    void SwitchState()
    {
        if (input.InputMove == Vector2.zero)
        {
            state.Idle();
        }
        if(push.IsPush)
        {
            state.Push();
        }
        if (climb.ClimbCheck() && input.ClimbAction)
        {
            state.Climb();
        }
    }
}
