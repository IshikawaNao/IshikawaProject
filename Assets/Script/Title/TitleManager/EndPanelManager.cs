using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPanelManager : MonoBehaviour
{
    public void EndWindow(TitleManager tm , ITitleSelect title, Animator anim, float value, bool input, bool decision, bool returnButton)
    {
        if (tm.num == 2) { tm.num = 3; }
        tm.isDelay = false;

        // �A�j���[�V�������Đ�����Ă���Ԃ��̏����ɓ���Ȃ��悤�ɂ���
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "TitleStart" && input)
        {
            //�@�I���p�l���\�����I��ԍ��擾
            tm.num = title.QuitNum(value);
        }

        if (decision)
        {
            // �I������
            title.QuitDecision(tm.num, anim);
        }

        // �߂錈�莞
        if (returnButton)
        {
            anim.SetTrigger("PanelEnd");
        }
    }
}
