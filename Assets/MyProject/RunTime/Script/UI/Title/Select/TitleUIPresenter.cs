using UniRx;

public class TitleUIPresenter 
{    
    TitleUIModel model;
    // �I��ԍ�
    int selectionNumbar = 0;

    public TitleUIPresenter(TitleView _view, int _buttonsNum)
    {
        model = new TitleUIModel(_buttonsNum - 1);

        model.SelectNumbar
            .Where(x => x != selectionNumbar)
            .Subscribe(x =>
            {
                _view.ExitUIAnimation(selectionNumbar);
                _view.EnterUIAnimation(x);
                selectionNumbar = x;
            }).AddTo(_view);
    }

   // �{�^���I��
   public void HandleInput()
   {
        if (KeyInput.Instance.PressedMove)
        {
            model.SelectNumCahnge(KeyInput.Instance.InputMove);
        }
   }
}
