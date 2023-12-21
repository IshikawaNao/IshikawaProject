using UniRx;

public class StageSelectUIPresenter 
{
    StageSelectUIModel model;
    // 選択番号
    int selectionNumbar = 0;

    private readonly ReactiveProperty<bool> inputValue = new ReactiveProperty<bool>();

    public StageSelectUIPresenter(StageSelectUIView _view, int _stageNum)
    {
        model = new StageSelectUIModel(_stageNum);

        model.SelectNumbar
            .Where(x => x != selectionNumbar)
            .Subscribe(x =>
            {
                _view.ExitUIAnimation(selectionNumbar);
                _view.EnterUIAnimation(x);
                selectionNumbar = x;
            }).AddTo(_view);

        // 選択ボタン押下を検知
        inputValue
            .Subscribe(x => model.SelectNumCahnge(KeyInput.Instance.InputMove))
            .AddTo(_view);
    }

    // ボタン選択
    public void HandleInput()
    {
        inputValue.Value = KeyInput.Instance.PressedMove;
    }
}
