using System.Collections.Generic;
using UnityEngine;

public class PlayerStatecontroller
{
    IPlayerState _currentState;    // ���݂̏��
    IPlayerState _previousState;   // ���O�̏��

    KeyInput input;
    GameObject playerObj;
    Rigidbody rb;
    CapsuleCollider col;
    Animator anim;
    PushObject push;
    PlayerClimb climb;
    StageManager sm;

    /// <summary> �X�e�[�g�Ǘ��Z�b�g�A�b�v </summary>
    public PlayerStatecontroller(KeyInput _input, GameObject _playerObj, Rigidbody _rb, CapsuleCollider _col, Animator _anim, PushObject _push, PlayerClimb _climb, StageManager _sm)
    {
        input = _input;
        playerObj = _playerObj;
        rb = _rb;
        col = _col;
        anim = _anim;
        push = _push;
        climb = _climb;
        sm = _sm;
    }

    // ��Ԃ̃e�[�u��
    Dictionary<PlayerState, IPlayerState> _stateTable;

    public void Init(PlayerController player, PlayerState initState)
    {
        if (_stateTable != null) return; // ���x�����������Ȃ�

        // �e��ԑI�N���X�̏�����
        Dictionary<PlayerState, IPlayerState> table = new()
        {
            { PlayerState.Idle, new IdelState(this, input,push,climb,sm) },
            { PlayerState.Move, new MoveState(this, rb, anim, input, playerObj,push,climb) },
            { PlayerState.Climb, new ClimbState(this, anim,playerObj, col, climb) },
            { PlayerState.Push, new PushState(this, rb, anim, input, playerObj,push) }
        };
        _stateTable = table;
        _currentState = _stateTable[initState];
    }

    // �ʂ̏�ԂɕύX����
    public void ChangeState(PlayerState next)
    {
        if (_stateTable == null) return; // ���������̎��͖���
        Debug.Log(_stateTable[next]);
        if (_currentState == null || _currentState.State == next)
        {
            return; // ������Ԃɂ͑J�ڂ��Ȃ�
        }
        // �ޏ� �� ���ݏ�ԕύX �� ����
        var nextState = _stateTable[next];
        _previousState = _currentState;
        _previousState?.Exit();
        _currentState = nextState;
        _currentState.Entry();
    }

    // �X�e�[�g�؂�ւ�
    public void Idle() => ChangeState(PlayerState.Idle);
    public void Move() => ChangeState(PlayerState.Move);
    public void Climb() => ChangeState(PlayerState.Climb);
    public void Push() => ChangeState(PlayerState.Push);

    // ���݂̏�Ԃ�Update����
    public void Update() => _currentState?.Update();
    public void FixedUpdate() => _currentState?.FixedUpdate();
}
