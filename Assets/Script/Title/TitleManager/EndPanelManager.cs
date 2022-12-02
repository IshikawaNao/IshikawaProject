using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPanelManager : MonoBehaviour
{
    public void EndWindow(TitleManager tm , ITitleSelect title, Animator anim, float value, bool input, bool decision, bool returnButton)
    {
        if (tm.num == 2) { tm.num = 3; }
        tm.isDelay = false;

        // アニメーションが再生されている間この処理に入らないようにする
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "TitleStart" && input)
        {
            //　終了パネル表示時選択番号取得
            tm.num = title.QuitNum(value);
        }

        if (decision)
        {
            // 終了処理
            title.QuitDecision(tm.num, anim);
        }

        // 戻る決定時
        if (returnButton)
        {
            anim.SetTrigger("PanelEnd");
        }
    }
}
