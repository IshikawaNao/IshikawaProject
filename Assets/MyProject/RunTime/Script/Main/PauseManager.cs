using UnityEngine.UI;
using UnityEngine;
using SoundSystem;

public class PauseManager : MonoBehaviour
{

    // 選択数字
    int num = 0;
    const int maxNum = 2;
    const int minNum = 0;

    [SerializeField]
    GameObject pausePanel;

    [SerializeField]
    GameObject pauseButton;

    [SerializeField]
    GameObject optionPanel;

    [SerializeField]
    Image[] selectButton;

    [SerializeField]
    OptionUIManager om;
    [SerializeField]
    TutorialStageDisplay tutorial;

    ButtonMove bm;
    UiAddition ua;

    KeyInput input;  

    void Start()
    {
        bm = new ButtonMove();
        ua = new UiAddition();
        pausePanel.SetActive(false);
        optionPanel.SetActive(false);
        input = KeyInput.Instance;
        selectButton[0].color = Color.white;
        selectButton[1].color = Color.blue;
        selectButton[2].color = Color.blue;
    }


    void Update()
    {
        UISelect();
    }

   

    void UISelect()
    {
        if (pausePanel.activeSelf == true)
        {
            // key入力orLスティック操作時
            if(input.PressedMove && bm.SelectDelyTime() && om.IsOptionOpen)
            {
                num = ua.Addition(num, minNum, maxNum, input.InputMove.y);
                bm.SelectTextMove(selectButton, num, maxNum);
            }
            else if (input.LongPressedMove && bm.SelectDelyTime() && om.IsOptionOpen)
            {
                num = ua.Addition(num,minNum,maxNum,input.InputMove.y);
                bm.SelectTextMove(selectButton,num, maxNum);
            }

            //　決定処理
            if (input.DecisionInput && om.IsOptionOpen)
            {
                DecisionPush();
            }

            if (input.EscInput && om.IsOptionOpen)
            {
                if (optionPanel.activeSelf == true)
                {
                    optionPanel.SetActive(false);
                    pauseButton.SetActive(true);
                }
                else
                {
                    pausePanel.SetActive(false);
                }
            }
        }
        else
        {
            if (input.EscInput && !tutorial.IsTutoria)
            {
                pausePanel.SetActive(true);
            }
        }
    }

    void DecisionPush()
    {
        SoundManager.Instance.PlayOneShotSe("decision");
        switch (num)
        {
            case 0:
                optionPanel.SetActive(true);
                pauseButton.SetActive(false);
                selectButton[0].color = Color.white;
                selectButton[1].color = Color.clear;
                selectButton[2].color = Color.clear;
                break;
            case 1:
                FadeManager.Instance.LoadScene("Main", 1.5f);
                break;
            case 2:
                FadeManager.Instance.LoadScene("Title", 1.5f);
                break;
        }
    }
}
