using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public int ButtonNum { get; set; } = 0;

    [SerializeField]
    GameObject pausePanel;

    [SerializeField]
    GameObject pauseButton;

    [SerializeField]
    GameObject optionPanel;

    Animator anim;

    KeyInput input;

    IPauseSelect select;
    

    void Start()
    {
        select = new PauseSelect();
        input = GameObject.Find("KeyInput").GetComponent<KeyInput>();
        pausePanel.SetActive(false);
        optionPanel.SetActive(false);
    }


    void Update()
    {
        if (pausePanel.activeSelf == true)
        {
            //����
            Vector2 selectvalue = input.InputMove;

            // key����orL�X�e�B�b�N���쎞
            if (input.PressedMove)
            {
                // �I��ԍ��擾
                ButtonNum = select.SutageNum(selectvalue.y, ButtonNum);
            }

            //�@���菈��
            if (input.DecisionInput)
            {
                select.StageDecision(ButtonNum, anim, optionPanel, pauseButton);
            }

            if (input.EscInput)
            {
                if(optionPanel.activeSelf == true)
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
