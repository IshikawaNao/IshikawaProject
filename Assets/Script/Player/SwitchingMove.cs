using UnityEngine;

/// <summary>
/// 移動処理の切り替え
/// </summary>
public class SwitchingMove 
{
    PlayerMove playerMove;  // プレイヤーの移動

    Vector2 range = new Vector2(0.5f, -0.5f);

    public void SwitchMove(GameObject player,Rigidbody rb, Vector2 input, Animator anim, bool isPush, bool isClimb,bool isGround ,bool isMove)
    {
        // 移動の切り替え
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

        // 移動
        if (isMove)
        {
            playerMove.ExcuteMove(input, anim);
        }
        else if(!isMove)
        {
        }
    }
    // 歩きと走りを切り替える
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
