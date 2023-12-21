using UniRx;

public class EndUIPresenter 
{
    EndUIModel model;
    // 選択番号
    int selectionNumbar = 0;

    public EndUIPresenter(EndUIView _view, int _buttonsNum)
    {
        model = new EndUIModel(_buttonsNum - 1);

        model.SelectNumbar
            .Where(x => x != selectionNumbar)
            .Subscribe(x =>
            {
                _view.ExitUIAnimation(selectionNumbar);
                _view.EnterUIAnimation(x);
                selectionNumbar = x;
            }).AddTo(_view);
        KeyInput.Instance.DecisionInputDetection
            .Where(x => x && selectionNumbar != 0)
            .Subscribe(x =>
            {
                model.Decision();
            }).AddTo(_view);
    }

    // ボタン選択
    public void HandleInput()
    {
        if (KeyInput.Instance.PressedMove)
        {
            model.SelectNumCahnge(KeyInput.Instance.InputMove);
        }
        if(KeyInput.Instance.PressedMove) 
        {

        }
    }
}
