using UnityEngine;
using DG.Tweening;

/// <summary>
/// プレイヤー制御
/// </summary>
public class PlayerController : MonoBehaviour
{
    bool isPush = false;        // オブジェクトが動くフラグ
    public bool Push { get { return isPush; } }

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
    GroundRay gl;         // 接地判定
    [SerializeField]
    Animator anim;          // animator

    KeyInput input;         // 入力受け取り
    IMoveObject iObject;
    IClimb iClimb;
    IFly iFly;

    void Start()
    {
        input = KeyInput.Instance;

        switchMove = new SwitchingMove();
        iObject = new PushObject();
        iClimb = new PlayerClimb();
        iFly = new PlayerFly();

        isMove = true;
    }

    void Update()
    {
        limitationOfAction();
        PauseOpen();
    }

    private void FixedUpdate()
    {
        if(isMoveAction())
        {
            // プレイヤーの移動切り替え
            var _playerMover = switchMove.SwitchMove(this.gameObject, rb, input.InputMove, isPush, isClimb, gl.IsGround());
            // 移動
            _playerMover.Move(input.InputMove, anim);
            if (isPush)
            {
                iObject.Move(iObject.Box_rb(), input.InputMove, this.gameObject, anim);
            }
        }
    }

    void limitationOfAction()
    {
        if (isMoveAction())
        {
            ObjectMove();
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
        operation.PushCheck(iObject.Push(this.gameObject));
        operation.JumpCheck(iFly.FlyFrag(this.gameObject));
    }
    // 移動できるか
    bool isMoveAction()
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
        if (gl.IsGround() && isJump)
        {
            isJump = false;
            anim.SetBool("IsJump", false);
            anim.SetBool("IsIanding",true);
        }
        else if(!gl.IsGround() && !isClimb && !isJump)
        {
            isJump = true;
            anim.SetBool("IsIanding", false);
            anim.SetBool("IsJump", true);
        }
    }

    // オブジェクトを動かす
    void ObjectMove()
    {
        if (iObject.Push(this.gameObject) && gl.IsGround() && !isClimb)
        {
            if (input.PushAction)
            {
                isPush = true;
            }
            else
            {
                anim.SetBool("IsObjectMove", false);
                isPush = false;
            }
        }
        else
        {
            anim.SetBool("IsObjectMove", false);
            isPush = false;
        }
    }

    // オブジェクトを登る
    void ObjectClimb()
    {
        if (iClimb.Climb(this.gameObject) && !isPush)
        {
            if (input.ClimbAction && gl.IsGround())
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

    // 修正
    void PauseOpen()
    {
        if (Pause.activeSelf){ isMove = false;}
        else { isMove = true; }
    }

    // ソナー
    void Sonar()
    {
        if(input.SonarAction　&& gl.IsGround())
        {
            sonar.Sonar();
            sonar.SonarWaveGeneration(this.transform.position);
        }
    }
}
