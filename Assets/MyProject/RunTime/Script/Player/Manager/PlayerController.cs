using UnityEngine;

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

    PlayerStatecontroller playerState;

    KeyInput input;         // ���͎󂯎��
    GroundRay gr;         // �ڒn����
    PushObject push;    // Push�I�u�W�F�N�g�󂯎��
    public PushObject PushMoveObject { get { return push; } }
    PlayerClimb climb;
    PlayerJump jump;

    void Start()
    {
        input = KeyInput.Instance;
        gr = new GroundRay(this.gameObject);
        push = new PushObject(this.gameObject,anim);
        climb = new PlayerClimb(this.gameObject);
        jump = new PlayerJump(rb,anim,this.gameObject);

        isMove = true;
        playerState = new PlayerStatecontroller(input, this.gameObject, rb, col, anim, push, climb, sm);
        playerState.Init(this, PlayerState.Idle);
    }

    void Update()
    {
        playerState.Update();
        LimitationOfAction();
        PauseOpen();
    }

    private void FixedUpdate()
    {
        playerState.FixedUpdate();
    }

    void LimitationOfAction()
    {
            push.PushAnimationChange(gr.IsGround(), isClimb, input.PushAction);
        if (IsMoveAction())
        {
            OperationOpen();
            Fly();
            Sonar();
        }
        jump.Jump(gr.IsGround(),isClimb);
    }
    void OperationOpen()
    {
        operation.ClimbCheck(climb.ClimbCheck());
        operation.PushCheck(push.CanPush());
        operation.JumpCheck(gr.IsFly());
    }
    // �ړ��ł��邩
    bool IsMoveAction()
    {
        if (isMove && !sm.Goal && !sm.IsTimeLine && !sonar.IsOnSonar && !tutorial.IsTutoria)
        {
            return true;
        }
        //rb.velocity = Vector3.zero;
        anim.SetBool("IsIdle", true);
        anim.SetBool("IsWalk", false);
        anim.SetBool("IsRan", false);
        return false;
    }

    // ���֔��
    void Fly()
    {
        if(input.InputJump)
        {
           // jump.Fly(jump.FlyFrag());
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
