using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TitleView 
{
    Sequence tween = null;
    // スケールサイズ
    private const float ScaleNum = 0.8f;
    // 到達時間
    private const float ArrivalTime = 0.1f;
    // アニメーションが再開できるか
    private bool isPlay = false;

    Image img;

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
                 .Insert(0,img.DOColor(Color.black, ArrivalTime))
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
    public void UIExit()
    {
        img.color = Color.white;
        tween.Restart();
        tween.Pause();
        isPlay = true;
    }
    public void UISelect(Animation anim)
    {
       
    }
}
