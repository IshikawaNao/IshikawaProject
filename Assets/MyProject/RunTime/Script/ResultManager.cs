using UnityEngine;
using TMPro;

public class ResultManager : MonoBehaviour
{
    float clearTime = 0;
    string rank = "";

    [SerializeField]
    TextMeshProUGUI crearTimeText;

    CreateData cd;

    KeyInput input;
    StageNumberSelect sn;

    void Start()
    {
        cd = CreateData.Instance;
        sn = StageNumberSelect.Instance;
        input = KeyInput.Instance;
        cd.LoadClearData(ref clearTime, ref rank,sn.StageNumber);
        
    }

    void Update()
    {
        crearTimeText.text = clearTime.ToString();

        if(input.DecisionInput)
        {
            FadeManager.Instance.LoadScene("Title", 1.0f);
        }
    }
}
