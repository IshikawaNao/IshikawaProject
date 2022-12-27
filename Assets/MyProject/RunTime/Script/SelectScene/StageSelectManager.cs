using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    // ステージナンバー
    int stageNum = 0;
    const int minStageNum = 0;
    const int maxStageNum = 2;

    [SerializeField]
    StageNumberSelect stageNumberSelect;

    [SerializeField]
    GameObject stageOpen;

    [SerializeField]
    Image[] stage;

    [SerializeField, Header("入力")]
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
            // key入力orLスティック操作時
            if (input.PressedMove)
            {
                // 選択番号取得
                stageNum = ua.Addition(stageNum, minStageNum,
                    maxStageNum, input.InputMove.y);

                bm.SelectTextMove(stage, stageNum, maxStageNum);
            }

            //　決定処理
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
