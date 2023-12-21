using DG.Tweening;
using SoundSystem;
using UnityEngine;
using UnityEngine.Playables;

public class TeleportState : IPlayerState
{
    public PlayerState State => PlayerState.Teleport;
    public void Entry() { TeleporterEfect(); }
    public void Update(){}  
    public void FixedUpdate(){ }
    public void Exit() { }


    const float TimelineTime = 4.5f;
    PlayerStatecontroller state;
    Animator anim;
    GameObject efect;
    RayHitDetection rayHit;
    PlayableDirector timeLine;
    Rigidbody rb;

    public TeleportState(PlayerStatecontroller _state, Animator _anim, GameObject _efect, RayHitDetection _rayHit, Rigidbody _rb)
    {
        state = _state;
        anim = _anim;
        efect = _efect;
        timeLine = efect.GetComponent<PlayableDirector>();
        rayHit = _rayHit;
        rb = _rb;
    }

    private void TeleporterEfect() 
    {
        efect.SetActive(true);
        timeLine.Play();
        anim.SetInteger("MovementState", 0);
        anim.SetFloat("Speed", 0);
        rb.velocity = Vector3.zero;
        rayHit.Teleport();
        SoundManager.Instance.PlayOneShotSe((int)SEList.Teleport);
        DOVirtual.DelayedCall(TimelineTime, () => state.Idle());
    }
}
