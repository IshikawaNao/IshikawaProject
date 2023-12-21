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
    [SerializeField, Header("ステージパネル")]
    GameObject[] stagePanel;
    [SerializeField, Header("Stageボタン")]
    Image[] StageButton;
    [SerializeField, Header("クリアタイム")]
    TextMeshProUGUI clearTimeText;
    [SerializeField, Header("クリアランク")]
    TextMeshProUGUI clearRankText;
    [SerializeField, Header("クリアイメージ")]
    GameObject rankCircle;

    StageSelectUIPresenter prersenter;
    SaveDataManager save;

    // Fillサイズ
    private const float EnterFillValue = 1f;
    private const float ExitFillValue = 0;
    // 到達時間
    private const float ArrivalTime = 0.25f;
    private const float MoveArrivalTime = 0.5f;
    // テキスト
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
        // ステージ選択を初期化
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

    // ステートに変更があった場合新たなアニメーションを開始する
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
        // アニメーション
        StageButton[_stageNum].DOFillAmount(EnterFillValue, ArrivalTime)
                          .SetEase(Ease.OutCubic)
                          .Play();
        // 選択されているステージ
        StageNumberSelect.Instance.SelectStage(_stageNum);
    }

    // ステージクリア状況の表示
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

    // ステートに変更があった場合前のFillを元に戻す
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

    // 無効時のアニメーション
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
