using Cysharp.Threading.Tasks;
using DG.Tweening;
using SoundSystem;
using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionView : MonoBehaviour, IUIView
{
    [SerializeField, Header("RectTransform")]
    RectTransform rectTransform;
    [SerializeField, Header("CanvasGroup")]
    CanvasGroup canvasGroup;
    [SerializeField, Header("スクリーンマネージャー")]
    ScreenSizeSet screenSize;
    [SerializeField, Header("ビューパネル")]
    GameObject[] viewPanel;
    [SerializeField, Header("Aoudioボタン")]
    Image[] AoudioButton;
    [SerializeField, Header("Systemボタン")]
    Image[] SystemButton;


    Vector2 optionSelectButton;
    public Vector2 OptionSelectButton { get { return optionSelectButton; } }

    // Fillサイズ
    private const float EnterFillValue = 1f;
    private const float ExitFillValue = 0;
    // 到達時間
    private const float ArrivalTime = 0.25f;
    private const float MoveArrivalTime = 0.5f;

    OptionPresenter presenter;

    void Start()
    {
        int[] i = new int[] { AoudioButton.Length - 1, SystemButton.Length - 1 };
        presenter = new OptionPresenter(this, viewPanel.Length - 1, i, screenSize);
        EnterUIAnimation(Vector2.zero);
    }

    void Update()
    {
        presenter.HandleInput();
    }

    private void OnEnable()
    {
        EnabldUIAnimation();
    }

    // ステートに変更があった場合新たなアニメーションを開始する
    public void EnterUIAnimation(Vector2 _imageNum)
    {
        optionSelectButton = _imageNum;
        switch (_imageNum.x)
        {
            case 0:
                viewPanel[0].SetActive(true);
                viewPanel[1].SetActive(false);
                AoudioButton[(int)_imageNum.y].DOFillAmount(EnterFillValue, ArrivalTime)
                            .SetEase(Ease.OutCubic)
                            .Play();
                break;
            case 1:
                viewPanel[0].SetActive(false);
                viewPanel[1].SetActive(true);
                SystemButton[(int)_imageNum.y].DOFillAmount(EnterFillValue, ArrivalTime)
                            .SetEase(Ease.OutCubic)
                            .Play();
                break;
        }
        SoundManager.Instance.PlayOneShotSe((int)SEList.Select);
    }
    // ステートに変更があった場合前のFillを元に戻す
    public void ExitUIAnimation(Vector2 _imageNum)
    {
        switch (_imageNum.x)
        {
            case 0:
                AoudioButton[(int)_imageNum.y].DOFillAmount(ExitFillValue, ArrivalTime)
                            .SetEase(Ease.OutCubic)
                            .Play();
                break;
            case 1:
                SystemButton[(int)_imageNum.y].DOFillAmount(ExitFillValue, ArrivalTime)
                            .SetEase(Ease.OutCubic)
                            .Play();
                break;
        }
    }

    public void EnabldUIAnimation()
    {
        viewPanel[0].SetActive(true);
        viewPanel[1].SetActive(false);
        rectTransform.DOScale(1, MoveArrivalTime)
            .SetEase(Ease.OutBack);
    }

    // 無効時のアニメーション
    public async UniTask DisableAnimation()
    {
        rectTransform.DOScale(0, MoveArrivalTime)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
                {
                    viewPanel[0].SetActive(false);
                    viewPanel[1].SetActive(false);
                    this.gameObject.SetActive(false);
                    presenter.Initialization();
                });
        await UniTask.Delay(TimeSpan.FromSeconds(MoveArrivalTime));
    }
}
