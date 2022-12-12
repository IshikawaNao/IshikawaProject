using UnityEngine;
using System.Collections;
using Cinemachine;

/// <summary>
/// カメラの移動
/// </summary>
public class CameraMove : MonoBehaviour
{
    PlayerController pl;
    CinemachineFreeLook _camera;

    GameObject targetPos;
    KeyInput input;
    IMoveObject iObject;
    

    void Start()
    {
        _camera = GetComponent<CinemachineFreeLook>();
        pl = GameObject.Find("Player").GetComponent<PlayerController>();
        input = GameObject.Find("KeyInput").GetComponent<KeyInput>();
        iObject = new PushObject();
    }

    void Update()
    {
       if(pl.Push)
       {
            LookTarget();
       }
    }

    void LookTarget()
    {
        targetPos = serchTag(pl.gameObject,"Move");

        transform.LookAt(targetPos.transform);

       /* Vector3 followObjectPosition = new Vector3(_camera.Follow.position.x, _camera.transform.position.y, _camera.Follow.position.z);
        Vector3 target = new Vector3(targetPos.transform.position.x, _camera.transform.position.y, targetPos.transform.position.z);

        // ベクトルの計算
        Vector3 followTarget = target - followObjectPosition;
        // 外積
        Vector3 fllowReverse = Vector3.Scale(followTarget, new Vector3(-1, 1, -1));
        // カメラ
        Vector3 followCamera = _camera.transform.position - followObjectPosition;

        // カメラ角度
        Vector3 axis = Vector3.Cross(followCamera, fllowReverse);

        float direction = axis.y < 0 ? -1 : 1;
        float angle = Vector3.Angle(followCamera, fllowReverse);

        _camera.m_XAxis.Value = angle * direction;*/
    }

    // 近くにあるオブジェクトを参照
    GameObject serchTag(GameObject nowObj, string tagName)
    {
        // 距離を測る用の変数
        float distance = 0;
        // 一番近いオブジェクトの距離
        float closeDistance = 0;

        GameObject target = null;

        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            distance = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            // 距離が近ければオブジェクトを取得
            if (distance > closeDistance)
            {
                distance = closeDistance;
                target = obs;
            }
        }

        return target;
    }
}
