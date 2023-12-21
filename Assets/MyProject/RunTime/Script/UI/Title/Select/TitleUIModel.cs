using SoundSystem;
using System;
using UniRx;
using UnityEngine;

public class TitleUIModel 
{
    private readonly IntReactiveProperty selectNumbar = new IntReactiveProperty(100);
    public IReadOnlyReactiveProperty<int> SelectNumbar => selectNumbar;

    private int selectedUIIndex = 0;

    private const int MinIndex = 0;
    private int maxIndex;

    private bool isSelect = true;

    private const float DelayTime = 0.2f;

    public TitleUIModel(int _maxIndex)
    {
        maxIndex = _maxIndex;
        selectNumbar.Value = MinIndex;
    }

    /// <summary> 現在選択されている項目の数値を返す</summary>
    public void SelectNumCahnge(Vector2 value)
    {
        // DelayTimeが終わるまでは処理を返す
        if (!isSelect) { return; }
        
        isSelect = false;

        Observable.Timer(TimeSpan.FromSeconds(DelayTime))
        .Subscribe(_ => isSelect = true);

        // ↑が押されたら-- ↓が押されたら++
        if (value.y > 0) { selectedUIIndex--; }
        else if (value.y < 0) { selectedUIIndex++; }

        // 最大値を出ないように補正 
        selectedUIIndex = Mathf.Clamp(selectedUIIndex, MinIndex, maxIndex);
        // 選択UIを更新
        selectNumbar.Value = selectedUIIndex;
        // 選択SE外観
        SoundManager.Instance.PlayOneShotSe((int)SEList.Select);
    }
}