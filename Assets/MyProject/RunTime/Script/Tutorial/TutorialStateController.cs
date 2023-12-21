using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using DG.Tweening;

public class TutorialStateController : MonoBehaviour
{
    [SerializeField, Header("�v���C���[�R���g���[���[")]
    PlayerController playerCon;
    [SerializeField, Header("�`���[�g���A���f�[�^")]
    TutorialData data;
    [SerializeField, Header("�`���[�g���A���p�l��")]
    GameObject tutorialPanel;
    [SerializeField, Header("�`���[�g���A���e�L�X�g")]
    TextMeshProUGUI text;
    [SerializeField, Header("�`���[�g���A���f��")]
    VideoPlayer video;
    // UI�A�j���[�V����
    Tween tween;
    // �Q�[���̏�ԃ��X�g
    List<BaseState> state;
    int currentState;

    void Start()
    {
        Init();
        var pos = tutorialPanel.GetComponent<RectTransform>();
        tween = pos.DOAnchorPos(new Vector2(-700f, 0), 0.6f)
                                        .SetEase(Ease.OutSine)
                                        .SetAutoKill(false);
    }

    void Update()
    {
        PlayerStateMonitoring();
    }

    void Init()
    {
        // �`���[�g���A���X�e�[�W�ȊO�̃X�e�[�W�̏ꍇ�j�󂷂�
        if (StageNumberSelect.Instance.StageNumber != 0) { Destroy(this); }

        // �Q�[���̏�Ԃ����Ԃɓo�^
        state = new List<BaseState>
        {
            new MoveState(this),
            new PushState(this),
            new ClimbState(this),
            new DetectionObjState(this),
            new TeleportState(this),
            new BeamState(this),
            new GoalState(this),
        };

        // �ŏ��̏�Ԃ̊J�n����
        state[currentState].Enter();
    }

    void PlayerStateMonitoring()
    {
        // ��Ԃ��X�V
        var stateAction = state[currentState].Update();

        // ���̏�Ԃ֑J�ڂ��邩����
        if (stateAction == BaseState.StateAction.STATE_ACTION_NEXT)
        {
            // ��Ԃ̏I������
            state[currentState].Exit();
            // ���̏�Ԃ�
            ++currentState;
            // ��Ԃ̊J�n����
            state[currentState].Enter();
        }
    }

    // �X�e�[�g�x�[�X���ۃN���X
    abstract class BaseState
    {
        public TutorialStateController Controller { get; set; }

        public enum StateAction
        {
            STATE_ACTION_WAIT,
            STATE_ACTION_NEXT
        }

        public BaseState(TutorialStateController c) { Controller = c; }

        public virtual void Enter() { }
        public virtual StateAction Update() { return StateAction.STATE_ACTION_NEXT; }
        public virtual void Exit() { }
    }


    // �ړ��`���[�g���A���X�e�[�g
    class MoveState : BaseState
    {
        public MoveState(TutorialStateController c) : base(c) { }
        public override void Enter()
        {
            // �`���[�g���A���p�l���\��
            Controller.tutorialPanel.SetActive(true);
            // �e�L�X�g�Ɠ�����f�[�^�N���X������
            Controller.text.text = Controller.data.Data[Controller.currentState].explanatory;
            Controller.video.clip = Controller.data.Data[Controller.currentState].clip;
            Controller.tween.Play();
        }
        public override StateAction Update()
        {
            // �v���C���[�̃X�e�[�g�����m���ăp�l����؂�ւ���
            if (Controller.playerCon.CurrentState.State == PlayerState.Move)
            {
                Controller.tween.SmoothRewind();
                return StateAction.STATE_ACTION_NEXT;
            }
            return StateAction.STATE_ACTION_WAIT;
        }
        public override void Exit()
        {
           
        }
    }

    // �����`���[�g���A���X�e�[�g
    class PushState : BaseState
    {
        public PushState(TutorialStateController c) : base(c) { }
        public override void Enter()
        {
            // �`���[�g���A���p�l���\��
            Controller.tutorialPanel.SetActive(true);
            // �e�L�X�g�Ɠ�����f�[�^�N���X������
            Controller.text.text = Controller.data.Data[Controller.currentState].explanatory;
            Controller.video.clip = Controller.data.Data[Controller.currentState].clip;
            Controller.tween.Restart(false);
        }
        public override StateAction Update()
        {
            // �v���C���[�̃X�e�[�g�����m���ăp�l����؂�ւ���
            if (Controller.playerCon.CurrentState.State == PlayerState.Push)
            {
                return StateAction.STATE_ACTION_NEXT;
            }
            return StateAction.STATE_ACTION_WAIT;
        }
        public override void Exit()
        {

        }
    }

