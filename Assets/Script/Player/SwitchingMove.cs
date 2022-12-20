using UnityEngine;

/// <summary>
/// ˆÚ“®ˆ—‚ÌØ‚è‘Ö‚¦
/// </summary>
public class SwitchingMove 
{
    PlayerMove playerMove;  // ƒvƒŒƒCƒ„[‚ÌˆÚ“®

    Vector2 range = new Vector2(0.5f, -0.5f);

    public void SwitchMove(GameObject player,Rigidbody rb, Vector2 input, Animator anim, bool isPush, bool isClimb,bool isGround ,bool isMove)
    {
        // ˆÚ“®‚ÌØ‚è‘Ö‚¦
        if (!isPush && !IsRan(input,range) && isGround)
        {
            playerMove = new PlayerMove(new PlayerNormalMove(rb, player));
        }
        else if(!isPush && IsRan(input, range) && isGround)
        {
            playerMove.ChangeMove(new PlayerRan(rb, player));
        }
        else if (isPush)
        {
            playerMove.ChangeMove(new PlayerPushMove(rb, player));
        }
        else if(!isGround && !isClimb)
        {
            playerMove.ChangeMove(new PlayerAirMobile(rb, player));
        }

        // ˆÚ“®
        if (isMove)
        {
            playerMove.ExcuteMove(input, anim);
        }
        else if(!isMove)
        {
        }
    }
    // •à‚«‚Æ‘–‚è‚ğØ‚è‘Ö‚¦‚é
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
