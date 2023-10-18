using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Header("カメラ")]
    Camera mainCam;
    [SerializeField, Header("プレイヤーコントローラー")]
    PlayerController pc;
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

    CameraMove cm = new CameraMove();
    CameraRotate cr = new CameraRotate();
    KeyInput input;
    SaveDataManager saveDataManager;

    void Start()
    {
        this.transform.position = Look.transform.position;
        input = KeyInput.Instance;
        saveDataManager = SaveDataManager.Instance;
    }

    void Update()
    {
        cm.SetPlayerAlpha(this.gameObject,mat, mainCam);
        if (input.CameraReset)
        {
            cr.GimmickCP(this.gameObject, Look);
        }
    }

    private void LateUpdate()
    {
        this.transform.position = Look.transform.position;
        cm.CameraForwardMove(this.gameObject, target, wall_layerMask, mainCam);
        // オブジェクトを押している最中とスタート時はカメラが動かないようにする
        if (pc.PushMoveObject.IsPush || !sm.IsTimeLine) 
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
