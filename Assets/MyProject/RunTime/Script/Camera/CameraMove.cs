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
    const float dis = -0.1f;

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

    bool Rayhit()
    {
        Vector3 rayPosition = this.transform.position;
        
        if(Physics.SphereCast(rayPosition, radius, Vector3.right, out RaycastHit hit, maxDistance))
        {   
            if(hit.collider.gameObject.name == "Player")
            {
                return false;
            }
            return true;
        }
        else
        {
            return false;
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
        cmPos += dis;
        colliderHit = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        colliderHit = false;
    }
}
