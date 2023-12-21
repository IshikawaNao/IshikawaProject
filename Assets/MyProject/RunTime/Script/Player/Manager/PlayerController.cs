using SoundSystem;
using UnityEngine;

/// <summary> プレイヤー制御 </summary>
public class PlayerController : MonoBehaviour
{
    // 現在のステート
    public IPlayerState CurrentState { get { return state.CurrentState; } }

    [SerializeField,Header("ステージマネージャー")]
    StageManager sm;
    [SerializeField,Header("ソナーエフェクト")]
    SonarEffect sonar;       
    [SerializeField,Header("Rigidbody")]
    Rigidbody rb;
    [SerializeField,Header("コライダー")]
    CapsuleCollider col;
    [SerializeField, Header("animator")]
    Animator anim;
    [SerializeField, Header("テレポートアニメーション")]
    GameObject teleport;
    [SerializeField,Header("UIManager")] 
    MainUIManager mainUIManager;
    [SerializeField, Header("RayHit検知")]
    RayHitDetection rayHitDetection;
    // ステート変更
    PlayerStatecontroller state;
    // 入力受け取り
    KeyInput input;         
    SoundManager soundManager;

    void Start()
    {
        input = KeyInput.Instance;
        soundManager = SoundManager.Instance;
        state = new PlayerStatecontroller(input, soundManager, this.gameObject, rb, col, 
            anim, rayHitDetection, sm, teleport, sonar, mainUIManager);
        state.Init(PlayerState.Idle);
    }

    void Update()
    {
        state.Update();
    }

    private void FixedUpdate()
    {
        state.FixedUpdate();
    }
}
