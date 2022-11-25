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

    PlayerMove playerMove;  // �v���C���[�̈ړ�
    Rigidbody rb;           // rigidbody
    CapsuleCollider col;    // �R���C�_�[
    GroundLayer gl;         // �ڒn����
    KeyInput input;         // ���͎󂯎��

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
        // �ړ�
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

    // �W�����v
    void Jump()
    {
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Jmp_Base_A_Keep" && gl.IsGround())
        {
            anim.SetTrigger("IsIanding");
            anim.SetBool("IsJump", false);
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
