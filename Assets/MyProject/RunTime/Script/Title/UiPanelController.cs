using UnityEngine;

public class UiPanelController 
{
    public void PanelSwitching(Animator anim, float num)
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
                break;
            case 2:
                anim.SetTrigger("Start");
                anim.SetInteger("PanelInt", 3);
                break;
        }
        
    }
}
