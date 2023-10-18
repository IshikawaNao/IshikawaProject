using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using SoundSystem;

public class StageSelectManager : MonoBehaviour
{
    // �X�e�[�W�i���o�[
    int stageNum = 0;
    const int minStageNum = 0;
    const int maxStageNum = 1;

    const float moveVal = 15;
    const float waiteTime = 2;
    bool IsSceneChange = false;
    
    [SerializeField]
    GameObject stageOpen;

    [SerializeField]
    Material[] stage;


    KeyInput input;

    [SerializeField]
    Animator anim;


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
        IsSceneChange = false;
    }

    
    void Update()
    {
        if(stageOpen.activeSelf == true)
        {
            // key����orL�X�e�B�b�N���쎞
            if (input.PressedMove && bm.SelectDelyTime())
            {
                stage[0].SetFloat("_Boolean", 0);
                stage[1].SetFloat("_Boolean", 0);

                // �I��ԍ��擾
                stageNum = ua.Addition(stageNum, minStageNum,
                    maxStageNum, -input.InputMove.x);

                bm.SelectUIMove(stageOpen, moveVal,  input.InputMove.x, stage, stageNum);

            }

            //�@���菈��
            if (input.DecisionInput && bm.SelectDelyTime() && !IsSceneChange)
            {
                SoundManager.Instance.PlayOneShotSe("decision");
                StageNumberSelect.Instance.StageNumber = stageNum;
                FadeManager.Instance.LoadScene("Main", waiteTime);
                IsSceneChange = true;
            }
        }
    }
}
