using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlownState : IPlayerState
{
    // ƒWƒƒƒ“ƒv‚·‚é—Í
    private const float jumpPower = 20;
    PlayerStatecontroller state;
    Rigidbody rb;

    public PlayerState State => PlayerState.Climb;
    public void Entry() { }
    public void Update() { }
    public void FixedUpdate() { }
    public void Exit() { }

    public BlownState(PlayerStatecontroller _state , Rigidbody _rb) 
    {
        state = _state;
        rb = _rb;
    }

    void BlownMove()
    {
        rb.AddForce(0, jumpPower, 0, ForceMode.Impulse);
    }
}