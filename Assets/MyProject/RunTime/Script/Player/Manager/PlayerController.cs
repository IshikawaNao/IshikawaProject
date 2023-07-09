using UnityEngine;
using DG.Tweening;

/// <summary>
/// プレイヤー制御
/// </summary>
public class PlayerController : MonoBehaviour
{
    bool isClimb = false;       // オブジェクトを登るフラグ

    bool isMove = true;         // プレイヤーの移動のフラグ

    bool isJump = false;        // ジャンプのフラグ

    [SerializeField]
    GameObject Pause;       // ポーズ画面

    [SerializeField]
    OperationExplanationMove operation;　//オプション

    [SerializeField]
    StageManager sm;
    [SerializeField]
    TutorialStageDisplay tutorial;

    [SerializeField]
    SonarEffect sonar;       // ソナーエフェクト

    SwitchingMove switchMove;// 移動の切り替え
    [SerializeField]
    Rigidbody rb;           // rigidbody
    [SerializeField]
    CapsuleCollider col;    // コライダー
    [SerializeField]
    Animator anim;          // animator

    KeyInput input;         // 入力受け取り
    GroundRay gr;         // 接地判定
    PushObject push;    // Pushオブジェクト受け取り
    public PushObject PushMoveObject { get { return push; } }
    IClimb iClimb;
    IFly iFly;

    void Start()
    {
        input = KeyInput.Instance;

        switchMove = new SwitchingMove();
        gr = new GroundRay(this.gameObject);
        push = new PushObject(this.gameObject,anim);
        iClimb = new PlayerClimb();
        iFly = new PlayerFly();

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
            Jump();
            Sonar();
        }
    }
    void OperationOpen()
    {
        operation.ClimbCheck(iClimb.Climb(this.gameObject));
        operation.PushCheck(push.CanPush());
        operation.JumpCheck(iFly.FlyFrag(this.gameObject));
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

    // ジャンプ
    void Jump()
    {
        if (gr.IsGround() && isJump)
        {
            isJump = false;
            anim.SetBool("IsJump", false);
            anim.SetBool("IsIanding",true);
        }
        else if(!gr.IsGround() && !isClimb && !isJump)
        {
            DOVirtual.DelayedCall(1, () => JumpStart());
            
        }
    }

    void JumpStart()
    {
        if (!gr.IsGround() && !isClimb && !isJump)
        {
            isJump = true;
            anim.SetBool("IsIanding", false);
            anim.SetBool("IsJump", true);
        }
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
            iFly.Fly(rb, iFly.FlyFrag(this.gameObject), anim);
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
