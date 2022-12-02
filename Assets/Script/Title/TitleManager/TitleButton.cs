using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton : MonoBehaviour
{
    float delayTime;

    public void ButtonSelect(TitleManager tm, ITitleSelect title, Animator anim, float value, bool input, bool inputPuressed,bool decision)
    {
        //�@���菈��
        if (decision)
        {
            title.SelecDecision(tm.num, anim);
        }

        // key����orL�X�e�B�b�N���쎞
        if (input)
        {
            // �I��ԍ��擾
            tm.num = title.SelectNum(value, tm.num);

            // �I������
            title.UISelect(tm.num);
            tm.isDelay = false;
        }
        // ������
        else if (inputPuressed)
        {
            //���̏����܂ł̃f�B���C
            Delay(tm);

            if (tm.isDelay)
            {
                // �I��ԍ��擾
                tm.num = title.SelectNum(value, tm.num);

                // �I������
                title.UISelect(tm.num);

                tm.isDelay = false;
            }
        }
        else
        {
            delayTime = 0;
        }

        if (tm.num < 0) { tm.num = 0; }
        else if (tm.num > 2) { tm.num = 2; }
    }

    // �I���f�B���C
    void Delay(TitleManager tm) 
    { 
        if(delayTime < 0.4f)
        {
            delayTime += Time.deltaTime;
        }
        else
        {
            tm.isDelay = true; 
        }
    }
}
