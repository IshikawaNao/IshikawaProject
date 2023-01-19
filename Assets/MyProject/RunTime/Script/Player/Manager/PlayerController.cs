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

    public bool IsMove { get; set; } = true;         // プレイヤーの移動のフラグ

    bool isJump = false;

    [SerializeField]
    GameObject Pause;       // ポーズ画面

    [SerializeField]
    OperationExplanationMove operation;

    SwitchingMove switchMove;// 移動の切り替え
    Rigidbody rb;           // rigidbody
    CapsuleCollider col;    // コライダー
    GroundRay gl;         // 接地判定
    KeyInput input;         // 入力受け取り

    Animator anim;          // animator

    IMoveObject iObject;
    IClimb iClimb;
    IFly iFly;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        gl = GetComponent<GroundRay>();
        input = KeyInput.Instance;
        anim = GetComponent<Animator>();

        switchMove = new SwitchingMove();
        iObject = new PushObject();
        iClimb = new PlayerClimb();
        iFly = new PlayerFly();

        IsMove = true;
    }

    void Update()
    {
        ObjectMove();
        ObjectClimb();
        OperationOpen();
        Fly();
        PauseOpen();
        Jump();
    }

    private void FixedUpdate()
    {
        switchMove.SwitchMove(this.gameObject,rb, input.InputMove, anim, isPush, isClimb , gl.IsGround(), IsMove);
        if (isPush)
        {
            iObject.Move(iObject.Box_rb(), input.InputMove, this.gameObject, anim);
        }
    }

    void OperationOpen()
    {
        operation.ClimbCheck(iClimb.Climb(this.gameObject));
        operation.PushCheck(iObject.Push(this.gameObject));
        operation.JumpCheck(iFly.FlyFrag(this.gameObject));
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
        if (iObject.Push(this.gameObject) && gl.IsGround())
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
        if (iClimb.Climb(this.gameObject))
        {
            if (input.ClimbAction && gl.IsGround())
            {
                isClimb = true;
                IsMove = false;
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
            IsMove = true;
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

    void PauseOpen()
    {
        if (Pause.activeSelf){ IsMove = false;}
        else { IsMove = true; }
    }
}
