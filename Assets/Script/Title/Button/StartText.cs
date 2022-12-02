using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartText : MonoBehaviour
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
        if(titleManager.num == 0)
        {
            flag = true;
            iSelect.SelectTextMove(text, flag);
        }
        else if(titleManager.num != 0)
        {
            flag = false;
            iSelect.SelectTextMove(text, flag);
        }
    }

    public void OnEnter()
    {
        titleManager.num = 0;
    }
}