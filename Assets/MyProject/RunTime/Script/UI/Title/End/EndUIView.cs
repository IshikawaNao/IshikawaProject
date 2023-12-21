using Cysharp.Threading.Tasks;
using DG.Tweening;
using SoundSystem;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EndUIView : MonoBehaviour, IUIView
{
    [SerializeField, Header("Fill")]
    Image[] fillImage;
    [SerializeField, Header("CanvasGroup")]
    CanvasGroup canvasGroup;

    EndUIPresenter presenter;

    // 選択番号
    int selectionNumbar = 0;
    public int SelectionNumbar { get { return selectionNumbar; } }

    // Fillサイズ
    private const float EnterFillValue = 1f;
    private const float ExitFillValue = 0;
    // 到達時間
    private const float ArrivalTime = 0.25f;
    private const float FadeArrivalTime = 0.5f;

    private void Start()
    {
        presenter = new EndUIPresenter(this, fillImage.Length);
        EnterUIAnimation(0);
    }

    private void Update()
    {
        presenter.HandleInput();
    }

    // ステートに変更があった場合新たなアニメーションを開始する
    public void EnterUIAnimation(int _imageNum)
    {
        fillImage[_imageNum].DOFillAmount(EnterFillValue, ArrivalTime)
            .SetEase(Ease.OutCubic)
            .Play();

        selectionNumbar = _imageNum;
    }
    // ステートに変更があった場合前のFillを元に戻す
    public void ExitUIAnimation(int _imageNum)
    {
        fillImage[_imageNum].DOFillAmount(ExitFillValue, ArrivalTime)
            .SetEase(Ease.OutCubic)
            .Play();
    }

    // 有効時のアニメーション
    public void EnabldUIAnimation()
    {
        canvasGroup.DOFade(1, FadeArrivalTime)
            .SetEase(Ease.OutBack);
    }

    // 無効時のアニメーション
    public async UniTask DisableAnimation()
    {
        canvasGroup.DOFade(0, FadeArrivalTime)
            .SetEase(Ease.OutBack)
            .OnComplete(() => this.gameObject.SetActive(false));
        await UniTask.Delay(TimeSpan.FromSeconds(FadeArrivalTime));
    }

    private void OnEnable()
    {
        EnabldUIAnimation();
    }
}
