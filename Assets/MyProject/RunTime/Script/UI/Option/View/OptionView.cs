using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Data;

public class OptionView 
{
    Sequence tween = null;
    // �X�P�[���T�C�Y
    private const float ScaleNum = 0.3f;
    // ���B����
    private const float ArrivalTime = 0.1f;
    // �A�j���[�V�������ĊJ�ł��邩
    private bool isPlay = false;

    Image img;

    // �{�^���A�j���[�V����
    public void UIMove(Image Button)
    {
        img = Button;
        if (tween == null)
        {
            tween = DOTween.Sequence()
                .Append(img.transform.DOScale(
                 new Vector3(ScaleNum, ScaleNum, ScaleNum),
                 ArrivalTime
                 )).SetLoops(2, LoopType.Yoyo)
                 .Insert(0, img.DOColor(Color.black, ArrivalTime))
                 .OnComplete(() => img.color = Color.red);
            return;
        }
        if (isPlay)
        {
            isPlay = false;

            tween = DOTween.Sequence()
                .Append(img.transform.DOScale(
                 new Vector3(ScaleNum, ScaleNum, ScaleNum),
                 ArrivalTime
                 )).SetLoops(2, LoopType.Yoyo)
                 .Insert(0, img.DOColor(Color.black, ArrivalTime))
                 .OnComplete(() => img.color = Color.red);
            tween.Play();
        }
    }
    // �{�^�����I������ĂȂ����
    public void UIExit()
    {
        img.color = Color.white;
        tween.Restart();
        tween.Pause();
        isPlay = true;
    }

    public void ChangeState(int num,GameObject[] obj)
    {
        for (int i = 0; i < obj.Length; i++)
        {
            obj[i].SetActive(false);
            if (i == num)
            {
                obj[num].SetActive(true);
            }
        }
    }
}
