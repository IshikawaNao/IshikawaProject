using SoundSystem;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PausePresenter : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private GameObject pauseButton;

    [SerializeField]
    private GameObject optionPanel;
    [SerializeField]
    private Image[] selectButton;
    [SerializeField,Header("CanvasGroup")]
    CanvasGroup canvasGroup;

    private PauseView view;
    private PauseModel model;

    private KeyInput input;

    IUIView uiView;

    private ReactiveProperty<int> stateNum = new ReactiveProperty<int>(0);
    public IReadOnlyReactiveProperty<int> StateObservable { get { return stateNum; } }
    ReactiveProperty<bool> escInputValue = new ReactiveProperty<bool>();
    public IReadOnlyReactiveProperty<bool> EscInputValue { get { return escInputValue; } }
    ReactiveProperty<bool> decisionInputSurveillance = new ReactiveProperty<bool>();
    public IReadOnlyReactiveProperty<bool> DecisionInputSurveillance { get { return decisionInputSurveillance; } }

    private void Start()
    {
        view = new PauseView(canvasGroup);
        model = new PauseModel(selectButton.Length - 1);
        input = KeyInput.Instance;
        view.UIMove(selectButton[0]);

        // UniRxのSubscribeを使ってユーザーのアクションを監視
        EscInputValue
            .Where(value => input.EscInput)
            .Subscribe(value => Back())
            .AddTo(this);

        DecisionInputSurveillance
            .Where(value => (pausePanel.activeSelf && pauseButton.activeSelf) && input.Inputdetection)
            .Subscribe(value => DecisionPush())
            .AddTo(this);
    }

    private void Update()
    {
        UISelect();
        view.UIMove(selectButton[model.Num]);
        escInputValue.Value = input.EscInput;
        decisionInputSurveillance.Value = input.DecisionInput;
    }

    private void UISelect()
    {
        if (pausePanel.activeSelf && pauseButton.activeSelf)
        {
            var value = input.InputMove;
            if (value != Vector2.zero && model.IsSelect.Value)
            {
                var num = model.Num;
                if (num == model.SelectNum(value)) { return; }
                view.UIExit();
            }
        }
    }

    private void DecisionPush()
    {
        // 効果音の再生とUIの終了処理
        SoundManager.Instance.PlayOneShotSe((int)SEList.Decision);
        view.UIExit();
        model.Decision();
        stateNum.Value = (int)model.CurrentState;
        // 状態に応じた処理
        switch (model.CurrentState)
        {
            case PauseUIState.Option:
                StateChange();
                break;
            case PauseUIState.MainScene:
                FadeManager.Instance.LoadScene("Main", 1.5f);
                break;
            case PauseUIState.TitleScene:
                FadeManager.Instance.LoadScene("Title", 1.5f);
                break;
                // Add more cases as needed
        }
    }

    // 選択ステート変更
    private async void StateChange()
    {
        // アニメーションが終わるまで処理を止める
        await view.DisableAnimation(pauseButton);
        // 選択されたステートを表示する
        optionPanel.SetActive(true);
        // 選択されたObjectのアニメーションのViewを取得
        uiView = optionPanel.GetComponent<IUIView>();
    }
    // タイトルセレクトへ戻る
    private async void Back()
    {
        if(pauseButton.activeSelf)
        {
            await view.DisableAnimation(pausePanel);
            pauseButton.SetActive(false);
        }
        else if(optionPanel.activeSelf)
        {
            // 選択されていたステートのアニメーション
            await uiView.DisableAnimation();
            // タイトルを表示する
            pauseButton.SetActive(true);
            view.EnabldUIAnimation();
        }
        else
        {
            pausePanel.SetActive(true);
            pauseButton.SetActive(true);
            view.EnabldUIAnimation();
        }

    }

    private void OnDestroy()
    {
        // PauseViewのDisposableの解放
        view.Dispose();
    }
}
