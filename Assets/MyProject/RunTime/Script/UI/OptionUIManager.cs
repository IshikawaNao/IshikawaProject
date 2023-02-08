using UnityEngine.UI;
using UnityEngine;
using SoundSystem;

public class OptionUIManager : MonoBehaviour
{
    // ボタン選択数字
    int num = 0;
    const int maxNum = 2;
    const int minNum = 0;

    public bool IsPanelSelect { get; private set; } = true;
    public bool IsOptionOpen { get; set; } = true;
    public bool EndPanel { get; set; } = true;
    public bool IsAudioOpen { get; private set; } = true;
    public bool IsSystemOpen { get; private set; } = true;

    // ボタンが押せるようになったか
    bool SelectDown
    {
        get
        {
            if (IsPanelSelect && bm.SelectDelyTime())
            {
                return true;
            }
            return false;
        }
    }

    float clearTime1 = 0;
    float clearTime2 = 0;
    string clearRank1 = "";
    string clearRank2 = "";
    bool isFullScreen;

    ButtonMove bm;
    UiAddition ua;
    KeyInput input;
    CreateData cd;

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

    [SerializeField]
    Slider masterSlider;
    [SerializeField]
    Slider bgmSllider;
    [SerializeField]
    Slider seSllider;
    [SerializeField]
    Slider cameraSlider;
    [SerializeField]
    Toggle screenToggle;

    void Start()
    {
        bm = new ButtonMove();
        ua = new UiAddition();
        input = KeyInput.Instance;

        cd = CreateData.Instance;

        cd.LoadClearData(ref clearTime1, ref clearRank1, 0);
        cd.LoadClearData(ref clearTime2, ref clearRank2, 1);

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
        if (input.PressedMove && SelectDown)
        {
            num = ua.Addition(num, minNum, maxNum, input.InputMove.y);
            bm.SelectTextMove(button, num, maxNum);
            PanelChenge();
        }
        // 長押し
        else if (input.LongPressedMove && SelectDown)
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
            SoundManager.Instance.PlayOneShotSe("decision");
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
                Save();
            }
        }
    }

    // 値をセーブする
    public void Save()
    {
        float vm = masterSlider.value,
            vb = bgmSllider.value,
            vs = seSllider.value,
            sn = cameraSlider.value,
            ct1 = clearTime1,
            ct2 = clearTime2;
        string cr1 = clearRank1,
            cr2 = clearRank2;
        bool fs = screenToggle.isOn;
       
        cd.Save(vm, vb, vs, sn, ct1, ct2, cr1, cr2, fs);
        Invoke(nameof(Dylay), 0.2f);
        panelCover.color = new Color(.5f, 0, 0, 1);
    }

    // 選択時のディレイ
    void Dylay()
    {
        IsPanelSelect = true;
    }
}
