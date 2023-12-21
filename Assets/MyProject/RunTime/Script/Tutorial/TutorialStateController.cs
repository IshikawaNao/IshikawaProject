using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using DG.Tweening;

public class TutorialStateController : MonoBehaviour
{
    [SerializeField, Header("プレイヤーコントローラー")]
    PlayerController playerCon;
    [SerializeField, Header("チュートリアルデータ")]
    TutorialData data;
    [SerializeField, Header("チュートリアルパネル")]
    GameObject tutorialPanel;
    [SerializeField, Header("チュートリアルテキスト")]
    TextMeshProUGUI text;
    [SerializeField, Header("チュートリアル映像")]
    VideoPlayer video;
    // UIアニメーション
    Tween tween;
    // ゲームの状態リスト
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
        // チュートリアルステージ以外のステージの場合破壊する
        if (StageNumberSelect.Instance.StageNumber != 0) { Destroy(this); }

        // ゲームの状態を順番に登録
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

        // 最初の状態の開始処理
        state[currentState].Enter();
    }

    void PlayerStateMonitoring()
    {
        // 状態を更新
        var stateAction = state[currentState].Update();

        // 次の状態へ遷移するか判定
        if (stateAction == BaseState.StateAction.STATE_ACTION_NEXT)
        {
            // 状態の終了処理
            state[currentState].Exit();
            // 次の状態へ
            ++currentState;
            // 状態の開始処理
            state[currentState].Enter();
        }
    }

    // ステートベース抽象クラス
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


    // 移動チュートリアルステート
    class MoveState : BaseState
    {
        public MoveState(TutorialStateController c) : base(c) { }
        public override void Enter()
        {
            // チュートリアルパネル表示
            Controller.tutorialPanel.SetActive(true);
            // テキストと動画をデータクラスから代入
            Controller.text.text = Controller.data.Data[Controller.currentState].explanatory;
            Controller.video.clip = Controller.data.Data[Controller.currentState].clip;
            Controller.tween.Play();
        }
        public override StateAction Update()
        {
            // プレイヤーのステートを検知してパネルを切り替える
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

    // 押すチュートリアルステート
    class PushState : BaseState
    {
        public PushState(TutorialStateController c) : base(c) { }
        public override void Enter()
        {
            // チュートリアルパネル表示
            Controller.tutorialPanel.SetActive(true);
            // テキストと動画をデータクラスから代入
            Controller.text.text = Controller.data.Data[Controller.currentState].explanatory;
            Controller.video.clip = Controller.data.Data[Controller.currentState].clip;
            Controller.tween.Restart(false);
        }
        public override StateAction Update()
        {
            // プレイヤーのステートを検知してパネルを切り替える
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

    // 登るチュートリアルステート
    class ClimbState : BaseState
    {
        public ClimbState(TutorialStateController c) : base(c) { }
        public override void Enter()
        {
            // チュートリアルパネル表示
            Controller.tutorialPanel.SetActive(true);
            // テキストと動画をデータクラスから代入
            Controller.text.text = Controller.data.Data[Controller.currentState].explanatory;
            Controller.video.clip = Controller.data.Data[Controller.currentState].clip;
            Controller.tween.Restart(false);
        }
        public override StateAction Update()
        {
            // プレイヤーのステートを検知してパネルを切り替える
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

    // 検知チュートリアルステート
    class DetectionObjState : BaseState
    {
        // 検知ギミックオブジェクト
        DetectionObject detectionObj;

        public DetectionObjState(TutorialStateController c) : base(c) { }
        public override void Enter()
        {
            detectionObj = GameObject.FindWithTag("SensingFloor").GetComponent<DetectionObject>();
            // チュートリアルパネル表示
            Controller.tutorialPanel.SetActive(true);
            // テキストと動画をデータクラスから代入
            Controller.text.text = Controller.data.Data[Controller.currentState].explanatory;
            Controller.video.clip = Controller.data.Data[Controller.currentState].clip;
            Controller.tween.Restart(false);
        }
        public override StateAction Update()
        {
            // 地面のオブジェクトの状態を検知してパネルを切り替える
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

    // テレポートチュートリアルステート
    class TeleportState : BaseState
    {
        public TeleportState(TutorialStateController c) : base(c) { }
        public override void Enter()
        {
            // チュートリアルパネル表示
            Controller.tutorialPanel.SetActive(true);
            // テキストと動画をデータクラスから代入
            Controller.text.text = Controller.data.Data[Controller.currentState].explanatory;
            Controller.video.clip = Controller.data.Data[Controller.currentState].clip;
            Controller.tween.Restart(false);
        }
        public override StateAction Update()
        {
            // プレイヤーのステートを検知してパネルを切り替える
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

    // ソナーチュートリアルステート
    class BeamState : BaseState
    {
        SonarVisibleTouch bridge;
        public BeamState(TutorialStateController c) : base(c) { }
        public override void Enter()
        {
            bridge = GameObject.FindWithTag("Bridge").GetComponent<SonarVisibleTouch>();
            // チュートリアルパネル表示
            Controller.tutorialPanel.SetActive(true);
            // テキストと動画をデータクラスから代入
            Controller.text.text = Controller.data.Data[Controller.currentState].explanatory;
            Controller.video.clip = Controller.data.Data[Controller.currentState].clip;
            Controller.tween.Restart(false);
        }
        public override StateAction Update()
        {
            // 透明状態解除を検知してパネルを切り替える
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

    // ソナーチュートリアルステート
    class GoalState : BaseState
    {
        public GoalState(TutorialStateController c) : base(c) { }
        public override void Enter()
        {
            // チュートリアルパネル表示
            Controller.tutorialPanel.SetActive(true);
            // テキストと動画をデータクラスから代入
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
