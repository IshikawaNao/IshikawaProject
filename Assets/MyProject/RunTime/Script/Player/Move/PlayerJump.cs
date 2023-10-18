using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class PlayerJump
{
    RaycastHit hit;
    // rayの距離,高さ
    const float fallCheckDistance = 0.7f;
    // ジャンプする力
    private const float jumpPower = 20;
    private bool isJump;
    public bool IsJump { get { return isJump; } }
    bool isFall;
    public bool IsForwardWall { get { return isFall; } }
    Rigidbody rb;
    Animator anim;
    GameObject player;

    public PlayerJump(Rigidbody _rb, Animator _anim, GameObject _player)
    {
        rb = _rb;
        anim = _anim;
        player = _player;
    }

    public void Fly(bool isFly)
    {
        if(isFly)
        {
            
        }
    }
    
    /// <summary>
    /// 落下中か否か
    /// </summary>
    public bool IsFall()
    {
        //  落下判定に使用する変数
        Ray fallCheckRay = new Ray(player.transform.position , Vector3.down);
        //  壁判定を格納
        isFall = Physics.Raycast(fallCheckRay, fallCheckDistance);
        return isFall;
    }

    // ジャンプ
    public void Jump(bool isground,bool isclimb)
    {
        /*if(isground && !isJump)
        {
            isJump = false;
            anim.SetBool("IsJump", false);
            anim.SetBool("IsIanding", true);
        }
        else if (!isground && !isclimb && !isJump)
        {
            DOVirtual.DelayedCall(1, () => JumpStart(isground,isclimb));
        }*/
    }

    void JumpStart(bool isground, bool isclimb)
    {
        if (!isground && !isclimb && !isJump)
        {
            isJump = true;
            anim.SetBool("IsIanding", false);
            anim.SetBool("IsJump", true);
        }
    }
}
