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
        if (input > 0)
        {
            select--;
            if (select < 0) { select = 0; }
        

        }
        else if (input < 0)
        {
            select++;
            if (select > 2) { select = 2; }
        }
        return select;
    }

    // 決定処理
    public void SelecDecision(int num, GameObject[] ui)
    {
        switch (num)
        {
            case 0:
                SceneManager.LoadScene("Main");
                print("シーン遷移");
                break;
            case 1:
                ui[0].SetActive(true);
                break;
            case 2:
                ui[1].SetActive(true);
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
    public void QuitDecision(int num, GameObject[] ui)
    {
        switch(num)
        {
            case 3:
                ui[1].SetActive(false);
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
