using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Header("�J����")]
    Camera mainCam;
    [SerializeField, Header("�v���C���[�R���g���[���[")]
    PlayerController pc;
    [SerializeField, Header("�X�e�[�W�}�l�[�W���[")]
    StageManager sm;
    [SerializeField, Header("���b�N�I���I�u�W�F�N�g")]
    GameObject Look;
    [SerializeField, Header("�J�����^�[�Q�b�g�I�u�W�F�N�g")]
    GameObject target;
    [SerializeField, Header("�v���C���[�}�e���A��")]
    Material mat;
    [SerializeField, Header("���C���[�}�X�N")]
    LayerMask wall_layerMask;

    CameraMove cm = new CameraMove();
    CameraRotate cr = new CameraRotate();
    KeyInput input;
    SaveDataManager saveDataManager;

    void Start()
    {
        input = KeyInput.Instance;
        saveDataManager = SaveDataManager.Instance;
    }

    void Update()
    {
        this.transform.position = Look.transform.position;
        cm.SetPlayerAlpha(this.gameObject,mat, mainCam);
        if (input.CameraReset)
        {
            cr.GimmickCP(this.gameObject, Look);
        }
    }

    private void LateUpdate()
    {
        cm.CameraForwardMove(this.gameObject, target, wall_layerMask, mainCam);
        // �I�u�W�F�N�g�������Ă���Œ��ƃX�^�[�g���̓J�����������Ȃ��悤�ɂ���
        if (pc.PushMoveObject.IsPush || sm.IsStart) 
        {
            cr.GimmickCP(this.gameObject, Look);
            return;
        }
        else if(sm.Fall)
        {
            cr.DropCamera();
        }
        this.transform.localRotation = cr.RotateCameraBy(input.CameraPos, this.gameObject, saveDataManager.Sensitivity);
    }
}
