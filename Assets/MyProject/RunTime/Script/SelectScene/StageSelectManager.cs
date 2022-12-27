using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    // �X�e�[�W�i���o�[
    int stageNum = 0;
    const int minStageNum = 0;
    const int maxStageNum = 2;

    [SerializeField]
    StageNumberSelect stageNumberSelect;

    [SerializeField]
    GameObject stageOpen;

    [SerializeField]
    Image[] stage;

    [SerializeField, Header("����")]
    KeyInput input;

    [SerializeField]
    Animator anim;

    ButtonMove bm;
    UiAddition ua;


    void Start()
    {
        bm = new ButtonMove();
        ua = new UiAddition();
    }

    
    void Update()
    {
        if(stageOpen.activeSelf == true)
        {
            // key����orL�X�e�B�b�N���쎞
            if (input.PressedMove)
            {
                // �I��ԍ��擾
                stageNum = ua.Addition(stageNum, minStageNum,
                    maxStageNum, input.InputMove.y);

                bm.SelectTextMove(stage, stageNum, maxStageNum);
            }

            //�@���菈��
            if (input.DecisionInput)
            {
                stageNumberSelect.StageNumber = stageNum;
                SceneManager.LoadScene("Main");
            }

            if (input.EscInput)
            {
                anim.SetBool("PanelEnd", true);
            }
        }
        
    }
}
