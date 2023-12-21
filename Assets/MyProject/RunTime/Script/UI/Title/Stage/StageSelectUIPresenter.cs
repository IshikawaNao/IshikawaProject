using UniRx;

public class StageSelectUIPresenter 
{
    StageSelectUIModel model;
    // �I��ԍ�
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

        // �I���{�^�����������m
        inputValue
            .Subscribe(x => model.SelectNumCahnge(KeyInput.Instance.InputMove))
            .AddTo(_view);
    }

    // �{�^���I��
    public void HandleInput()
    {
        inputValue.Value = KeyInput.Instance.PressedMove;
    }
}
