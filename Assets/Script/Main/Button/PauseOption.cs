using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseOption : MonoBehaviour
{
    bool flag;

    TextMeshProUGUI text;
    PauseManager pm;
    ISelectText iSelect;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        pm = GameObject.Find("StageManager").GetComponent<PauseManager>();
        iSelect = new SelectText();
    }

    void Update()
    {
        if (pm.ButtonNum == 0)
        {
            flag = true;
            iSelect.SelectTextMove(text, flag);
        }
        else if (pm.ButtonNum != 0)
        {
            flag = false;
            iSelect.SelectTextMove(text, flag);
        }
    }

    public void OnEnter()
    {
        pm.ButtonNum = 0;
    }
}
