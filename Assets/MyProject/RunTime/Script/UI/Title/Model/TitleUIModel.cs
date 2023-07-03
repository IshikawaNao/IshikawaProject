using UnityEngine;
using DG.Tweening;
using System;

public class TitleUIModel 
{
    // 選択されているステートの数値
    private int num = 0;
    public int Num { get { return num; } }
    private const int MinNum = 0;
    private  int MaxNum;
    // 選択時のディレイのためのフラグ
    private bool isSelect = true;
    public bool IsSelect { get => isSelect; }
    // ディレイタイム
    private const float DelayTime = 0.4f;

    public TitleUIModel (int _MaxNum)
    {
        MaxNum = _MaxNum;
    }

    /// <summary> 入力によるステートの切り替え</summary>
    public int SelectNum(Vector2 value)
    {
        if (!isSelect)
        {
            return num;
        }
        isSelect = false;
        // DelayTime待ってディレイ解除
        DOVirtual.DelayedCall(DelayTime, () => isSelect = true);

        if (value.y > 0)
        {
            num--;
        }
        else if (value.y < 0)
        {
            num++;
        }

        // 最大値を超えないよう補正
        num = Math.Clamp(num, MinNum, MaxNum);
        return num;
    }

    public int StateNum(int num)
    {
        var state = 0;
        switch (num)
        {
            case 0:
                state = 2;
                break;
            case 1:
                state = 1;
                break;
            case 2:
                state = 3;
                break;
            case 3:
                state = 4;
                break;
        }
        return state;
    }

}
