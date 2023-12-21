using UniRx;
using UnityEngine;

public class OptionPresenter
{
    OptionModel model;
    // �I��ԍ�
    Vector2 selectionNumbar;
    Vector2 screenSizeNum = new Vector2(1, 1);

    private readonly ReactiveProperty<bool> inputValue = new ReactiveProperty<bool>();

    public OptionPresenter(OptionView _view, int _stateNum , int[] _buttonsNum, ScreenSizeSet screenSize)
    {
        model = new OptionModel(_stateNum,_buttonsNum);

        // �I���{�^���̐؂�ς������m
        model.SelectValue
            .Where(x => x != selectionNumbar)
            .Subscribe(x =>
            {
                _view.ExitUIAnimation(selectionNumbar);
                _view.EnterUIAnimation(x);
                selectionNumbar = x;
            }).AddTo(_view);

        // �I���{�^�����������m
        inputValue
            .Subscribe(x => model.SelectCahge(KeyInput.Instance.InputMove))
            .AddTo(_view);

        // ����{�^�����������m
        KeyInput.Instance.DecisionInputDetection
            .Where(x => selectionNumbar == screenSizeNum && x)
            .Subscribe(x =>
            {
                screenSize.SetScreen();
            }).AddTo(_view);
    }

    // �{�^���I��
    public void HandleInput()
    {
        inputValue.Value = KeyInput.Instance.PressedMove;
    }

    public void Initialization()
    {
        model.InitializationNum();
    }
}
