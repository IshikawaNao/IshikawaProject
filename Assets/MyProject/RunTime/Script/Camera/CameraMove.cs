using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    KeyInput input;

    // �J�����ʒu
    float cmPos = 10;
    const float maxCmPos = 10;
    const float minCmPos = 2;

    // �J�����̈ʒu�������鑬�x
    const float dis = 1f;
    // �J�����w���Ray�̒���
    const float rayDistance = 0.5f;

    const float waiteTime = 1;
    float saveTime;

    // Ray���a����
    const float radius = 5;
    const float maxDistance = 6;

    float setAlpha = 0;
    const float maxAlpha = 1;
    const float minAlpha = 0.25f;

    // �X�N���[���̏���
    const float scrollDivisor = -600;

    bool colliderHit = false;

    [SerializeField]
    Material mat;

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
        // �}�E�X�̃X�N���[�����擾 �p�b�h����
        var scrollvalue = input.Scroll / scrollDivisor;
        Avoid(scrollvalue);
        MoveInput();
        SetPlayerAlpha();
    }

    // �J�����̑O��ړ�
    void Avoid(float scrollvalue)
    {
        var pushCameraPos = Vector3.up;
        if (IsBackObject() && colliderHit)
        {
            cmPos -= dis;
        }
        else if(!IsBackObject() && !colliderHit)
        {
            cmPos = maxCmPos;  
        }
        pushCameraPos.z = Mathf.Clamp(cmPos, minCmPos, maxCmPos);
        this.transform.localPosition = pushCameraPos;
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

    void SetPlayerAlpha()
    {
        if(cmPos < 4) { setAlpha = minAlpha; }
        else { setAlpha = maxAlpha; }
        mat.SetFloat("_Alpha", setAlpha);

    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
             colliderHit = false;
        }
        else
        {
            colliderHit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        colliderHit = false;
    }
}
