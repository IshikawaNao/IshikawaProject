using SoundSystem;
using System;
using UniRx;
using UnityEngine;

public class StageSelectUIModel 
{
    private readonly IntReactiveProperty selectNumbar = new IntReactiveProperty(100);
    public IReadOnlyReactiveProperty<int> SelectNumbar => selectNumbar;

    private int selectedUIIndex = 0;

    private const int MinIndex = 0;
    private int maxIndex;

    private bool isSelect = true;

    private const float DelayTime = 0.2f;

    public StageSelectUIModel(int _maxIndex)
    {
        maxIndex = _maxIndex;
        selectNumbar.Value = MinIndex;
    }

    /// <summary> ���ݑI������Ă��鍀�ڂ̐��l��Ԃ�</summary>
    public void SelectNumCahnge(Vector2 value)
    {
        // DelayTime���I���܂ł͏�����Ԃ�
        if (!isSelect) { return; }

        isSelect = false;

        Observable.Timer(TimeSpan.FromSeconds(DelayTime))
        .Subscribe(_ => isSelect = true);

        // ���������ꂽ��-- ���������ꂽ��++
        if (value.y > 0) { selectedUIIndex--; }
        else if (value.y < 0) { selectedUIIndex++; }

        // �ő�l���o�Ȃ��悤�ɕ␳ 
        selectedUIIndex = Mathf.Clamp(selectedUIIndex, MinIndex, maxIndex);
        // �I��UI���X�V
        selectNumbar.Value = selectedUIIndex;
        // �I��SE�O��
        SoundManager.Instance.PlayOneShotSe((int)SEList.Select);
    }
}