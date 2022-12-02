using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionText : MonoBehaviour
{
    bool flag;

    TextMeshProUGUI text;
    TitleManager titleManager;
    ISelectText iSelect;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        titleManager = GameObject.Find("TitleManager").GetComponent<TitleManager>();
        iSelect = new SelectText();
    }

    void Update()
    {
        if (titleManager.num == 1)
        {
            flag = true;
            iSelect.SelectTextMove(text, flag);
        }
        else if (titleManager.num != 1)
        {
            flag = false;
            iSelect.SelectTextMove(text, flag);
        }
    }

    public void OnEnter()
    {
        titleManager.num = 1;
    }
}
