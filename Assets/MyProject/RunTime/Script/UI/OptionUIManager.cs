using UnityEngine.UI;
using UnityEngine;

public class OptionUIManager : MonoBehaviour
{
    // ボタン選択数字
    int num = 0;
    const int maxNum = 2;
    const int minNum = 0;

    public bool IsPanelSelect { get; set; } = true;
    public bool IsOptionOpen { get; set; } = true;
    public bool IsAudioOpen { get; set; } = true;
    public bool IsSystemOpen { get; set; } = true;

    ButtonMove bm;
    UiAddition ua;
    KeyInput input;

    [SerializeField]
    GameObject option;

    [SerializeField]
    public Image panelCover;
    [SerializeField]
    GameObject[] panels;
    [SerializeField]
    Image[] button;
    [SerializeField]
    Animator anim;
    [SerializeField] 
    VolumeConfigUI volumeConfigUI;

    void Start()
    {
        bm = new ButtonMove();
        ua = new UiAddition();
        input = KeyInput.Instance;

        button[0].color = Color.white;
        button[1].color = Color.blue;
        button[2].color = Color.blue;
    }

    void Update()
    {
        if (option.activeSelf)
        {
            SelectNum();
            Decision();
            ReturnButton();
        }
    }

    // 選択数字
    void SelectNum()
    {
        // Y入力
        if (input.PressedMove && IsPanelSelect && bm.SelectDelyTime())
        {
            num = ua.Addition(num, minNum, maxNum, input.InputMove.y);
            bm.SelectTextMove(button, num, maxNum);
            PanelChenge();
        }
        // 長押し
        else if (input.LongPressedMove && IsPanelSelect && bm.SelectDelyTime())
        {
            num = ua.Addition(num, minNum, maxNum, input.InputMove.y);
            bm.SelectTextMove(button, num, maxNum);
            PanelChenge();
        }
    }

    // パネル切り替え
    void PanelChenge()
    {
        if (num == 0)
        {
            panels[0].SetActive(true);
            panels[1].SetActive(false);
            IsSystemOpen = false;
        }
        else if (num == 1)
        {
            panels[0].SetActive(false);
            panels[1].SetActive(true);
            IsAudioOpen = false;
        }
        else
        {
            IsSystemOpen = false;
            IsAudioOpen = false;
        }
    }

    // 決定
    void Decision()
    {
        if (input.DecisionInput && IsPanelSelect)
        {
            switch (num)
            {
                case 0:
                    panelCover.color = Color.clear;
                    IsPanelSelect = false;
                    IsAudioOpen = true;
                    break;
                case 1:
                    panelCover.color = Color.clear;
                    IsPanelSelect = false;
                    IsSystemOpen = true;
                    break;
                case 2:
                    num = minNum;
                    bm.SelectTextMove(button, num, maxNum);
                    PanelChenge();
                    anim.SetBool("PanelEnd", true);
                    anim.ResetTrigger("Start");
                    IsOptionOpen = true;
                    break;
            }
            
        }
    }

    // 戻る
    void ReturnButton()
    {
        if (input.EscInput)
        {
            if(IsPanelSelect)
            {
                anim.SetBool("PanelEnd", true);
                num = minNum;
                IsOptionOpen = true;
            }
            else if(!IsPanelSelect)
            {
                IsPanelSelect = true;
                panelCover.color = new Color(.5f,0,0,1);
            }
        }
    }
}
