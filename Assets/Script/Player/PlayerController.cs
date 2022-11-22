using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// �v���C���[����
/// </summary>
public class PlayerController : MonoBehaviour
{
    bool isPush = false;        // �I�u�W�F�N�g�������t���O

    bool isClimb = false;       // �I�u�W�F�N�g��o��t���O

    bool isMove = true;         // �v���C���[�̈ړ��̃t���O

    [SerializeField]
    GameObject cameraPos;   // �J�����̈ʒu

    Rigidbody rb;
    CapsuleCollider col;
    GroundLayer gl;
    KeyInput input;

    IPlayerMover iMover;
    IMoveObject iObject;
    IClimb iClimb;
    IFly iFly;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        gl = GetComponent<GroundLayer>();
        input = GameObject.Find("KeyInput").GetComponent<KeyInput>();

        iMover = new PlayerMove(rb, this.gameObject);
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
        // �ړ�
        if(isMove)
        {
            iMover.Move(input.InputMove, cameraPos);
        }
    }

    // �W�����v
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

    // �I�u�W�F�N�g�𓮂���
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

    // �I�u�W�F�N�g��o��
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
                this.transform.DOMove(this.transform.position + 0.2f * Vector3.up + col.radius * 2f * this.transform.forward, 0.1f);
            }
            isClimb = false;
            isMove = true;
        }
    }

    void Fly()
    {
        if(input.InputJump)
        {
            iFly.Fly(rb, iFly.FlyFrag(this.gameObject));
        }
    }
}