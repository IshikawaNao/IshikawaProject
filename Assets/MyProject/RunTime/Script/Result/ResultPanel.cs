using DG.Tweening;
using DG.Tweening.Core.Easing;
using TMPro;
using UnityEngine;

public class ResultPanel : MonoBehaviour
{
    [SerializeField, Header("�N���A�����N�f�[�^")]
    ClearRankData clearRankData;
    [SerializeField, Header("�N���A�^�C���e�L�X�g")]
    TextMeshProUGUI crearTimeText;
    [SerializeField, Header("�N���A�����N")]
    TextMeshProUGUI crearRankText;
    [SerializeField, Header("�ē��e�L�X�g")]
    TextMeshProUGUI guideText;

    public void ClearResult(float time)
    {
        var tect = this.GetComponent<RectTransform>();
        tect.DOScale(1f,.6f)
            .SetEase(Ease.OutBack, 5f);

        guideText.DOFade(0, 1)
             .SetLoops(-1, LoopType.Yoyo);

        switch (StageNumberSelect.Instance.StageNumber)
        {
            case 0:
                SaveDataManager.Instance.Rank1Save(clearRankData.Ranks[0].ClearRank(time));
                break;
            case 1:
                SaveDataManager.Instance.Rank2Save(clearRankData.Ranks[1].ClearRank(time));
                break;

        }
        SaveDataManager.Instance.Save();
        crearTimeText.text = Mathf.Floor(time).ToString();
        crearRankText.text = clearRankData.Ranks[StageNumberSelect.Instance.StageNumber].ClearRank(time);
    }
}
