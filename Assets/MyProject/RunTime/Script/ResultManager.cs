using UnityEngine;
using TMPro;

public class ResultManager : MonoBehaviour
{
    float crearTime;
    string rank = "S";

    [SerializeField]
    TextMeshProUGUI crearTimeText;

    [SerializeField]
    CreateData cd;

    [SerializeField]
    KeyInput input;

    SaveData save;

    void Start()
    {
        save = new SaveData();
        save = cd.loadData();
        crearTime = save.ClearTime[StageNumberSelect.Instance.StageNumber];
    }

    void Update()
    {
        crearTimeText.text = crearTime.ToString();

        if(input.DecisionInput)
        {
            FadeManager.Instance.LoadScene("Title", 1.0f);
        }
    }
}
