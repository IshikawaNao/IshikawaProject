using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour, ISelectManager
{
    int select;

    // 選択処理
    public void StageDecision(int num, Animator anim)
    {
        switch (num)
        {
            case 0:
                SceneManager.LoadScene("Main");
                print("シーン遷移");
                break;
            case 1:
                anim.SetTrigger("Start");
                anim.SetInteger("PanelInt", 1);
                break;
            case 2:
                anim.SetTrigger("Start");
                anim.SetInteger("PanelInt", 2);
                break;
        }
    }

    // ステージナンバー
    public int SutageNum(float input, int num)
    {
        select = num;
        if (input > 0) { select--; }
        else if (input < 0) { select++; }
        return select;
    }
}
