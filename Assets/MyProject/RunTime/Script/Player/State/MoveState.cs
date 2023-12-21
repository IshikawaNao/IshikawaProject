using SoundSystem;
using UnityEngine;

public class MoveState : IPlayerState
{
    PlayerStatecontroller state;
    Rigidbody rb;
    Animator anim;
    KeyInput input;
    GameObject player;
    RayHitDetection rayHitDetection;
    SoundManager soundManager;
    MainUIManager mainUIManager;

    const float Speed = 5;
    const float MaxSpeed = 10;
    const float Force = 2f;
    const float Friction = 3f; 
    const float RotationSpeed = 0.3f;

    public PlayerState State => PlayerState.Move;
    public void Entry() { anim.SetInteger("MovementState", 0); }
    public void Update() { SwitchState(); }
    public void FixedUpdate() 
    {
        AnimationCahge();
        Walk();
    }
    public void Exit() { soundManager.StopSE(); }

    public MoveState(PlayerStatecontroller _state,Rigidbody _rb, Animator _anim, KeyInput _input,GameObject _player, 
        RayHitDetection _rayHitDetection, SoundManager _soundManager, MainUIManager _mainUIManager) 
    { 
        state = _state; 
        rb = _rb;
        anim = _anim; 
        input = _input;
        player = _player;
        rayHitDetection = _rayHitDetection;
        soundManager = _soundManager;
        mainUIManager = _mainUIManager;
    }


    void Walk() 
    {
        // �J�����̕������擾
        Camera cm = Camera.main;
        var inputValue = input.InputMove;
        var cameraForward = Vector3.Scale(cm.transform.forward, new Vector3(1, 0, 1)).normalized;

        // �J�����̕��p�Ɠ��͂��v�Z
        var inputVector = cameraForward * inputValue.y + cm.transform.right * inputValue.x;
        var moveVector = Speed * inputVector.normalized;

        // ���͂��Ȃ��ꍇ�͖��C�������đ��x������������
        if (inputValue.magnitude < 0.01f)
        {
            if (soundManager.IsSEPlay()) { soundManager.StopSE(); } 
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime * Friction);
        }
        else
        {
            // ���͂�����ꍇ�͈ړ�
            Vector3 targetVelocity = Force * moveVector;
            rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, Time.deltaTime * 10f);
        }

        // �L�����N�^�[�̌�����i�s������
        if (inputVector != Vector3.zero)
        {
            player.transform.rotation = Quaternion.Slerp(Quaternion.LookRotation(moveVector), 
                player.transform.rotation, RotationSpeed);
        }
    }

    void AnimationCahge()
    {
        // ���݂̈ړ����x
        var velocity = rb.velocity.magnitude;
        // �A�j���[�V����
        anim.SetFloat("Speed", velocity / MaxSpeed);
        if (!soundManager.IsSEPlay()) { soundManager.PlayOneShotSe((int)SEList.Run); }
    }

    void SwitchState()
    {
        if (rb.velocity.magnitude == 0) { state.Idle(); }
        if (rayHitDetection.IsTeleport() && input.InputTeleport) { state.Teleport(); }
        if (rayHitDetection.IsBeamRotate && input.BeamRotateAction) { rayHitDetection.BeamRotate(); }
        if (rayHitDetection.CanPush() && input.PushAction){ state.Push(); }
        if (rayHitDetection.ClimbCheck() && input.ClimbAction){ state.Climb(); }
        if (!rayHitDetection.IsGround()) { state.Gliding(); }
        if (input.SonarAction) { state.Sonar(); }
        if (mainUIManager.IsPauseOpen) { state.Pause(); }
    }
}
