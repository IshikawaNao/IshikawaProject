using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Header("�v���C���[�R���g���[���[")]
    PlayerController playerCon;
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

    Camera mainCam;
    CameraMove cameraMove;
    CameraRotate cameraRotate;
    KeyInput input;
    SaveDataManager saveDataManager;

    void Start()
    {
        this.transform.position = Look.transform.position;
        input = KeyInput.Instance;
        saveDataManager = SaveDataManager.Instance;
        mainCam = Camera.main;
        cameraMove = new CameraMove();
        cameraRotate = new CameraRotate();
    }

    void Update()
    {
        cameraMove.SetPlayerAlpha(this.gameObject,mat, mainCam);
        if (input.CameraReset)
        {
            cameraRotate.GimmickCP(this.gameObject, Look);
        }
    }

    private void LateUpdate()
    {
        this.transform.position = Look.transform.position;
        cameraMove.CameraForwardMove(this.gameObject, target, wall_layerMask, mainCam);
        // �I�u�W�F�N�g�������Ă���Œ��ƃX�^�[�g���̓J�����������Ȃ��悤�ɂ���
        if (playerCon.CurrentState.State == PlayerState.Push || !sm.IsTimeLine) 
        {
            cameraRotate.GimmickCP(this.gameObject, Look);
            return;
        }
        // �������ɃJ�������������ɂ���
        else if(sm.Fall)
        {
            cameraRotate.DropCamera();
        }
        // �|�[�Y���̓J�������~�߂�
        else if(playerCon.CurrentState.State == PlayerState.Pause) 
        {
            return; 
        }
        this.transform.localRotation = cameraRotate.RotateCameraBy(input.CameraPos, this.gameObject, saveDataManager.Sensitivity);
    }
}
