using UnityEngine;
using DG.Tweening;
using System;

public class OptionModel
{ 
    private const int MinNum = 0;
    // �ő�l
    private  int[] MaxNum;
    private int StateMaxNum; 
    // �I�����̃f�B���C�̂��߂̃t���O
    private bool isSelect = true;
    public bool IsSelect { get => isSelect; }
    // X�X�e�[�g�ԍ��@Y�{�^���ԍ�
    Vector2 selectvalue;
    public Vector2 SelectValue { get { return selectvalue; } }

    // �f�B���C�^�C��
    private const float DelayTime = 0.4f;

    // �X�e�[�g�ƃ{�^���̍ő吔����
    public OptionModel(int _statemaxnum, int[] _maxnum)
    {
        StateMaxNum = _statemaxnum;
        MaxNum = _maxnum;
    }
    
    // �I���{�^���̐؂�ւ�
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
        // �ő�l�𒴂��Ȃ��悤�␳
        selectvalue.y = Math.Clamp(num, MinNum, MaxNum[(int)selectvalue.x]);
    }
    // �I���X�e�[�g�̐؂�ւ�
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
            // �ő�l�𒴂��Ȃ��悤�␳
            selectvalue.x = Math.Clamp(num.x, 0, StateMaxNum);
        }
    }
    /// <summary> ���͂ɂ��؂�ւ�</summary>
    public void SelectCahge(Vector2 value)
    {
        if (!isSelect)
        {
            return;
        }
        isSelect = false;
        // DelayTime�҂��ăf�B���C����
        DOVirtual.DelayedCall(DelayTime, () => isSelect = true);

        SelectNum(value);
        StateNum(value);
    }

    // ������
    public void ReturnOption()
    {
        selectvalue = Vector2.zero;
    }
}

