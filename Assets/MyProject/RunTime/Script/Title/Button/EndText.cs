using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndText : MonoBehaviour
{
    bool flag;

    TextMeshProUGUI text;
    TitleManager titleManager;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        titleManager = GameObject.Find("TitleManager").GetComponent<TitleManager>();
        
    }

    //void Update()
    //{
    //    if (titleManager.num == 2)
    //    {
    //        flag = true;
    //        iSelect.SelectTextMove(text, flag);
    //    }
    //    else if (titleManager.num != 2)
    //    {
    //        flag = false;
    //        iSelect.SelectTextMove(text, flag);
    //    }
    //}

    //public void OnEnter()
    //{
    //    titleManager.num = 2;
    //}
}
