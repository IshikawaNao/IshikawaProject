using UnityEngine;
using DG.Tweening;

/// <summary>
/// �v���C���[����
/// </summary>
public class PlayerController : MonoBehaviour
{
    bool isClimb = false;       // �I�u�W�F�N�g��o��t���O

    bool isMove = true;         // �v���C���[�̈ړ��̃t���O

    [SerializeField,Header("�|�[�Y���")]
    GameObject Pause;       
    [SerializeField,Header("�I�v�V����")]
    OperationExplanationMove operation;
    [SerializeField,Header("�X�e�[�W�}�l�[�W���[")]
    StageManager sm;
    [SerializeField,Header("�`���[�g���A��")]
    TutorialStageDisplay tutorial;
    [SerializeField,Header("�\�i�[�G�t�F�N�g")]
    SonarEffect sonar;       
    [SerializeField,Header("Rigidbody")]
    Rigidbody rb;           
    [SerializeField,Header("�R���C�_�[")]
    CapsuleCollider col;
    [SerializeField, Header("animator")]
    Animator anim; 

    SwitchingMove switchMove;// �ړ��̐؂�ւ�
    KeyInput input;         // ���͎󂯎��
    GroundRay gr;         // �ڒn����
    PushObject push;    // Push�I�u�W�F�N�g�󂯎��
    public PushObject PushMoveObject { get { return push; } }
    IClimb iClimb;
    PlayerJump jump;

    void Start()
    {
        input = KeyInput.Instance;

        switchMove = new SwitchingMove();
        gr = new GroundRay(this.gameObject);
        push = new PushObject(this.gameObject,anim);
        iClimb = new PlayerClimb();
        jump = new PlayerJump(rb,anim,this.gameObject);

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
            Sonar();
        }
        jump.Jump(gr.IsGround(),isClimb);
    }
    void OperationOpen()
    {
        operation.ClimbCheck(iClimb.Climb(this.gameObject));
        operation.PushCheck(push.CanPush());
        operation.JumpCheck(jump.FlyFrag());
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
            jump.Fly(jump.FlyFrag());
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
