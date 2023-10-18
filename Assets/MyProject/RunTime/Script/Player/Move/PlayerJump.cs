using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class PlayerJump
{
    RaycastHit hit;
    // ray�̋���,����
    const float fallCheckDistance = 0.7f;
    // �W�����v�����
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
    /// ���������ۂ�
    /// </summary>
    public bool IsFall()
    {
        //  ��������Ɏg�p����ϐ�
        Ray fallCheckRay = new Ray(player.transform.position , Vector3.down);
        //  �ǔ�����i�[
        isFall = Physics.Raycast(fallCheckRay, fallCheckDistance);
        return isFall;
    }

    // �W�����v
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
