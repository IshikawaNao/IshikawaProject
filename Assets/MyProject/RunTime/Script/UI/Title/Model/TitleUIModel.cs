using UnityEngine;
using DG.Tweening;
using System;

public class TitleUIModel 
{
    // �I������Ă���X�e�[�g�̐��l
    private int num = 0;
    public int Num { get { return num; } }
    private const int MinNum = 0;
    private  int MaxNum;
    // �I�����̃f�B���C�̂��߂̃t���O
    private bool isSelect = true;
    public bool IsSelect { get => isSelect; }
    // �f�B���C�^�C��
    private const float DelayTime = 0.4f;

    public TitleUIModel (int _MaxNum)
    {
        MaxNum = _MaxNum;
    }

    /// <summary> ���͂ɂ��X�e�[�g�̐؂�ւ�</summary>
    public int SelectNum(Vector2 value)
    {
        if (!isSelect)
        {
            return num;
        }
        isSelect = false;
        // DelayTime�҂��ăf�B���C����
        DOVirtual.DelayedCall(DelayTime, () => isSelect = true);

        if (value.y > 0)
        {
            num--;
        }
        else if (value.y < 0)
        {
            num++;
        }

        // �ő�l�𒴂��Ȃ��悤�␳
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
