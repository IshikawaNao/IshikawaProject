using DG.Tweening;
using UnityEngine;
using UnityEngine.U2D.IK;

public class ClimbState : IPlayerState
{
    PlayerStatecontroller state;
    GameObject PlayerObj;
    Animator anim;
    CapsuleCollider col;
    PlayerClimb climb;

    const float climbDis = 5;
    const float climbTime = 0.8f;

    bool isFrontMove = false;
    Tweener tweener;

    public PlayerState State => PlayerState.Climb;
    public void Entry() { ClimbStart(); }
    public void Update(){ ClimbMove(); }
    public void FixedUpdate(){  }
    public void Exit() { ClimbEnd(); }

    public ClimbState(PlayerStatecontroller _state , Animator _anim, GameObject _playerObj, CapsuleCollider _col, PlayerClimb _climb)
    {
        state = _state;
        anim = _anim;
        PlayerObj = _playerObj;
        col = _col;
        climb = _climb;
    }

    void ClimbStart()
    {
        tweener = PlayerObj.transform.DOMove(new Vector3(PlayerObj.transform.position.x, PlayerObj.transform.position.y + climbDis, PlayerObj.transform.position.z) , climbTime, false);
        tweener.Play();
        //anim.applyRootMotion = true;
        anim.SetBool("IsClimb", true);
        DOVirtual.DelayedCall(1.3f, () => state.Idle(), false);
    }

    void ClimbMove()
    {
        climb.ClimbCheck();
        if(!climb.IsForwardWall && !isFrontMove)
        {
            tweener.Pause();
            isFrontMove = true;
            PlayerObj.transform.DOMove(PlayerObj.transform.position + 1f * Vector3.up + col.radius * 4f * PlayerObj.transform.forward, .5f,false);
            return;
        }
    }

    void ClimbEnd()
    {
        anim.SetBool("IsClimb",false);
        isFrontMove = false;
        anim.applyRootMotion = false;
    }
}
