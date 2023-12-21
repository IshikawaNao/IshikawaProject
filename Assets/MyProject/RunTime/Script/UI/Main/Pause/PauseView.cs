using UniRx;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;

public class PauseView
{
    CompositeDisposable disposables = new CompositeDisposable();
    Sequence tween = null;
    CanvasGroup canvasGroup;

    bool isPlay = false;
    Image img;

    // 到達時間
    private const float ScaleNum = 0.8f;
    private const float ArrivalTime = 0.1f;
    private const float FadeArrivalTime = 0.5f;

    public PauseView(CanvasGroup _canvasGroup)
    {
        canvasGroup = _canvasGroup;
    }

    public void UIMove(Image Button)
    {
        img = Button;
        if (tween == null)
        {
            // アニメーションの初期化
            CreateTween();
            return;
        }

        if (isPlay)
        {
            isPlay = false;
            // アニメーションの再初期化
            CreateTween();
            tween.Play();
        }
    }

    public void UIExit()
    {
        // アニメーションの終了処理
        img.color = Color.white;
        tween.Restart();
        tween.Pause();
        isPlay = true;
    }

    private void CreateTween()
    {
        // DOTweenを使用したアニメーションの設定
        tween = DOTween.Sequence()
            .Append(img.transform.DOScale(new Vector3(ScaleNum, ScaleNum, ScaleNum), ArrivalTime))
            .SetLoops(2, LoopType.Yoyo)
            .Insert(0, img.DOColor(Color.black, ArrivalTime))
            .OnComplete(() => img.color = Color.red);
    }

    public void Dispose()
    {
        // 使用しているUniRxのDisposableの解放
        disposables.Dispose();
    }

    // 有効時のアニメーション
    public void EnabldUIAnimation()
    {
        canvasGroup.DOFade(1, FadeArrivalTime)
            .SetEase(Ease.OutBack);
    }

    // 無効時のアニメーション
    public async UniTask DisableAnimation(GameObject obj)
    {
        canvasGroup.DOFade(0, FadeArrivalTime)
            .SetEase(Ease.OutBack)
            .OnComplete(() => obj.SetActive(false));
        await UniTask.Delay(TimeSpan.FromSeconds(FadeArrivalTime));
    }
}