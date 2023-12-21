using UniRx;

public class TitleUIPresenter 
{    
    TitleUIModel model;
    // 選択番号
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

   // ボタン選択
   public void HandleInput()
   {
        if (KeyInput.Instance.PressedMove)
        {
            model.SelectNumCahnge(KeyInput.Instance.InputMove);
        }
   }
}
