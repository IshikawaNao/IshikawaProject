using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportState : IPlayerState
{
    public PlayerState State => PlayerState.Teleport;
    public void Entry() { /*...*/ }
    public void Update(){}  
    public void FixedUpdate(){ }
    public void Exit() {  }
}
