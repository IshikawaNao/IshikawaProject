using System;
using UniRx;
using UnityEngine;

public class OptionModel
{
    private const int MinNum = 0;
    // 最大値
    private int[] MaxNum;
    private int StateMaxNum;
    // 選択時のディレイのためのフラグ
    private bool isSelect = true;
    public bool IsSelect { get => isSelect; }
    // Xステート番号　Yボタン番号
    Vector2 selectNum;

    private readonly ReactiveProperty<Vector2> selectValue = new ReactiveProperty<Vector2>();
    public IReadOnlyReactiveProperty<Vector2> SelectValue => selectValue;

    // ディレイタイム
    private const float DelayTime = 0.2f;

    // ステートとボタンの最大数を代入
    public OptionModel(int _statemaxnum, int[] _maxnum)
    {
        StateMaxNum = _statemaxnum;
        MaxNum = _maxnum;
    }

    // 選択ボタンの切り替え
    private void SelectNumbar(Vector2 value)
    {
        // DelayTimeが終わるまで処理に入らないようにする
        if (!isSelect) { return; }

        isSelect = false;
        Observable.Timer(TimeSpan.FromSeconds(DelayTime))
            .Subscribe(_ => isSelect = true);

        var num = selectNum.y;
        if (value.y > 0) { num--; }
        else if (value.y < 0) { num++; }
        // 最大値を超えないよう補正
        selectNum.y = Math.Clamp(num, MinNum, MaxNum[(int)selectNum.x]);
        selectValue.Value = selectNum;
    }
    // 選択ステートの切り替え
    private void StateNumbar(Vector2 value)
    {
        // DelayTimeが終わるまで処理に入らないようにする
        if (!isSelect || selectNum.y != 0) { return; }
        isSelect = false;
        Observable.Timer(TimeSpan.FromSeconds(DelayTime))
            .Subscribe(_ => isSelect = true);
        var num = selectNum;

        if (value.x > 0){ num.x++; }
        else if (value.x < 0) {  num.x--; }
        // 最大値を超えないよう補正
        selectNum.x = Math.Clamp(num.x, 0, StateMaxNum);
        selectValue.Value = selectNum;
    }
    /// <summary> 入力による切り替え</summary>
    public void SelectCahge(Vector2 value)
    {
        if(value.x != 0) 
        {
            StateNumbar(value); 
        }
        else if(value.y != 0) { }
        {
            SelectNumbar(value);
        }
    }

    // 初期化
    public void InitializationNum()
    {
        selectNum = Vector2.zero;
        selectValue.Value = selectNum;
    }
}
