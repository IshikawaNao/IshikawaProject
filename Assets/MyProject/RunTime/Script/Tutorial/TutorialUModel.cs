using System;
using DG.Tweening;

/// <summary>
/// 選択確認UIModel
/// </summary>
public class TutorialUModel
{
    // 選択されているステートの数値
    private int num = 0;
    public int Num { get => num; }
    private const int MinNum = 0;
    // 選択時のディレイのためのフラグ
    private bool isSelect = true;
    public bool IsSelect { get => isSelect; }
    // ディレイタイム
    private const float DelayTime = 0.2f;

    /// <summary> 入力によるステートの切り替え</summary>
    public void SelectNum(float value , int MaxNum)
    {
        if (!isSelect)
        {
            return;
        }
        isSelect = false;
        // DelayTime待ってディレイ解除
        DOVirtual.DelayedCall(DelayTime, () => isSelect = true);

        if (value > 0)
        {
            num++;
        }
        else if (value < 0)
        {
            num--;
        }

        // 最大値を超えないよう補正
        num = Math.Clamp(num, MinNum, MaxNum);
    }
}
