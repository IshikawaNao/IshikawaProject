using UnityEngine;

/// <summary>
/// カメラの移動
/// </summary>
public class CameraMove 
{
    const float playerVisibilityCheck = 2;
    const float playerAlpha = 0.5f;

    /// <summary>　カメラへ向かってRayを飛ばす </summary>
    public void CameraForwardMove(GameObject cameraParent, GameObject target, LayerMask wall_layerMask, Camera main)
    {
        // 親オブジェクトの位置を取得
        Vector3 orgin = cameraParent.transform.position;
        // カメラの移動位置
        var cameraPos = target.transform.position;

        RaycastHit hit;
        // Raycastが壁にあったった時カメラの座標をRaycastが当たった座標にする
        if (Physics.Raycast(orgin, cameraPos - orgin, out hit, Vector3.Distance(orgin, cameraPos), wall_layerMask, QueryTriggerInteraction.Ignore))
        {
            cameraPos = hit.point;
        }
        // Local座標に変換する
        cameraPos = cameraParent.transform.InverseTransformPoint(cameraPos);
        main.transform.localPosition = cameraPos;
    }

    /// <summary> カメラがプレイヤーに近づいた時プレイヤーを半透明にする </summary>
    public void SetPlayerAlpha(GameObject cameraParent,Material mat,Camera main)
    {
        var dis = Vector3.Distance(cameraParent.transform.position, main.transform.position);
        if(dis < playerVisibilityCheck) 
        {
            mat.SetFloat("_Alpha", playerAlpha); 
        }
        else
        { 
            mat.SetFloat("_Alpha", 1); 
        }
    }
}
