using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoText : MonoBehaviour
{
    bool flag;

    [SerializeField]
    Animator anim;

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
        if (titleManager.num == 3)
        {
            flag = true;
            iSelect.SelectTextMove(text, flag);
        }
        else if (titleManager.num != 3)
        {
            flag = false;
            iSelect.SelectTextMove(text, flag);
        }
    }

    public void OnEnter()
    {
        titleManager.num = 3;
    }

    public void OnClick()
    {
        titleManager.num = 2;
        print(titleManager.num);
        anim.SetTrigger("EndPanelEnd");
    }
}
