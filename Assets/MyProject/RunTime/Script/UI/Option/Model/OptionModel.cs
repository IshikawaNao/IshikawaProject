using System;
using UniRx;
using UnityEngine;

public class OptionModel
{
    private const int MinNum = 0;
    // �ő�l
    private int[] MaxNum;
    private int StateMaxNum;
    // �I�����̃f�B���C�̂��߂̃t���O
    private bool isSelect = true;
    public bool IsSelect { get => isSelect; }
    // X�X�e�[�g�ԍ��@Y�{�^���ԍ�
    Vector2 selectNum;

    private readonly ReactiveProperty<Vector2> selectValue = new ReactiveProperty<Vector2>();
    public IReadOnlyReactiveProperty<Vector2> SelectValue => selectValue;

    // �f�B���C�^�C��
    private const float DelayTime = 0.2f;

    // �X�e�[�g�ƃ{�^���̍ő吔����
    public OptionModel(int _statemaxnum, int[] _maxnum)
    {
        StateMaxNum = _statemaxnum;
        MaxNum = _maxnum;
    }

    // �I���{�^���̐؂�ւ�
    private void SelectNumbar(Vector2 value)
    {
        // DelayTime���I���܂ŏ����ɓ���Ȃ��悤�ɂ���
        if (!isSelect) { return; }

        isSelect = false;
        Observable.Timer(TimeSpan.FromSeconds(DelayTime))
            .Subscribe(_ => isSelect = true);

        var num = selectNum.y;
        if (value.y > 0) { num--; }
        else if (value.y < 0) { num++; }
        // �ő�l�𒴂��Ȃ��悤�␳
        selectNum.y = Math.Clamp(num, MinNum, MaxNum[(int)selectNum.x]);
        selectValue.Value = selectNum;
    }
    // �I���X�e�[�g�̐؂�ւ�
    private void StateNumbar(Vector2 value)
    {
        // DelayTime���I���܂ŏ����ɓ���Ȃ��悤�ɂ���
        if (!isSelect || selectNum.y != 0) { return; }
        isSelect = false;
        Observable.Timer(TimeSpan.FromSeconds(DelayTime))
            .Subscribe(_ => isSelect = true);
        var num = selectNum;

        if (value.x > 0){ num.x++; }
        else if (value.x < 0) {  num.x--; }
        // �ő�l�𒴂��Ȃ��悤�␳
        selectNum.x = Math.Clamp(num.x, 0, StateMaxNum);
        selectValue.Value = selectNum;
    }
    /// <summary> ���͂ɂ��؂�ւ�</summary>
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

    // ������
    public void InitializationNum()
    {
        selectNum = Vector2.zero;
        selectValue.Value = selectNum;
    }
}
