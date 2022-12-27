using UnityEngine.UI;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    // 選択数字
    int num = 0;
    const int maxNum = 2;
    const int minNum = 0;

    const int inputZero = 0;

    [SerializeField]
    GameObject pausePanel;

    [SerializeField]
    GameObject pauseButton;

    [SerializeField]
    GameObject optionPanel;

    [SerializeField]
    Image[] selectButton;

    ButtonMove bm;
    UiAddition ua;

    Animator anim;

    [SerializeField,Header("入力")]
    KeyInput input;

    IPauseSelect select;
    

    void Start()
    {
        select = new PauseSelect();
        bm = new ButtonMove();
        ua = new UiAddition();
        pausePanel.SetActive(false);
        optionPanel.SetActive(false);
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
            if (input.PressedMove && bm.SelectDelyTime())
            {
                num = ua.Addition(num,minNum,maxNum,input.InputMove.y);
                bm.SelectTextMove(selectButton,num, maxNum);
            }

            //　決定処理
            if (input.DecisionInput)
            {
                select.StageDecision(num, anim, optionPanel, pauseButton);
            }

            if (input.EscInput)
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
            if (input.EscInput)
            {
                pausePanel.SetActive(true);
            }
        }
    }

    
}
