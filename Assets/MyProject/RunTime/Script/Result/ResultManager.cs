using UnityEngine;
using TMPro;
using SoundSystem;

public class ResultManager : MonoBehaviour
{
    float clearTime = 0;
    bool toTitle = false;

    [SerializeField]
    TextMeshProUGUI crearTimeText;
    [SerializeField]
    TextMeshProUGUI crearRankText;

    [SerializeField]
    ClearRankData clearRankData;

    KeyInput input;
    StageNumberSelect sn;

    private void Awake()
    {
        sn = StageNumberSelect.Instance;
        SaveDataManager.Instance.Load();
        switch(sn.StageNumber)
        {
            case 0:
                clearTime = SaveDataManager.Instance.ClearTime1;
                SaveDataManager.Instance.Rank1Save(clearRankData.Ranks[0].ClearRank(clearTime));
                break;
            case 1:
                clearTime = SaveDataManager.Instance.ClearTime2;
                SaveDataManager.Instance.Rank2Save(clearRankData.Ranks[1].ClearRank(clearTime));
                break;

        }
        
        GameObject obj = (GameObject)Resources.Load("ResultStage");
        Instantiate(obj, Vector3.zero, Quaternion.identity);
    }

    void Start()
    {
        input = KeyInput.Instance;
        crearRankText.text = clearRankData.Ranks[sn.StageNumber].ClearRank(clearTime);
    }

    void Update()
    {
        crearTimeText.text = clearTime.ToString();

        if(input.DecisionInput && !toTitle)
        {
            toTitle = true;
            FadeManager.Instance.LoadScene("Title", 1f);
        }
    }
}
