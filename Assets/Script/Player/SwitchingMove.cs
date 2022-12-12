using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingMove 
{
    PlayerMove playerMove;  // ƒvƒŒƒCƒ„[‚ÌˆÚ“®

    Vector2 range = new Vector2(0.5f, -0.5f);

    public void SwitchMove(GameObject player,Rigidbody rb, Vector2 input, Animator anim, bool isPush, bool isMove)
    {
        // ˆÚ“®‚ÌØ‚è‘Ö‚¦
        if (!isPush && !IsRan(input,range))
        {
            playerMove = new PlayerMove(new PlayerNormalMove(rb, player));
        }
        else if(!isPush && IsRan(input, range))
        {
            playerMove.ChangeMove(new PlayerRan(rb, player));
        }
        else if (isPush)
        {
            playerMove.ChangeMove(new PlayerPushMove(rb, player));
        }

        // ˆÚ“®
        if (isMove)
        {
            playerMove.ExcuteMove(input, anim);
            //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y - 5, rb.velocity.z);
        }
        else if(!isMove)
        {
            Debug.Log("a");
            //rb.velocity = new Vector3(0, 0, 0);
        }
    }
    bool IsRan(Vector2 input, Vector2 range)
    {
        if (input == Vector2.zero)
        {
            return false;
        }
        if (input.x > range.x && input.x < range.y)
        {
            return false;
        }
        return true;
    }
}
