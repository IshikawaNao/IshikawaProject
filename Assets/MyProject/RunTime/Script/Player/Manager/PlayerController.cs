using UnityEngine;

/// <summary>
/// プレイヤー制御
/// </summary>
public class PlayerController : MonoBehaviour
{
    bool isClimb = false;       // オブジェクトを登るフラグ

    bool isMove = true;         // プレイヤーの移動のフラグ

    [SerializeField,Header("ポーズ画面")]
    GameObject Pause;       
    [SerializeField,Header("オプション")]
    OperationExplanationMove operation;
    [SerializeField,Header("ステージマネージャー")]
    StageManager sm;
    [SerializeField,Header("チュートリアル")]
    TutorialStageDisplay tutorial;
    [SerializeField,Header("ソナーエフェクト")]
    SonarEffect sonar;       
    [SerializeField,Header("Rigidbody")]
    Rigidbody rb;           
    [SerializeField,Header("コライダー")]
    CapsuleCollider col;
    [SerializeField, Header("animator")]
    Animator anim; 

    PlayerStatecontroller playerState;

    KeyInput input;         // 入力受け取り
    GroundRay gr;         // 接地判定
    PushObject push;    // Pushオブジェクト受け取り
    public PushObject PushMoveObject { get { return push; } }
    PlayerClimb climb;
    PlayerJump jump;

    void Start()
    {
        input = KeyInput.Instance;
        gr = new GroundRay(this.gameObject);
        push = new PushObject(this.gameObject,anim);
        climb = new PlayerClimb(this.gameObject);
        jump = new PlayerJump(rb,anim,this.gameObject);

        isMove = true;
        playerState = new PlayerStatecontroller(input, this.gameObject, rb, col, anim, push, climb, sm);
        playerState.Init(this, PlayerState.Idle);
    }

    void Update()
    {
        playerState.Update();
        LimitationOfAction();
        PauseOpen();
    }

    private void FixedUpdate()
    {
        playerState.FixedUpdate();
    }

    void LimitationOfAction()
    {
            push.PushAnimationChange(gr.IsGround(), isClimb, input.PushAction);
        if (IsMoveAction())
        {
            OperationOpen();
            Fly();
            Sonar();
        }
        jump.Jump(gr.IsGround(),isClimb);
    }
    void OperationOpen()
    {
        operation.ClimbCheck(climb.ClimbCheck());
        operation.PushCheck(push.CanPush());
        operation.JumpCheck(gr.IsFly());
    }
    // 移動できるか
    bool IsMoveAction()
    {
        if (isMove && !sm.Goal && !sm.IsTimeLine && !sonar.IsOnSonar && !tutorial.IsTutoria)
        {
            return true;
        }
        //rb.velocity = Vector3.zero;
        anim.SetBool("IsIdle", true);
        anim.SetBool("IsWalk", false);
        anim.SetBool("IsRan", false);
        return false;
    }

    // 上空へ飛ぶ
    void Fly()
    {
        if(input.InputJump)
        {
           // jump.Fly(jump.FlyFrag());
        }
    }

    // ポーズ実行時動きを止める
    void PauseOpen()
    {
        if (Pause.activeSelf){ isMove = false;}
        else { isMove = true; }
    }

    // ソナー
    void Sonar()
    {
        if(input.SonarAction　&& gr.IsGround())
        {
            sonar.Sonar();
            sonar.SonarWaveGeneration(this.transform.position);
        }
    }
}
