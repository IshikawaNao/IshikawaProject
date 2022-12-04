using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleSelect : MonoBehaviour, ITitleSelect
{
    int select = 0;
    int quit = 3;

    // 選択処理
    public void UISelect(int num)
    {
        switch(num)
        {
            case 0:
                print("スタート");
                break;
            case 1:
                print("オプション");
                
                break;
            case 2:
                print("終了");
                break;
        }
    }

    //選択番号
    public int SelectNum(float input, int num)
    {
        select = num;
        if (input > 0){ select--; }
        else if (input < 0){ select++; }
        return select;
    }

    // 決定処理
    public void SelecDecision(int num, Animator anim)
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

    // 終了選択
    public int QuitNum(float input)
    {
        if (input > 0)
        {
            quit--;
            if (quit < 3) { quit = 3; }
        }
        else if (input < 0)
        {
            quit++;
            if (quit > 4) { quit = 4; }
        }
        return quit;
    }

    // 終了選択決定処理
    public void QuitDecision(int num, Animator anim)
    {
        switch(num)
        {
            case 3:
                anim.SetTrigger("PanelEnd");
                break;
            case 4:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
                break;
        }
    }
}
