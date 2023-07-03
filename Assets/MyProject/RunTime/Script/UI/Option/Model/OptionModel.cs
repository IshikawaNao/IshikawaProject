using UnityEngine;
using DG.Tweening;
using System;

public class OptionModel
{ 
    private const int MinNum = 0;
    // 最大値
    private  int[] MaxNum;
    private int StateMaxNum; 
    // 選択時のディレイのためのフラグ
    private bool isSelect = true;
    public bool IsSelect { get => isSelect; }
    // Xステート番号　Yボタン番号
    Vector2 selectvalue;
    public Vector2 SelectValue { get { return selectvalue; } }

    // ディレイタイム
    private const float DelayTime = 0.4f;

    // ステートとボタンの最大数を代入
    public OptionModel(int _statemaxnum, int[] _maxnum)
    {
        StateMaxNum = _statemaxnum;
        MaxNum = _maxnum;
    }
    
    // 選択ボタンの切り替え
    private void SelectNum(Vector2 value)
    {
        var num = selectvalue.y;
        if (value.y > 0)
        {
            num--;
        }
        else if (value.y < 0)
        {
            num++;
        }
        // 最大値を超えないよう補正
        selectvalue.y = Math.Clamp(num, MinNum, MaxNum[(int)selectvalue.x]);
    }
    // 選択ステートの切り替え
    private void StateNum(Vector2 value)
    {
        var num = selectvalue;
        if (num.y == 0)
        {
            if (value.x > 0)
            {
                num.x++;
            }
            else if (value.x < 0)
            {
                num.x--;
            }
            // 最大値を超えないよう補正
            selectvalue.x = Math.Clamp(num.x, 0, StateMaxNum);
        }
    }
    /// <summary> 入力による切り替え</summary>
    public void SelectCahge(Vector2 value)
    {
        if (!isSelect)
        {
            return;
        }
        isSelect = false;
        // DelayTime待ってディレイ解除
        DOVirtual.DelayedCall(DelayTime, () => isSelect = true);

        SelectNum(value);
        StateNum(value);
    }

    // 初期化
    public void ReturnOption()
    {
        selectvalue = Vector2.zero;
    }
}

