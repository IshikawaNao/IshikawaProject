using UnityEngine;
using DG.Tweening;

/// <summary>
/// �v���C���[����
/// </summary>
public class PlayerController : MonoBehaviour
{
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
    Animator anim;          // animator

    KeyInput input;         // ���͎󂯎��
    GroundRay gr;         // �ڒn����
    PushObject push;    // Push�I�u�W�F�N�g�󂯎��
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
            // �v���C���[�̈ړ��؂�ւ�
            var _playerMover = switchMove.SwitchMove(this.gameObject, rb, input.InputMove, push.IsPush, isClimb, gr.IsGround());
            // �ړ�
            _playerMover.Move(input.InputMove, anim);
            // �v�b�V���I�u�W�F�N�g�ړ�
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
    // �ړ��ł��邩
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

    // �W�����v
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

    // �I�u�W�F�N�g��o��
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

    // ���֔��
    void Fly()
    {
        if(input.InputJump)
        {
            iFly.Fly(rb, iFly.FlyFrag(this.gameObject), anim);
        }
    }

    // �|�[�Y���s���������~�߂�
    void PauseOpen()
    {
        if (Pause.activeSelf){ isMove = false;}
        else { isMove = true; }
    }

    // �\�i�[
    void Sonar()
    {
        if(input.SonarAction�@&& gr.IsGround())
        {
            sonar.Sonar();
            sonar.SonarWaveGeneration(this.transform.position);
        }
    }
}
