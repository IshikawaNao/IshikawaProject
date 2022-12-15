using UnityEngine;

/// <summary>
/// �ړ������̐؂�ւ�
/// </summary>
public class SwitchingMove 
{
    PlayerMove playerMove;  // �v���C���[�̈ړ�

    Vector2 range = new Vector2(0.5f, -0.5f);

    public void SwitchMove(GameObject player,Rigidbody rb, Vector2 input, Animator anim, bool isPush, bool isClimb,bool isGround ,bool isMove)
    {
        // �ړ��̐؂�ւ�
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

        // �ړ�
        if (isMove)
        {
            playerMove.ExcuteMove(input, anim);
        }
        else if(!isMove)
        {
        }
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
}
