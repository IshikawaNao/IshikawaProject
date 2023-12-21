using SoundSystem;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatecontroller
{
    IPlayerState _currentState;    // ���݂̏��
    public  IPlayerState CurrentState { get { return _currentState; } }
    IPlayerState _previousState;   // ���O�̏��

    KeyInput input;
    SoundManager soundManager;
    GameObject playerObj;
    Rigidbody rb;
    CapsuleCollider col;
    Animator anim;
    RayHitDetection rayHitDetection;
    StageManager sm;
    GameObject teleportEfect;
    SonarEffect sonar;
    MainUIManager mainUImanager;

    /// <summary> �X�e�[�g�Ǘ��Z�b�g�A�b�v </summary>
    public PlayerStatecontroller(KeyInput _input, SoundManager _soundManager, GameObject _playerObj, Rigidbody _rb, CapsuleCollider _col,
        Animator _anim, RayHitDetection _rayHitDetection,StageManager _sm, GameObject _teleportEfect, SonarEffect _sonar, MainUIManager _mainUImanager)
    {
        input = _input;
        soundManager = _soundManager;
        playerObj = _playerObj;
        rb = _rb;
        col = _col;
        anim = _anim;
        rayHitDetection = _rayHitDetection;
        sm = _sm;
        teleportEfect = _teleportEfect;
        sonar = _sonar;
        mainUImanager = _mainUImanager;
    }

    // ��Ԃ̃e�[�u��
    Dictionary<PlayerState, IPlayerState> _stateTable;

    public void Init(PlayerState initState)
    {
        if (_stateTable != null) return; // ���x�����������Ȃ�

        // �e��ԑI�N���X�̏�����
        Dictionary<PlayerState, IPlayerState> table = new()
        {
            { PlayerState.Idle, new IdelState(this, input,rayHitDetection,sm,anim,mainUImanager) },
            { PlayerState.Move, new MoveState(this, rb, anim, input, playerObj, rayHitDetection, soundManager, mainUImanager) },
            { PlayerState.Climb, new ClimbState(this, anim,playerObj, col, rayHitDetection) },
            { PlayerState.Push, new PushState(this, rb, anim, input, playerObj,rayHitDetection, soundManager) },
            { PlayerState.Gliding, new GlidingState(this, anim, rayHitDetection) },
            { PlayerState.Sonar, new SonarState(this, sonar, rb, anim) },
            { PlayerState.Teleport, new TeleportState(this, anim, teleportEfect, rayHitDetection, rb) },
            { PlayerState.Pause, new PauseState(this, rb, anim, mainUImanager) },
        };
        _stateTable = table;
        _currentState = _stateTable[initState];
    }

    // �ʂ̏�ԂɕύX����
    public void ChangeState(PlayerState next)
    {
        if (_stateTable == null) return; // ���������̎��͖���
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

    public void BackState()
    {
        if(_currentState == _previousState)  return; // ������ԂɑJ�ڂ��Ȃ�
        _currentState?.Exit();
        var next = _previousState;
        _previousState = _currentState;
        _currentState = next;
        _currentState.Entry();
    }

    // �X�e�[�g�؂�ւ�
    public void Idle() => ChangeState(PlayerState.Idle);
    public void Move() => ChangeState(PlayerState.Move);
    public void Climb() => ChangeState(PlayerState.Climb);
    public void Push() => ChangeState(PlayerState.Push);
    public void Gliding() => ChangeState(PlayerState.Gliding);
    public void Sonar() => ChangeState(PlayerState.Sonar);
    public void Teleport() => ChangeState(PlayerState.Teleport);
    public void Pause() => ChangeState(PlayerState.Pause);

    // ���݂̏�Ԃ�Update����
    public void Update() => _currentState?.Update();
    public void FixedUpdate() => _currentState?.FixedUpdate();
}
