using UniRx;
using UnityEngine;

public class OptionPresenter
{
    OptionModel model;
    // 選択番号
    Vector2 selectionNumbar;
    Vector2 screenSizeNum = new Vector2(1, 1);

    private readonly ReactiveProperty<bool> inputValue = new ReactiveProperty<bool>();

    public OptionPresenter(OptionView _view, int _stateNum , int[] _buttonsNum, ScreenSizeSet screenSize)
    {
        model = new OptionModel(_stateNum,_buttonsNum);

        // 選択ボタンの切り変わりを検知
        model.SelectValue
            .Where(x => x != selectionNumbar)
            .Subscribe(x =>
            {
                _view.ExitUIAnimation(selectionNumbar);
                _view.EnterUIAnimation(x);
                selectionNumbar = x;
            }).AddTo(_view);

        // 選択ボタン押下を検知
        inputValue
            .Subscribe(x => model.SelectCahge(KeyInput.Instance.InputMove))
            .AddTo(_view);

        // 決定ボタン押下を検知
        KeyInput.Instance.DecisionInputDetection
            .Where(x => selectionNumbar == screenSizeNum && x)
            .Subscribe(x =>
            {
                screenSize.SetScreen();
            }).AddTo(_view);
    }

    // ボタン選択
    public void HandleInput()
    {
        inputValue.Value = KeyInput.Instance.PressedMove;
    }

    public void Initialization()
    {
        model.InitializationNum();
    }
}
