using System;
using UniRx;
using UnityEngine;

public enum PauseUIState
{
    Option,
    MainScene,
    TitleScene,
    PauseState
}

public class PauseModel
{
    private int num = 0;
    public int Num { get { return num; } }
    private const int MinNum = 0;
    private int MaxNum;
    private PauseUIState currentState = PauseUIState.PauseState;
    public PauseUIState CurrentState { get { return currentState; } }

    private ReactiveProperty<bool> isSelect = new ReactiveProperty<bool>(true);
    public IReadOnlyReactiveProperty<bool> IsSelect { get { return isSelect; } }

    private const float DelayTime = 0.4f;

    public PauseModel(int _MaxNum)
    {
        MaxNum = _MaxNum;
    }

    public int SelectNum(Vector2 value)
    {
        if (!isSelect.Value) { return num; }

        isSelect.Value = false;
        // UniRx��Observable.Timer���g�p���ăf�B���C������
        Observable.Timer(TimeSpan.FromSeconds(DelayTime))
            .Subscribe(_ => isSelect.Value = true);

        // ���͂Ɋ�Â��đI�����ڂ�ύX
        if (value.y > 0) { num--; }
        else if (value.y < 0) { num++; }

        // �I��͈͂𐧌�
        num = Mathf.Clamp(num, MinNum, MaxNum);
        return num;
    }

    public void Decision()
    {
        // ���݂̏�Ԃɉ����Ď��̏�ԂɑJ��
        switch (num)
        {
            case (int)PauseUIState.Option:
                currentState = PauseUIState.Option;
                break;
            case (int)PauseUIState.MainScene:
                currentState = PauseUIState.MainScene;
                break;
            case (int)PauseUIState.TitleScene:
                currentState = PauseUIState.TitleScene;
                break;
                // Add more cases as needed
        }
    }
}

