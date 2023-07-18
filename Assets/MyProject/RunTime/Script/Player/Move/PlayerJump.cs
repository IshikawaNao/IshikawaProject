using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump
{
    RaycastHit hit;
    // ジャンプする力
    private const float jumpPower = 20;
    private bool isJump;
    public bool IsJump { get { return isJump; } }
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
            rb.AddForce(0, jumpPower, 0,ForceMode.Impulse);
        }
    }
    /// <summary>
    /// ジャンプ出来るか
    /// </summary>
    public bool FlyFrag()
    {
        Vector3 rayPosition = player.transform.position;
        Ray ray = new Ray(rayPosition, Vector3.down);
        if(Physics.Raycast(ray, out hit, 0.7f))
        {
            if(hit.collider.gameObject.tag.Contains("Fly"))
            {
                return true;
            }
        }
        Debug.DrawRay(rayPosition, Vector3.down * 0.7f, Color.red,1f);
        return false;
    }

    // ジャンプ
    public void Jump(bool isground,bool isclimb)
    {
        if(isground)
        {
            isJump = false;
            anim.SetBool("IsJump", false);
            anim.SetBool("IsIanding", true);
        }
        else if (!isground && !isclimb && !isJump)
        {
            DOVirtual.DelayedCall(1, () => JumpStart(isground,isclimb));
        }
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
