using UnityEngine;

/// <summary>
/// 正面オブジェクトが登れるか
/// </summary>
public class PlayerClimb 
{
    // rayの距離,高さ
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

    // playerの正面に登れる高さの壁があるか
    public bool ClimbCheck()
    {
        //  壁判定に使用する変数
        Ray wallCheckRay = new Ray(player.transform.position + Vector3.up * wallCheckOffset, player.transform.forward);
        Ray upperCheckRay = new Ray(player.transform.position + Vector3.up * upperWallCheckOffset, player.transform.forward);
        
        //  壁判定を格納
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
