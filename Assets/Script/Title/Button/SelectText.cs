using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class SelectText : ISelectText
{
    public void SelectTextMove(TextMeshProUGUI text, bool flag)
    {
        if (flag && text.alpha == 1)
        {
            text.DOFade(0f, 1.5f).SetLoops(-1,LoopType.Yoyo);
            text.color = new Color(1, 0, 0);
        }
        else if(!flag)
        {
            text.DOPause();
            text.alpha = 1;
            text.color = new Color(0, 0, 0);
        }
    }
}
