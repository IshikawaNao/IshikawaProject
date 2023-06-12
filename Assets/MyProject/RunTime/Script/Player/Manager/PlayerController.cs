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

    bool isMove = true;         // �v���C���[�̈ړ��̃t���O

    bool isJump = false;        // �W�����v�̃t���O

    [SerializeField]
    GameObject Pause;       // �|�[�Y���

    [SerializeField]
    OperationExplanationMove operation;�@//�I�v�V����

    [SerializeField]
    StageManager sm;
    [SerializeField]
    TutorialStageDisplay tutorial;

    [SerializeField]
    SonarEffect sonar;       // �\�i�[�G�t�F�N�g

    SwitchingMove switchMove;// �ړ��̐؂�ւ�
    [SerializeField]
    Rigidbody rb;           // rigidbody
    [SerializeField]
    CapsuleCollider col;    // �R���C�_�[
    [SerializeField]
    GroundRay gl;         // �ڒn����
    [SerializeField]
    Animator anim;          // animator

    KeyInput input;         // ���͎󂯎��
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
            // �v���C���[�̈ړ��؂�ւ�
            var _playerMover = switchMove.SwitchMove(this.gameObject, rb, input.InputMove, isPush, isClimb, gl.IsGround());
            // �ړ�
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
    // �ړ��ł��邩
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

    // �I�u�W�F�N�g��o��
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

    // ���֔��
    void Fly()
    {
        if(input.InputJump)
        {
            iFly.Fly(rb, iFly.FlyFrag(this.gameObject), anim);
        }
    }

    // �C��
    void PauseOpen()
    {
        if (Pause.activeSelf){ isMove = false;}
        else { isMove = true; }
    }

    // �\�i�[
    void Sonar()
    {
        if(input.SonarAction�@&& gl.IsGround())
        {
            sonar.Sonar();
            sonar.SonarWaveGeneration(this.transform.position);
        }
    }
}
