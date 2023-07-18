using DG.Tweening;
using UnityEngine;
using System;
using Unity.VisualScripting.FullSerializer;

public class EndUIModel 
{
    // 選択されているステートの数値
    private int num = 0;
    public int Num { get { return num; } }
    private const int MinNum = 0;
    private int MaxNum = 1;
    // 選択時のディレイのためのフラグ
    private bool isSelect = true;
    public bool IsSelect { get => isSelect; }
    // ディレイタイム
    private const float DelayTime = 0.4f;

    /// <summary> 終了ボタン選択/// </summary>
    public void EndSelectNum(Vector2 value)
    {
        if (!isSelect)
        {
            return;
        }
        isSelect = false;
        // DelayTime待ってディレイ解除
        DOVirtual.DelayedCall(DelayTime, () => isSelect = true);

        if (value.x > 0)
        {
            num--;
        }
        else if (value.x < 0)
        {
            num++;
        }

        // 最大値を超えないよう補正
        num = Math.Clamp(num, MinNum, MaxNum);
    }

    /// <summary> 決定押下時 </summary>
    public void QuitDecision(bool preesed,GameObject _title, GameObject _end)
    {
        if(!preesed)
        {
            return;
        }

        switch (num) 
        {
            case 0:
                _title.SetActive(true);
                _end.SetActive(false);
                break;
            case 1:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                SaveDataManager.Instance.Save();
#else
    Application.Quit();
#endif
                break;
        }
    }

}