    // �o��`���[�g���A���X�e�[�g
    class ClimbState : BaseState
    {
        public ClimbState(TutorialStateController c) : base(c) { }
        public override void Enter()
        {
            // �`���[�g���A���p�l���\��
            Controller.tutorialPanel.SetActive(true);
            // �e�L�X�g�Ɠ�����f�[�^�N���X������
            Controller.text.text = Controller.data.Data[Controller.currentState].explanatory;
            Controller.video.clip = Controller.data.Data[Controller.currentState].clip;
            Controller.tween.Restart(false);
        }
        public override StateAction Update()
        {
            // �v���C���[�̃X�e�[�g�����m���ăp�l����؂�ւ���
            if (Controller.playerCon.CurrentState.State == PlayerState.Climb)
            {
                return StateAction.STATE_ACTION_NEXT;
            }
            return StateAction.STATE_ACTION_WAIT;
        }
        public override void Exit()
        {

        }
    }

    // ���m�`���[�g���A���X�e�[�g
    class DetectionObjState : BaseState
    {
        // ���m�M�~�b�N�I�u�W�F�N�g
        DetectionObject detectionObj;

        public DetectionObjState(TutorialStateController c) : base(c) { }
        public override void Enter()
        {
            detectionObj = GameObject.FindWithTag("SensingFloor").GetComponent<DetectionObject>();
            // �`���[�g���A���p�l���\��
            Controller.tutorialPanel.SetActive(true);
            // �e�L�X�g�Ɠ�����f�[�^�N���X������
            Controller.text.text = Controller.data.Data[Controller.currentState].explanatory;
            Controller.video.clip = Controller.data.Data[Controller.currentState].clip;
            Controller.tween.Restart(false);
        }
        public override StateAction Update()
        {
            // �n�ʂ̃I�u�W�F�N�g�̏�Ԃ����m���ăp�l����؂�ւ���
            if (detectionObj.Placed)
            {
                return StateAction.STATE_ACTION_NEXT;
            }
            return StateAction.STATE_ACTION_WAIT;
        }
        public override void Exit()
        {

        }
    }

    // �e���|�[�g�`���[�g���A���X�e�[�g
    class TeleportState : BaseState
    {
        public TeleportState(TutorialStateController c) : base(c) { }
        public override void Enter()
        {
            // �`���[�g���A���p�l���\��
            Controller.tutorialPanel.SetActive(true);
            // �e�L�X�g�Ɠ�����f�[�^�N���X������
            Controller.text.text = Controller.data.Data[Controller.currentState].explanatory;
            Controller.video.clip = Controller.data.Data[Controller.currentState].clip;
            Controller.tween.Restart(false);
        }
        public override StateAction Update()
        {
            // �v���C���[�̃X�e�[�g�����m���ăp�l����؂�ւ���
            if (Controller.playerCon.CurrentState.State == PlayerState.Teleport)
            {
                return StateAction.STATE_ACTION_NEXT;
            }
            return StateAction.STATE_ACTION_WAIT;
        }
        public override void Exit()
        {

        }
    }

    // �\�i�[�`���[�g���A���X�e�[�g
    class BeamState : BaseState
    {
        SonarVisibleTouch bridge;
        public BeamState(TutorialStateController c) : base(c) { }
        public override void Enter()
        {
            bridge = GameObject.FindWithTag("Bridge").GetComponent<SonarVisibleTouch>();
            // �`���[�g���A���p�l���\��
            Controller.tutorialPanel.SetActive(true);
            // �e�L�X�g�Ɠ�����f�[�^�N���X������
            Controller.text.text = Controller.data.Data[Controller.currentState].explanatory;
            Controller.video.clip = Controller.data.Data[Controller.currentState].clip;
            Controller.tween.Restart(false);
        }
        public override StateAction Update()
        {
            // ������ԉ��������m���ăp�l����؂�ւ���
            if (!bridge.IsInvisible)
            {
                return StateAction.STATE_ACTION_NEXT;
            }
            return StateAction.STATE_ACTION_WAIT;
        }
        public override void Exit()
        {

        }
    }

    // �\�i�[�`���[�g���A���X�e�[�g
    class GoalState : BaseState
    {
        public GoalState(TutorialStateController c) : base(c) { }
        public override void Enter()
        {
            // �`���[�g���A���p�l���\��
            Controller.tutorialPanel.SetActive(true);
            // �e�L�X�g�Ɠ�����f�[�^�N���X������
            Controller.text.text = Controller.data.Data[Controller.currentState].explanatory;
            Controller.video.clip = Controller.data.Data[Controller.currentState].clip;
            Controller.tween.Restart(false);
        }
        public override StateAction Update()
        {
            return StateAction.STATE_ACTION_WAIT;
        }
        public override void Exit()
        {
          
        }
    }
}
