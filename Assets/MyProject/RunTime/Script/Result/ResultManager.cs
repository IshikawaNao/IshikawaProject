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
        GameObject obj = (GameObject)Resources.Load("ResultStage");
        Instantiate(obj, Vector3.zero, Quaternion.identity);
    }

    void Start()
    {
        input = KeyInput.Instance;
        sn = StageNumberSelect.Instance;
        var save = SaveDataManager.Instance;
        switch (sn.StageNumber)
        {
            case 0:
                clearTime = save.ClearTime1;
                save.Rank1Save(clearRankData.Ranks[0].ClearRank(clearTime));
                break;
            case 1:
                clearTime = save.ClearTime2;
                save.Rank2Save(clearRankData.Ranks[1].ClearRank(clearTime));
                break;

        }
        save.Save();
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
