using UnityEngine;
using DG.Tweening;

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

    SwitchingMove switchMove;// 移動の切り替え
    KeyInput input;         // 入力受け取り
    GroundRay gr;         // 接地判定
    PushObject push;    // Pushオブジェクト受け取り
    public PushObject PushMoveObject { get { return push; } }
    IClimb iClimb;
    PlayerJump jump;

    void Start()
    {
        input = KeyInput.Instance;

        switchMove = new SwitchingMove();
        gr = new GroundRay(this.gameObject);
        push = new PushObject(this.gameObject,anim);
        iClimb = new PlayerClimb();
        jump = new PlayerJump(rb,anim,this.gameObject);

        isMove = true;
    }

    void Update()
    {
        LimitationOfAction();
        PauseOpen();
    }

    private void FixedUpdate()
    {
        if(IsMoveAction())
        {
            // プレイヤーの移動切り替え
            var _playerMover = switchMove.SwitchMove(this.gameObject, rb, input.InputMove, push.IsPush, isClimb, gr.IsGround());
            // 移動
            _playerMover.Move(input.InputMove, anim);
            // プッシュオブジェクト移動
            push.Move(input.InputMove);
        }
    }

    void LimitationOfAction()
    {
        if (IsMoveAction())
        {
            push.PushAnimationChange(gr.IsGround(), isClimb, input.PushAction);
            ObjectClimb();
            OperationOpen();
            Fly();
            Sonar();
        }
        jump.Jump(gr.IsGround(),isClimb);
    }
    void OperationOpen()
    {
        operation.ClimbCheck(iClimb.Climb(this.gameObject));
        operation.PushCheck(push.CanPush());
        operation.JumpCheck(jump.FlyFrag());
    }
    // 移動できるか
    bool IsMoveAction()
    {
        if (isMove && !sm.Goal && !sm.IsStart && !sonar.IsOnSonar && !tutorial.IsTutoria)
        {
            return true;
        }
        rb.velocity = Vector3.zero;
        anim.SetBool("IsIdle", true);
        anim.SetBool("IsWalk", false);
        anim.SetBool("IsRan", false);
        return false;
    }

    

    // オブジェクトを登る
    void ObjectClimb()
    {
        if (iClimb.Climb(this.gameObject) && !push.IsPush)
        {
            if (input.ClimbAction && gr.IsGround())
            {
                isClimb = true;
                isMove = false;
            }
            if (isClimb)
            {
                iClimb.ClimbPlayer(rb,anim);
            }
        }
        else
        {
            if (isClimb)
            {
                this.transform.DOMove(this.transform.position + 0.1f * Vector3.up + col.radius * 2f * this.transform.forward, 0.05f);
            }
            isClimb = false;
            anim.SetBool("IsClimb", false);
            isMove = true;
        }
    }

    // 上空へ飛ぶ
    void Fly()
    {
        if(input.InputJump)
        {
            jump.Fly(jump.FlyFrag());
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
