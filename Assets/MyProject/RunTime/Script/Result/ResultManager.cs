using UnityEngine;
using TMPro;

public class ResultManager : MonoBehaviour
{
    float clearTime = 0;
    string rank = "";
    bool toTitle = false;

    [SerializeField]
    TextMeshProUGUI crearTimeText;
    [SerializeField]
    TextMeshProUGUI crearRankText;

    CreateData cd;
    [SerializeField]
    ClearRankData clearRankData;

    KeyInput input;
    StageNumberSelect sn;

    private void Awake()
    {
        cd = CreateData.Instance;
        sn = StageNumberSelect.Instance;

        cd.LoadClearData(ref clearTime, ref rank, sn.StageNumber);
        GameObject obj = (GameObject)Resources.Load("ResultStage");
        Instantiate(obj, Vector3.zero, Quaternion.identity);
    }

    void Start()
    {
        input = KeyInput.Instance;
        cd.VolSet();
        crearRankText.text = clearRankData.Ranks[sn.StageNumber].ClearRank(clearTime);
    }

    void Update()
    {
        crearTimeText.text = clearTime.ToString();

        if(input.DecisionInput && !toTitle)
        {
            toTitle = true;
            FadeManager.Instance.LoadScene("Title", 1.0f);
        }
    }
}
