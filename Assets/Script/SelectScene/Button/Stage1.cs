using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stage1 : MonoBehaviour
{
    bool flag;

    TextMeshProUGUI text;
    StageSelectManager stage;
    ISelectText iSelect;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        stage = GameObject.Find("StageSelectManager").GetComponent<StageSelectManager>();
        iSelect = new SelectText();
    }


    void Update()
    {
        if (stage.sutageNum == 0)
        {
            flag = true;
            iSelect.SelectTextMove(text, flag);
        }
        else if (stage.sutageNum != 0)
        {
            flag = false;
            iSelect.SelectTextMove(text, flag);
        }
    }

    public void OnEnter()
    {
        stage.sutageNum = 0;
    }
}
