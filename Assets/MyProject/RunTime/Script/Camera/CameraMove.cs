using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    KeyInput input;

    // カメラ位置
    float cmPos = 10;
    const float maxCmPos = 10;
    const float minCmPos = 3;

    // カメラの位置を下げる数
    const float dis = 0.1f;

    const float waiteTime = 1;
    float saveTime;

    // Ray半径距離
    const float radius = 5;
    const float maxDistance = 6;

    // スクロールの除数
    const float scrollDivisor = -600;

    bool colliderHit = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        var cam = Vector3.up;
        cam.z = maxCmPos;
        this.transform.localPosition = cam;
    }

    private void Update()
    {
        // マウスのスクロールを取得 パッド調整
        var scrollvalue = input.Scroll / scrollDivisor;

        Avoid(scrollvalue);
        ResetCamera();
        MoveInput();
    }

    void Avoid(float scrollvalue)
    {
        var pushCameraPos = Vector3.up;
        if (colliderHit)
        {
            
            pushCameraPos.z = Mathf.Clamp(cmPos, minCmPos, maxCmPos);
            this.transform.localPosition = pushCameraPos;
        }
        else if(!colliderHit)
        {
            cmPos += scrollvalue;
            pushCameraPos.z = Mathf.Clamp(cmPos, minCmPos, maxCmPos);
            this.transform.localPosition = pushCameraPos;
        }

    }

    void MoveInput()
    {
        if(input.LongPressedMove)
        {
            if(waiteTime + saveTime <= Time.time)
            {
                cmPos += dis;
                cmPos = Mathf.Clamp(cmPos, minCmPos, maxCmPos);
            }
        }
        else
        {
            saveTime = Time.time;
        }
    }

    void ResetCamera()
    {
        if(input.CameraReset)
        {
            cmPos = maxCmPos;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        cmPos -= dis;
        colliderHit = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        colliderHit = false;
    }
}
