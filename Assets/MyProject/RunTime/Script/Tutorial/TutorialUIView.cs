using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// ‘I‘ðŠm”FUIView
/// </summary>
public class TutorialUIView 
{
    public void PanelSelect(int num, List<Image> list)
    {
        var i = 0;
        foreach (Image img in list)
        {
            img.color = Color.white;

            if(num == i)
            {
                img.DOColor(new Color(1f, 0, 0), 0.2f);
            }
            i++;
        }
    }
}
