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

    // �I��ԍ�
    int selectionNumbar = 0;
    public int SelectionNumbar { get { return selectionNumbar; } }

    // Fill�T�C�Y
    private const float EnterFillValue = 1f;
    private const float ExitFillValue = 0;
    // ���B����
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

    // �X�e�[�g�ɕύX���������ꍇ�V���ȃA�j���[�V�������J�n����
    public void EnterUIAnimation(int _imageNum)
    {
        fillImage[_imageNum].DOFillAmount(EnterFillValue, ArrivalTime)
            .SetEase(Ease.OutCubic)
            .Play();

        selectionNumbar = _imageNum;
    }
    // �X�e�[�g�ɕύX���������ꍇ�O��Fill�����ɖ߂�
    public void ExitUIAnimation(int _imageNum)
    {
        fillImage[_imageNum].DOFillAmount(ExitFillValue, ArrivalTime)
            .SetEase(Ease.OutCubic)
            .Play();
    }

    // �L�����̃A�j���[�V����
    public void EnabldUIAnimation()
    {
        canvasGroup.DOFade(1, FadeArrivalTime)
            .SetEase(Ease.OutBack);
    }

    // �������̃A�j���[�V����
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
