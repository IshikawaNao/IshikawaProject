using DG.Tweening;
using UnityEngine;
using System;
using Unity.VisualScripting.FullSerializer;

public class EndUIModel 
{
    // �I������Ă���X�e�[�g�̐��l
    private int num = 0;
    public int Num { get { return num; } }
    private const int MinNum = 0;
    private int MaxNum = 1;
    // �I�����̃f�B���C�̂��߂̃t���O
    private bool isSelect = true;
    public bool IsSelect { get => isSelect; }
    // �f�B���C�^�C��
    private const float DelayTime = 0.4f;

    /// <summary> �I���{�^���I��/// </summary>
    public void EndSelectNum(Vector2 value)
    {
        if (!isSelect)
        {
            return;
        }
        isSelect = false;
        // DelayTime�҂��ăf�B���C����
        DOVirtual.DelayedCall(DelayTime, () => isSelect = true);

        if (value.x > 0)
        {
            num--;
        }
        else if (value.x < 0)
        {
            num++;
        }

        // �ő�l�𒴂��Ȃ��悤�␳
        num = Math.Clamp(num, MinNum, MaxNum);
    }

    /// <summary> ���艟���� </summary>
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
