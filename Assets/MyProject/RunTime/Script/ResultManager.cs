using UnityEngine;
using TMPro;

public class ResultManager : MonoBehaviour
{
    float clearTime = 0;
    string rank = "";
    bool toTitle = false;

    [SerializeField]
    TextMeshProUGUI crearTimeText;

    CreateData cd;

    KeyInput input;
    StageNumberSelect sn;

    private void Awake()
    {
        cd = CreateData.Instance;
        cd.LoadClearData(ref clearTime, ref rank, sn.StageNumber);
        GameObject obj = (GameObject)Resources.Load("ResultStage");
        Instantiate(obj, Vector3.zero, Quaternion.identity);
    }

    void Start()
    {
        sn = StageNumberSelect.Instance;
        input = KeyInput.Instance;
        cd.VolSet();
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
