/// <summary>
/// プレイヤー制御
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    
    bool isPush = false;　　  // オブジェクトが動くフラグ

    bool isClimb = false;　　 // オブジェクトを登るフラグ

    bool isMove = true;　    // プレイヤーの移動のフラグ

    [SerializeField]
    GameObject cameraPos;   // カメラの位置

    Rigidbody rb;
    CapsuleCollider col;
    GroundLayer gl;
    KeyInput input;

    IPlayerMover iMover;
    IMoveObject iObject;
    IClimb iClimb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        gl = GetComponent<GroundLayer>();
        input = GetComponent<KeyInput>();

        iMover = new PlayerMove(rb, this.gameObject);
        iObject = new PushObject();
        iClimb = new PlayerClimb();
    }

    
    void Update()
    {
        Jump();
        ObjectMove();
        ObjectClimb();
    }

    private void FixedUpdate()
    {
        // 移動
        if(isMove)
        {
            iMover.Move(input.InputMove, cameraPos);
        }
    }

    // ジャンプ
    void Jump()
    {
        if (input.InputJump)
        {
            if (gl.IsGround())
            {
                iMover.Jump();
            }
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
                iObject.Move(iObject.Box_rb(), input.InputMove, this.gameObject);
            }
        }
        else
        {
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
                iClimb.ClimbPlayer(rb);
            }
        }
        else
        {
            if (isClimb)
            {
                transform.DOMove(this.transform.position + 0.2f * Vector3.up + col.radius * 2f * this.transform.forward, 0.1f);
            }
            isClimb = false;
            isMove = true;
        }
    }
}
