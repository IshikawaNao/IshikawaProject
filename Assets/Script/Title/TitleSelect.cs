using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleSelect : MonoBehaviour, ITitleSelect
{
    int select = 0;
    int quit = 3;

    // �I������
    public void UISelect(int num)
    {
        switch(num)
        {
            case 0:
                print("�X�^�[�g");
                break;
            case 1:
                print("�I�v�V����");
                break;
            case 2:
                print("�I��");
                break;
        }
    }

    //�I��ԍ�
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

    // ���菈��
    public void SelecDecision(int num, GameObject[] ui)
    {
        switch (num)
        {
            case 0:
                SceneManager.LoadScene("Main");
                print("�V�[���J��");
                break;
            case 1:
                ui[0].SetActive(true);
                break;
            case 2:
                ui[1].SetActive(true);
                break;
        }
    }

    // �I���I��
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

    // �I���I�����菈��
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
