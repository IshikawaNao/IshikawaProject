using UnityEngine;

public class UiPanelController 
{
    public void PanelSwitching(TitleManager tm, Animator anim, float num)
    {
        switch (num)
        {
                case 0:
                anim.SetTrigger("Start");
                anim.SetInteger("PanelInt", 1);
                
                break;
                case 1:
                anim.SetTrigger("Start");
                anim.SetInteger("PanelInt", 2);
                tm.isDelay = false;
                break;
            case 2:
                anim.SetTrigger("Start");
                anim.SetInteger("PanelInt", 3);
                break;
        }
        
    }
}
