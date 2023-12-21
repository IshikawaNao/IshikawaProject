using Unity.VisualScripting;
using UnityEngine;

/// <summary> RayHit確認 </summary>
public class RayHitDetection : MonoBehaviour 
{
    // プレイヤー
    [SerializeField, Header("Player")]
    GameObject player;

    PlayerTeleporter teleporter;
    BeamIaunch beamIaunch;

    Vector3 checkVec = new(1, 0, 1);

    // 接地判定用のrayの長さ
    float distance = 0.2f;

    // 移動判定用rayの長さ
    const float RayLength = 1f;

    // 登り判定用rayの距離,高さ
    const float WallCheckOffset = 0.85f;
    const float UpperWallCheckOffset = 5f;
    const float WallCheckDistance = 1f;

    // rayの距離,高さ
    const float fallCheckDistance = 0.7f;
    // ビームオブジェクトのレイヤー
    const int BeamLayer = 10;

    Rigidbody rb = null;
    public Rigidbody PushRb { get { return rb; } }

    bool isForwardWall;
    public bool IsForwardWall { get { return isForwardWall; } }
    bool isUpperWall;
    public bool IsUpperWall { get { return isUpperWall; } }
    bool isBeamRotate;
    public bool IsBeamRotate { get { return isBeamRotate; } }

    /// <summary> 接地しているか </summary>
    public bool IsGround()
    {
        Vector3 rayPosition = player.transform.position;
        Ray ray = new Ray(rayPosition, Vector3.down);
        Debug.DrawRay(rayPosition, Vector3.down * distance, Color.red, 1f);
        return Physics.Raycast(ray, distance);
    }

    /// <summary> テレポート出来るか </summary>
    public bool IsTeleport()
    {
        Vector3 rayPosition = player.transform.position;
        Ray ray = new Ray(rayPosition, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, distance))
        {
            if (hit.collider.gameObject.tag.Contains("Teleport"))
            {
                teleporter = hit.collider.gameObject.GetComponent<PlayerTeleporter>();
                return teleporter.Activated;
            }
        }
        return false;
    }

    /// <summary> テレポート実行 </summary>
    public void Teleport(){ if(teleporter != null) { teleporter.Teleport(player); } }

    /// <summary> playerの正面に動かせるオブジェクトがあるか </summary>
    public bool CanPush()
    {
        Vector3 orgin = player.transform.position;
        Vector3 direction = Vector3.Scale(player.transform.forward, checkVec);
        var ray = new Ray(orgin, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, RayLength))
        {
            //　ビームオブジェクト検知
            if(hit.collider.gameObject.layer == BeamLayer)
            {
                beamIaunch = hit.collider.gameObject.GetComponent<BeamIaunch>();
                isBeamRotate = true;
            }
            // 動かせるオブジェクトの場合そのオブジェクトの情報を取得
            if (hit.collider.gameObject.tag.Contains("Move"))
            {
                rb = hit.rigidbody;
                return true;
            }
        }
        isBeamRotate = false;
        rb = null;
        return false;
    }

    /// <summary> 回転実行 </summary>
    public void BeamRotate() { if (beamIaunch != null) {　beamIaunch.BeamHeadRotate(); } }

    /// <summary> playerの正面に登れる高さの壁があるか </summary>
    public bool ClimbCheck()
    {
        //  壁判定に使用する変数
        Ray wallCheckRay = new Ray(player.transform.position + Vector3.up * WallCheckOffset, player.transform.forward);
        Ray upperCheckRay = new Ray(player.transform.position + Vector3.up * UpperWallCheckOffset, player.transform.forward);

        //  壁判定を格納
        isForwardWall = Physics.Raycast(wallCheckRay, WallCheckDistance);
        isUpperWall = Physics.Raycast(upperCheckRay, WallCheckDistance);

        //Debug.DrawRay(player.transform.position + Vector3.up * wallCheckOffset, player.transform.forward, Color.red, 3);
        //Debug.DrawRay(player.transform.position + Vector3.up * upperWallCheckOffset, player.transform.forward, Color.red, 3);

        // 前方に壁あり上部に壁がない時True
        if (isForwardWall && !isUpperWall) {  return true; }
        return false;
    }

    /// <summary> 落下中か否か </summary>
    public bool IsFall()
    {
        //  落下判定に使用する変数
        Ray fallCheckRay = new Ray(player.transform.position, Vector3.down);
        return Physics.Raycast(fallCheckRay, fallCheckDistance);
    }
}
