using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// プレイヤー制御
/// </summary>
public class PlayerController : MonoBehaviour
{
    bool isPush = false;        // オブジェクトが動くフラグ

    bool isClimb = false;       // オブジェクトを登るフラグ

    bool isMove = true;         // プレイヤーの移動のフラグ

    [SerializeField]
    GameObject cameraPos;   // カメラの位置

    PlayerMove playerMove;  // プレイヤーの移動
    Rigidbody rb;           // rigidbody
    CapsuleCollider col;    // コライダー
    GroundLayer gl;         // 接地判定
    KeyInput input;         // 入力受け取り

    Animator anim;          // animator

    IMoveObject iObject;
    IClimb iClimb;
    IFly iFly;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        gl = GetComponent<GroundLayer>();
        input = GameObject.Find("KeyInput").GetComponent<KeyInput>();
        anim = GetComponent<Animator>();

        playerMove = new PlayerMove(new PlayerNormalMove(rb, this.gameObject));
        iObject = new PushObject();
        iClimb = new PlayerClimb();
        iFly = new PlayerFly();
    }

    void Update()
    {
        Jump();
        ObjectMove();
        ObjectClimb();
        Fly();
    }

    private void FixedUpdate()
    {
        // 移動
        if(!isPush)
        {
            playerMove = new PlayerMove(new PlayerNormalMove(rb, this.gameObject));
        }
        else if(isPush)
        {
            playerMove.ChangeMove(new PlayerPushMove(rb, this.gameObject));
        }

        if(isMove)
        {
            playerMove.ExcuteMove(input.InputMove, cameraPos, anim);
        }
    }

    // ジャンプ
    void Jump()
    {
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Jmp_Base_A_Keep" && gl.IsGround())
        {
            anim.SetTrigger("IsIanding");
            anim.SetBool("IsJump", false);
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
            if (isPush)
            {
                iObject.Move(iObject.Box_rb(), input.InputMove, this.gameObject,anim);
            }
            else
            {
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
                isMove = false;
            }
            if (isClimb)
            {
                anim.SetBool("IsClimb",true);
                iClimb.ClimbPlayer(rb);
            }
        }
        else
        {
            if (isClimb)
            {
                this.transform.DOMove(this.transform.position + 0.2f * Vector3.up + col.radius * 2f * this.transform.forward, 0.1f);
            }
            isClimb = false;
            anim.SetBool("IsClimb", false);
            isMove = true;
        }
    }

    void Fly()
    {
        if(input.InputJump)
        {
            iFly.Fly(rb, iFly.FlyFrag(this.gameObject), anim);
        }
    }
}
