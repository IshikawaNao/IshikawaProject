using UnityEngine;

/// <summary>
/// �ړ������̐؂�ւ�
/// </summary>
public class SwitchingMove 
{
    // �v���C���[�̈ړ�
    private IPlayerMover _playerMover;

    Vector2 range = new Vector2(0.5f, -0.5f);

    public IPlayerMover SwitchMove(GameObject player,Rigidbody rb, Vector2 input, bool isPush, bool isClimb,bool isGround)
    {
        if(_playerMover == null)
        {
            ChangeMove(new PlayerNormalMove(rb, player));
        }
        // �ړ��̐؂�ւ�
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
    // �����Ƒ����؂�ւ���
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
