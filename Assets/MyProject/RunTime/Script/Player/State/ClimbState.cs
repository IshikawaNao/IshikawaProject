using DG.Tweening;
using UnityEngine;

public class ClimbState : IPlayerState
{
    PlayerStatecontroller state;
    GameObject PlayerObj;
    Animator anim;
    CapsuleCollider col;
    RayHitDetection rayHitDetection;

    const float ClimbDis = 5;
    const float RlimbTime = 0.8f;
    const float DlayTime = 1.3f;

    bool isFrontMove = false;
    Tweener tweener;

    public PlayerState State => PlayerState.Climb;
    public void Entry() { ClimbStart(); }
    public void Update(){ ClimbMove(); }
    public void FixedUpdate(){  }
    public void Exit() { ClimbEnd(); }

    public ClimbState(PlayerStatecontroller _state , Animator _anim, GameObject _playerObj, CapsuleCollider _col, RayHitDetection _rayHitDetection)
    {
        state = _state;
        anim = _anim;
        PlayerObj = _playerObj;
        col = _col;
        rayHitDetection = _rayHitDetection;
    }

    void ClimbStart()
    {
        tweener = PlayerObj.transform.DOMove(new Vector3(PlayerObj.transform.position.x, PlayerObj.transform.position.y + ClimbDis, PlayerObj.transform.position.z) , RlimbTime, false);
        tweener.Play();
        //anim.applyRootMotion = true;
        anim.SetInteger("MovementState", 2);
        DOVirtual.DelayedCall(DlayTime, () => state.Idle(), false);
    }

    void ClimbMove()
    {
        rayHitDetection.ClimbCheck();
        if(!rayHitDetection.IsForwardWall && !isFrontMove)
        {
            tweener.Pause();
            isFrontMove = true;
            PlayerObj.transform.DOMove(PlayerObj.transform.position + 1f * Vector3.up + col.radius * 4f * PlayerObj.transform.forward, .5f,false);
            return;
        }
    }

    void ClimbEnd()
    {
        isFrontMove = false;
        anim.applyRootMotion = false;
    }
}
