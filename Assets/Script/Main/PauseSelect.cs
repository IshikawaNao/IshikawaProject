using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSelect : IPauseSelect
{
    int select;

    // 選択処理
    public void StageDecision(int num, Animator anim, GameObject option, GameObject Pause)
    {
        switch (num)
        {
            case 0:
                option.SetActive(true);
                Pause.SetActive(false);
                break;
            case 1:
                SceneManager.LoadScene("Main");
                break;
            case 2:
                SceneManager.LoadScene("Title");
                break;
        }
    }

    // ステージナンバー
    public int SutageNum(float input, int num)
    {
        select = num;
        if (input > 0) 
        {
            if(select == 0) { select = 0; }
            else { select--; } 
        }
        else if (input < 0) 
        {
            if (select == 2) { select = 2; }
            else { select++; }
        }
        return select;
    }
}
