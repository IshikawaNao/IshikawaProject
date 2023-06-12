using System;
using DG.Tweening;

/// <summary>
/// �I���m�FUIModel
/// </summary>
public class TutorialUModel
{
    // �I������Ă���X�e�[�g�̐��l
    private int num = 0;
    public int Num { get => num; }
    private const int MinNum = 0;
    // �I�����̃f�B���C�̂��߂̃t���O
    private bool isSelect = true;
    public bool IsSelect { get => isSelect; }
    // �f�B���C�^�C��
    private const float DelayTime = 0.2f;

    /// <summary> ���͂ɂ��X�e�[�g�̐؂�ւ�</summary>
    public void SelectNum(float value , int MaxNum)
    {
        if (!isSelect)
        {
            return;
        }
        isSelect = false;
        // DelayTime�҂��ăf�B���C����
        DOVirtual.DelayedCall(DelayTime, () => isSelect = true);

        if (value > 0)
        {
            num++;
        }
        else if (value < 0)
        {
            num--;
        }

        // �ő�l�𒴂��Ȃ��悤�␳
        num = Math.Clamp(num, MinNum, MaxNum);
    }
}
