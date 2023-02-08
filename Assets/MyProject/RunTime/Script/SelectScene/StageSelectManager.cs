using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using SoundSystem;

public class StageSelectManager : MonoBehaviour
{
    // ステージナンバー
    int stageNum = 0;
    const int minStageNum = 0;
    const int maxStageNum = 1;

    const float moveVal = 15;
    const float waiteTime = 2;
    
    

    [SerializeField]
    GameObject stageOpen;

    [SerializeField]
    Material[] stage;

    [SerializeField]
    GameObject startButton;

    KeyInput input;

    [SerializeField]
    Animator anim;

    [SerializeField]
    OptionUIManager optionUIManager;

    [SerializeField]
    TitleManager title;

    ButtonMove bm;
    UiAddition ua;


    void Start()
    {
        input = KeyInput.Instance;
        bm = new ButtonMove();
        ua = new UiAddition();
        stage[0].SetFloat("_Boolean", 1);
        stage[1].SetFloat("_Boolean", 0);
    }

    
    void Update()
    {
        if(stageOpen.activeSelf == true)
        {
            startButton.SetActive(true);
            // key入力orLスティック操作時
            if (input.PressedMove && bm.SelectDelyTime())
            {
                stage[0].SetFloat("_Boolean", 0);
                stage[1].SetFloat("_Boolean", 0);

                // 選択番号取得
                stageNum = ua.Addition(stageNum, minStageNum,
                    maxStageNum, -input.InputMove.x);

                bm.SelectUIMove(stageOpen, moveVal,  input.InputMove.x, stage, stageNum);
                print(stageNum);
            }

            //　決定処理
            if (input.DecisionInput && bm.SelectDelyTime())
            {
                SoundManager.Instance.PlayOneShotSe("decision");
                StageNumberSelect.Instance.StageNumber = stageNum;
                FadeManager.Instance.LoadScene("Main", 1.0f);
            }

            if (input.EscInput)
            {
                anim.SetBool("PanelEnd", true);
                optionUIManager.IsOptionOpen = true;
                optionUIManager.EndPanel = true;
            }
        }
        else
        {
            startButton.SetActive(false);
        }
        
    }
}
