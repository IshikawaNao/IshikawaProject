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

    // ���B����
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
            // �A�j���[�V�����̏�����
            CreateTween();
            return;
        }

        if (isPlay)
        {
            isPlay = false;
            // �A�j���[�V�����̍ď�����
            CreateTween();
            tween.Play();
        }
    }

    public void UIExit()
    {
        // �A�j���[�V�����̏I������
        img.color = Color.white;
        tween.Restart();
        tween.Pause();
        isPlay = true;
    }

    private void CreateTween()
    {
        // DOTween���g�p�����A�j���[�V�����̐ݒ�
        tween = DOTween.Sequence()
            .Append(img.transform.DOScale(new Vector3(ScaleNum, ScaleNum, ScaleNum), ArrivalTime))
            .SetLoops(2, LoopType.Yoyo)
            .Insert(0, img.DOColor(Color.black, ArrivalTime))
            .OnComplete(() => img.color = Color.red);
    }

    public void Dispose()
    {
        // �g�p���Ă���UniRx��Disposable�̉��
        disposables.Dispose();
    }

    // �L�����̃A�j���[�V����
    public void EnabldUIAnimation()
    {
        canvasGroup.DOFade(1, FadeArrivalTime)
            .SetEase(Ease.OutBack);
    }

    // �������̃A�j���[�V����
    public async UniTask DisableAnimation(GameObject obj)
    {
        canvasGroup.DOFade(0, FadeArrivalTime)
            .SetEase(Ease.OutBack)
            .OnComplete(() => obj.SetActive(false));
        await UniTask.Delay(TimeSpan.FromSeconds(FadeArrivalTime));
    }
}