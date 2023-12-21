using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Header("プレイヤーコントローラー")]
    PlayerController playerCon;
    [SerializeField, Header("ステージマネージャー")]
    StageManager sm;
    [SerializeField, Header("ロックオンオブジェクト")]
    GameObject Look;
    [SerializeField, Header("カメラターゲットオブジェクト")]
    GameObject target;
    [SerializeField, Header("プレイヤーマテリアル")]
    Material mat;
    [SerializeField, Header("レイヤーマスク")]
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
        // オブジェクトを押している最中とスタート時はカメラが動かないようにする
        if (playerCon.CurrentState.State == PlayerState.Push || !sm.IsTimeLine) 
        {
            cameraRotate.GimmickCP(this.gameObject, Look);
            return;
        }
        // 落下時にカメラを下向きにする
        else if(sm.Fall)
        {
            cameraRotate.DropCamera();
        }
        // ポーズ時はカメラを止める
        else if(playerCon.CurrentState.State == PlayerState.Pause) 
        {
            return; 
        }
        this.transform.localRotation = cameraRotate.RotateCameraBy(input.CameraPos, this.gameObject, saveDataManager.Sensitivity);
    }
}
