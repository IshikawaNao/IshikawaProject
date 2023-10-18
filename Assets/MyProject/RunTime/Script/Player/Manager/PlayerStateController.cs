using System.Collections.Generic;
using UnityEngine;

public class PlayerStatecontroller
{
    IPlayerState _currentState;    // 現在の状態
    IPlayerState _previousState;   // 直前の状態

    KeyInput input;
    GameObject playerObj;
    Rigidbody rb;
    CapsuleCollider col;
    Animator anim;
    PushObject push;
    PlayerClimb climb;
    StageManager sm;

    /// <summary> ステート管理セットアップ </summary>
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

    // 状態のテーブル
    Dictionary<PlayerState, IPlayerState> _stateTable;

    public void Init(PlayerController player, PlayerState initState)
    {
        if (_stateTable != null) return; // 何度も初期化しない

        // 各状態選クラスの初期化
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

    // 別の状態に変更する
    public void ChangeState(PlayerState next)
    {
        if (_stateTable == null) return; // 未初期化の時は無視
        Debug.Log(_stateTable[next]);
        if (_currentState == null || _currentState.State == next)
        {
            return; // 同じ状態には遷移しない
        }
        // 退場 → 現在状態変更 → 入場
        var nextState = _stateTable[next];
        _previousState = _currentState;
        _previousState?.Exit();
        _currentState = nextState;
        _currentState.Entry();
    }

    // ステート切り替え
    public void Idle() => ChangeState(PlayerState.Idle);
    public void Move() => ChangeState(PlayerState.Move);
    public void Climb() => ChangeState(PlayerState.Climb);
    public void Push() => ChangeState(PlayerState.Push);

    // 現在の状態をUpdateする
    public void Update() => _currentState?.Update();
    public void FixedUpdate() => _currentState?.FixedUpdate();
}
