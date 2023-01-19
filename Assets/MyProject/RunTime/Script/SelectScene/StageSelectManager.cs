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

    const float moveVal = 700;
    const float waiteTime = 2;
    
    

    [SerializeField]
    GameObject stageOpen;

    [SerializeField]
    Image[] stage;

    [SerializeField]
    GameObject startButton;

    KeyInput input;

    [SerializeField]
    Animator anim;

    ButtonMove bm;
    UiAddition ua;


    void Start()
    {
        input = KeyInput.Instance;
        bm = new ButtonMove();
        ua = new UiAddition();
    }

    
    void Update()
    {
        if(stageOpen.activeSelf == true)
        {
            startButton.SetActive(true);
            // key����orL�X�e�B�b�N���쎞
            if (input.PressedMove && bm.SelectDelyTime())
            {
                // �I��ԍ��擾
                stageNum = ua.Addition(stageNum, minStageNum,
                    maxStageNum, input.InputMove.y);

                bm.SelectUIMove(stageOpen, moveVal,  input.InputMove.y);
            }

            //�@���菈��
            if (input.DecisionInput && bm.SelectDelyTime())
            {
                StageNumberSelect.Instance.StageNumber = stageNum;
                SoundManager.Instance.StopBGMWithFadeOut("Title", 1);
                FadeManager.Instance.LoadScene("Main", 1.0f);
            }

            if (input.EscInput)
            {
                anim.SetBool("PanelEnd", true);
            }
        }
        else
        {
            startButton.SetActive(false);
        }
        
    }
}
