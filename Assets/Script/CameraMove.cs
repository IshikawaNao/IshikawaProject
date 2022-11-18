using UnityEngine;

/// <summary>
/// カメラの移動
/// </summary>
public class CameraMove : MonoBehaviour
{
    [SerializeField]
    GameObject playerObj;
    Vector3 targetPos;

    KeyInput input;
    void Start()
    {
        input = GameObject.Find("KeyInput").GetComponent<KeyInput>();
        targetPos = playerObj.transform.position;
    }

    void Update()
    {
        // playerの移動量分、カメラを移動
        transform.position += playerObj.transform.position - targetPos;
        targetPos = playerObj.transform.position;

        //マウスの移動量
        float mouseInputX = input.CameraPos.x;

        // targetの位置のY軸を中心に、回転（公転）する
        transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * 100f);
    }
}
