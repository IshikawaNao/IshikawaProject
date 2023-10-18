using UnityEngine;

/// <summary>
/// ���ʃI�u�W�F�N�g���o��邩
/// </summary>
public class PlayerClimb 
{
    // ray�̋���,����
    const float wallCheckOffset = 0.85f;
    const float upperWallCheckOffset = 5f;
    const float wallCheckDistance = 1f;
    RaycastHit hit;

    Vector3 rayPos;
    public Vector3 RayPos { get { return rayPos; } }

    bool isForwardWall;
    public bool IsForwardWall { get { return isForwardWall; } }
    bool isUpperWall;
    public bool IsUpperWall { get { return isUpperWall; } }

    GameObject player;

    public PlayerClimb(GameObject _player) => player = _player;

    public void ClimbMove()
    {
        player.transform.position = Vector3.MoveTowards(player.transform.position, rayPos,0);
    }

    // player�̐��ʂɓo��鍂���̕ǂ����邩
    public bool ClimbCheck()
    {
        //  �ǔ���Ɏg�p����ϐ�
        Ray wallCheckRay = new Ray(player.transform.position + Vector3.up * wallCheckOffset, player.transform.forward);
        Ray upperCheckRay = new Ray(player.transform.position + Vector3.up * upperWallCheckOffset, player.transform.forward);
        
        //  �ǔ�����i�[
        isForwardWall = Physics.Raycast(wallCheckRay, wallCheckDistance);
        isUpperWall = Physics.Raycast(upperCheckRay, wallCheckDistance);

        Physics.Raycast(wallCheckRay, out hit);
        rayPos = hit.point;

        Debug.DrawRay(player.transform.position + Vector3.up * wallCheckOffset, player.transform.forward, Color.red,  3);
        Debug.DrawRay(player.transform.position + Vector3.up * upperWallCheckOffset, player.transform.forward, Color.red, 3);
        if (isForwardWall && !isUpperWall)
        {
            return true;
        }
        return false;
    }
}
