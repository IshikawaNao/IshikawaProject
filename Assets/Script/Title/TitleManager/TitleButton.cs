using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton : MonoBehaviour
{
    float delayTime;

    public void ButtonSelect(TitleManager tm, ITitleSelect title, Animator anim, float value, bool input, bool inputPuressed,bool decision)
    {
        //　決定処理
        if (decision)
        {
            title.SelecDecision(tm.num, anim);
        }

        // key入力orLスティック操作時
        if (input)
        {
            // 選択番号取得
            tm.num = title.SelectNum(value, tm.num);

            // 選択処理
            title.UISelect(tm.num);
            tm.isDelay = false;
        }
        // 長押し
        else if (inputPuressed)
        {
            //次の処理までのディレイ
            Delay(tm);

            if (tm.isDelay)
            {
                // 選択番号取得
                tm.num = title.SelectNum(value, tm.num);

                // 選択処理
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

    // 選択ディレイ
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
