using UnityEngine;

public class PushState : IPlayerState
{
    // �v����
    const float speed = 2.7f;

    Vector3 checkVec = new(1, 0, 1);

    PlayerStatecontroller state;
    Rigidbody rb;
    GameObject player;
    Animator anim;
    KeyInput input;
    PushObject push;
    public PlayerState State => PlayerState.Push;
    public void Entry() { /*...*/ }
    public void Update()
    {
        SwitchState();
    }
    public void FixedUpdate()
    {
        var move = input.InputMove;
        PushMoveAnimator(move);
        push.Move(move);
        ObjectMove(move);
    }
    public void Exit() { /*...*/ }

    public PushState(PlayerStatecontroller _state , Rigidbody _rb,  Animator _anim, KeyInput _input, GameObject _player, PushObject _push)
    {
        state = _state;
        rb = _rb;
        player = _player;
        anim = _anim;
        input = _input;
        push = _push;
    }

    // �A�j���[�V�����̊Ǘ�
    void PushMoveAnimator(Vector2 move)
    {
        anim.SetFloat("BlendX", move.x);
        anim.SetFloat("BlendY", move.y);
    }
    // �v�b�V���I�u�W�F�N�g�ړ�
    public void ObjectMove(Vector2 move)
    {
        if (push.IsPush)
        {
            Vector3 playerForward = Vector3.Scale(player.transform.forward, checkVec);
            Vector3 moveForward = playerForward * move.y + player.transform.right * move.x;
            Vector3 moveVector = moveForward.normalized * speed;   //�ړ����x
            rb.velocity = moveVector;
        }
    }

    void SwitchState()
    {
        if(!push.IsPush)
        {
            state.Idle();
        }
    }
}
