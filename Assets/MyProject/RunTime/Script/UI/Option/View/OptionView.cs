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
    [SerializeField, Header("�X�N���[���}�l�[�W���[")]
    ScreenSizeSet screenSize;
    [SerializeField, Header("�r���[�p�l��")]
    GameObject[] viewPanel;
    [SerializeField, Header("Aoudio�{�^��")]
    Image[] AoudioButton;
    [SerializeField, Header("System�{�^��")]
    Image[] SystemButton;


    Vector2 optionSelectButton;
    public Vector2 OptionSelectButton { get { return optionSelectButton; } }

    // Fill�T�C�Y
    private const float EnterFillValue = 1f;
    private const float ExitFillValue = 0;
    // ���B����
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

    // �X�e�[�g�ɕύX���������ꍇ�V���ȃA�j���[�V�������J�n����
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
    // �X�e�[�g�ɕύX���������ꍇ�O��Fill�����ɖ߂�
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

    // �������̃A�j���[�V����
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
