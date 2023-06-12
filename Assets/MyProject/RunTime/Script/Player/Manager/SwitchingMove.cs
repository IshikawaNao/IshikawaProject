using UnityEngine;

/// <summary>
/// ˆÚ“®ˆ—‚ÌØ‚è‘Ö‚¦
/// </summary>
public class SwitchingMove 
{
    // ƒvƒŒƒCƒ„[‚ÌˆÚ“®
    private IPlayerMover _playerMover;

    Vector2 range = new Vector2(0.5f, -0.5f);

    public IPlayerMover SwitchMove(GameObject player,Rigidbody rb, Vector2 input, bool isPush, bool isClimb,bool isGround)
    {
        if(_playerMover == null)
        {
            ChangeMove(new PlayerNormalMove(rb, player));
        }
        // ˆÚ“®‚ÌØ‚è‘Ö‚¦
        if (!isPush && !IsRan(input,range) && isGround)
        {
            ChangeMove(new PlayerNormalMove(rb, player));
        }
        else if(!isPush && IsRan(input, range) && isGround)
        {
           ChangeMove(new PlayerRan(rb, player));
        }
        else if (isPush)
        {
            ChangeMove(new PlayerPushMove(rb, player));
        }
        else if(!isGround && !isClimb)
        {
            ChangeMove(new PlayerAirMobile(rb, player));
        }

        return _playerMover;
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

    void ChangeMove(IPlayerMover playerMover)
    {
        _playerMover = playerMover;
    }
}
