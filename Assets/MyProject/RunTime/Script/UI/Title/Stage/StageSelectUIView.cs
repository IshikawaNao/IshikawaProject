using Cysharp.Threading.Tasks;
using DG.Tweening;
using SoundSystem;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectUIView : MonoBehaviour, IUIView
{
    [SerializeField, Header("RectTransform")]
    RectTransform rectTransform;
    [SerializeField, Header("�X�e�[�W�p�l��")]
    GameObject[] stagePanel;
    [SerializeField, Header("Stage�{�^��")]
    Image[] StageButton;
    [SerializeField, Header("�N���A�^�C��")]
    TextMeshProUGUI clearTimeText;
    [SerializeField, Header("�N���A�����N")]
    TextMeshProUGUI clearRankText;
    [SerializeField, Header("�N���A�C���[�W")]
    GameObject rankCircle;

    StageSelectUIPresenter prersenter;
    SaveDataManager save;

    // Fill�T�C�Y
    private const float EnterFillValue = 1f;
    private const float ExitFillValue = 0;
    // ���B����
    private const float ArrivalTime = 0.25f;
    private const float MoveArrivalTime = 0.5f;
    // �e�L�X�g
    private const string ClearTime = "ClearTime:";
    private const string EmptyTime = "--";

    enum Stage
    {
        Tutorial,
        Stage1,
    }

    void Start()
    {
        prersenter = new StageSelectUIPresenter(this, stagePanel.Length - 1);
        // �X�e�[�W�I����������
        StageNumberSelect.Instance.SelectStage((int)Stage.Tutorial);
        save = SaveDataManager.Instance;
        DisplayClearTime(save.ClearTime1, save.Rank1);
        EnterUIAnimation(0);
    }

    private void Update()
    {
        prersenter.HandleInput();
    }

    private void OnEnable()
    {
        EnabldUIAnimation();
    }

    // �X�e�[�g�ɕύX���������ꍇ�V���ȃA�j���[�V�������J�n����
    public void EnterUIAnimation(int _stageNum)
    {
        switch (_stageNum)
        {
            case 0:
                stagePanel[0].SetActive(true);
                stagePanel[1].SetActive(false);
                DisplayClearTime(save.ClearTime1,save.Rank1);
               
                break;
            case 1:
                stagePanel[0].SetActive(false);
                stagePanel[1].SetActive(true);
                DisplayClearTime(save.ClearTime2, save.Rank2);
                break;
        }
        // �A�j���[�V����
        StageButton[_stageNum].DOFillAmount(EnterFillValue, ArrivalTime)
                          .SetEase(Ease.OutCubic)
                          .Play();
        // �I������Ă���X�e�[�W
        StageNumberSelect.Instance.SelectStage(_stageNum);
    }

    // �X�e�[�W�N���A�󋵂̕\��
    private void DisplayClearTime(float time, string rank)
    {
        if(time <= 0)
        {
            clearTimeText.text = ClearTime + EmptyTime;
            rankCircle.SetActive(false);
        }
        else
        {
            clearTimeText.text = ClearTime + time.ToString();
            rankCircle.SetActive(true);
            clearRankText.text = rank;
        }
    }

    // �X�e�[�g�ɕύX���������ꍇ�O��Fill�����ɖ߂�
    public void ExitUIAnimation(int _stageNum)
    {
        StageButton[_stageNum].DOFillAmount(ExitFillValue, ArrivalTime)
                             .SetEase(Ease.OutCubic)
                             .Play();
        stagePanel[0].SetActive(true);
        stagePanel[1].SetActive(false);
    }

    public void EnabldUIAnimation()
    {
        stagePanel[0].SetActive(true);
        stagePanel[1].SetActive(false);
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
                stagePanel[0].SetActive(false);
                stagePanel[1].SetActive(false);
                this.gameObject.SetActive(false);
            });
        await UniTask.Delay(TimeSpan.FromSeconds(MoveArrivalTime));
    }
}
