using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    KeyInput input;

    // カメラ位置
    float cmPos = 10;
    const float maxCmPos = 10;
    const float minCmPos = 0;

    public float a;
    public float b;
    public float c;

    // カメラの位置を下げる速度
    const float dis = 0.4f;
    // カメラ背後のRayの長さ
    const float rayDistance = 0.5f;

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
        input = KeyInput.Instance;
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

    // カメラの前後移動
    void Avoid(float scrollvalue)
    {
        var pushCameraPos = Vector3.up;
        print(colliderHit);
        if (IsBackObject() && colliderHit)
        {
            cmPos -= dis;
            pushCameraPos.z = Mathf.Clamp(cmPos, minCmPos, maxCmPos);
            this.transform.localPosition = pushCameraPos;
        }
        else if(!IsBackObject() && !colliderHit)
        {
            cmPos = maxCmPos;
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

    public void ResetCamera()
    {
        cmPos = maxCmPos;
    }

    bool IsBackObject()
    {
        Vector3 orgin = this.transform.position;
        Vector3 direction = Vector3.Scale(this.transform.forward, new Vector3(-1,0,-1) * rayDistance);
        Ray ray = new Ray(orgin, direction);

        Debug.DrawRay(orgin, direction, Color.red);
        return !Physics.Raycast(orgin, direction);
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name != "Player")
        {
             colliderHit = true;
        }
        else
        {
            colliderHit = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        colliderHit = false;
    }
}
