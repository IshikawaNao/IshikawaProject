using UnityEngine;
using DG.Tweening;

/// <summary>
/// �v���C���[����
/// </summary>
public class PlayerController : MonoBehaviour
{
    bool isPush = false;        // �I�u�W�F�N�g�������t���O
    public bool Push { get { return isPush; } }

    bool isClimb = false;       // �I�u�W�F�N�g��o��t���O

    public bool IsMove { get; set; } = true;         // �v���C���[�̈ړ��̃t���O

    bool isJump = false;

    [SerializeField]
    GameObject Pause;       // �|�[�Y���

    [SerializeField]
    OperationExplanationMove operation;

    SwitchingMove switchMove;// �ړ��̐؂�ւ�
    Rigidbody rb;           // rigidbody
    CapsuleCollider col;    // �R���C�_�[
    GroundRay gl;         // �ڒn����
    KeyInput input;         // ���͎󂯎��

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

    // �W�����v
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

    // �I�u�W�F�N�g�𓮂���
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

    // �I�u�W�F�N�g��o��
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

    // ���֔��
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
