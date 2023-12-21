using SoundSystem;
using UnityEngine;

public class PushState : IPlayerState
{
    // 要調整
    const float PlayerSpeed = 2.7f;
    const float ObjectSpeed = 2.8f;

    Vector3 checkVec = new(1, 0, 1);

    PlayerStatecontroller state;
    Rigidbody rb;
    GameObject playerObj;
    Animator anim;
    KeyInput input;
    RayHitDetection rayHitDetection;
    SoundManager soundManager;

    public PlayerState State => PlayerState.Push;
    public void Entry() { anim.SetInteger("MovementState", 1); }
    public void Update()
    {
        PushAnimationChange();
    }
    public void FixedUpdate()
    {
        var move = input.InputMove;
        PushMoveAnimator(move);
        ObjectMove(move);
    }
    public void Exit() { /*...*/ }

    public PushState(PlayerStatecontroller _state , Rigidbody _rb,  Animator _anim, KeyInput _input, GameObject _playerObj, RayHitDetection _rayHitDetection, SoundManager _soundManager)
    {
        state = _state;
        rb = _rb;
        playerObj = _playerObj;
        anim = _anim;
        input = _input;
        rayHitDetection = _rayHitDetection;
        soundManager = _soundManager;
    }

    // アニメーションの管理
    void PushMoveAnimator(Vector2 move)
    {
        anim.SetFloat("BlendX", move.x);
        anim.SetFloat("BlendY", move.y);
    }
    // プッシュオブジェクト移動
    void ObjectMove(Vector2 move)
    {
        if (rayHitDetection.CanPush())
        {
            var playerForward = Vector3.Scale(playerObj.transform.forward, checkVec);
            var moveForward = playerForward * move.y + playerObj.transform.right * move.x;
            var playerMoveVector = moveForward.normalized * PlayerSpeed;   //移動速度
            var objectMoveVector = moveForward.normalized * ObjectSpeed;   //移動速度
            rb.velocity = playerMoveVector;
            rayHitDetection.PushRb.velocity = objectMoveVector;
        }
    }

    // プッシュが選択された時の処理
    void PushAnimationChange()
    {
        // Objectを移動中か
        if (rayHitDetection.CanPush() && input.PushAction)
        {
            anim.SetBool("IsObjectMove", true);
            if (!soundManager.IsSEPlay() && input.InputMove != Vector2.zero) { soundManager.PlayOneShotSe((int)SEList.Push); }
            else if (!soundManager.IsSEPlay() && input.InputMove == Vector2.zero) { soundManager.StopSE(); }

        }
        // 離すとステート移動
        else
        {
            if (soundManager.IsSEPlay()) { soundManager.StopSE(); }
            anim.SetBool("IsObjectMove", false);
            state.Idle();
        }
    }
}
