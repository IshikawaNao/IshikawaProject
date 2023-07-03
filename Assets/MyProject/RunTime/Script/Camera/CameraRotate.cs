using UnityEngine;

/// <summary>
/// �J�����̈ړ�
/// </summary>
public class CameraRotate : MonoBehaviour
{
    [SerializeField,Header("���C���J����")]
    GameObject mainCM;

    [SerializeField, Header("�v���C���[")]
    GameObject player;
    [SerializeField]
    PlayerController pc;

    [SerializeField, Header("�X�e�[�W�}�l�[�W���[")]
    StageManager sm;
    [SerializeField]
    CameraMove cm;

    KeyInput input;

    // ���x
    float sensitivity = 0.1f;
    
    // �J�����p�x
    float verticalValue = -30;
    const float verticalMaxValue = 10f;
    const float verticalMinValue = -40f;


    void Start()
    {
        mainCM.transform.localPosition = new Vector3(0, 1, 10);
        input = KeyInput.Instance;
    }

    private void LateUpdate()
    {
        this.transform.position = player.transform.position;
        SwitchingCP();
        ResetCamera();
    }

    void SwitchingCP()
    {
        if(pc.Push || sm.IsStart) { GimmickCP(); }
        else {  Pcm(); }
    }

    void Pcm()
    {
        float horizontalInput = input.CameraPos.x;
        float verticalInput = input.CameraPos.y;

        Vector3 rot = transform.localRotation.eulerAngles;
        float horizontalValue = rot.y + horizontalInput * sensitivity;

        verticalValue  -= verticalInput * sensitivity;
        verticalValue = Mathf.Clamp(verticalValue, verticalMinValue, verticalMaxValue);

        transform.localRotation = Quaternion.Euler(verticalValue, horizontalValue, 0);
    }

    void ResetCamera()
    {
        if (input.CameraReset)
        {
            GimmickCP();
        }
    }


    void GimmickCP()
    {
        Vector3 rot = new Vector3 (-player.transform.forward.x, -player.transform.forward.y, -player.transform.forward.z);
        verticalValue = rot.x;
        this.transform.localRotation = Quaternion.LookRotation( rot);
        cm.ResetCamera();
    }
}
