using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
   public void OptionWindow(bool returnButton, Animator anim, TitleManager tm)
   {
        tm.isDelay = false;
        // �߂錈�莞
        if (returnButton)
        {
            anim.SetTrigger("PanelEnd");
        }
    }
}
