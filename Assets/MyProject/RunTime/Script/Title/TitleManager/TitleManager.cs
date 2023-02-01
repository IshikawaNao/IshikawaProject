using UnityEngine;
using SoundSystem;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    // �I�𐔎�
    int num = 0;
    const int maxNum = 2;
    const int minNum = 0;

    const int fadeTime = 1;

    // �I���f�B���C�̂��߂̃t���O
    public bool isDelay { get; set; }
    public bool EndPanel { get; set; } = true;

    KeyInput input;

    [SerializeField, Header("Animator")]
    Animator anim;

    ButtonMove bm;
    UiAddition ua;

    [SerializeField]
    Image[] button;

    UiPanelController uiPanelController;

    [SerializeField]
    EndPanelManager endPanelManager;

    [SerializeField] 
    VolumeConfigUI volumeConfigUI;

    [SerializeField] 
    OptionUIManager optionUIManager;

    void Start()
    {
        // �X���C�_�[�̐��l���f
        volumeConfigUI.SetMasterVolume(SoundManager.Instance.MasterVolume);
        volumeConfigUI.SetBGMVolume(SoundManager.Instance.BGMVolume);
        volumeConfigUI.SetSeVolume(SoundManager.Instance.SEVolume);

        // �{�����[���̐ݒ�
        volumeConfigUI.SetMasterSliderEvent(vol => SoundManager.Instance.MasterVolume = vol);
        volumeConfigUI.SetBGMSliderEvent(vol => SoundManager.Instance.BGMVolume = vol);
        volumeConfigUI.SetSeSliderEvent(vol => SoundManager.Instance.SEVolume = vol);

        CreateData.Instance.VolSet();

        bm = new ButtonMove();
        ua = new UiAddition();
        uiPanelController = new UiPanelController();
        input = KeyInput.Instance;

        button[0].color = Color.white;
        button[1].color = Color.blue;
        button[2].color = Color.blue;

        SoundManager.Instance.PlayBGMWithFadeIn("Title", fadeTime);
    }

    void Update()
    {
        SelectNum();
        DecisionButton();
    }

    // �I�𐔎�
    void SelectNum()
    {
        if(input.PressedMove && (optionUIManager.IsOptionOpen || EndPanel) && bm.SelectDelyTime())
        {
            num = ua.Addition(num, minNum, maxNum, input.InputMove.y);
            bm.SelectTextMove(button, num, maxNum);
        }
        else if(input.LongPressedMove && (optionUIManager.IsOptionOpen || EndPanel) && bm.SelectDelyTime())
        {
            num = ua.Addition(num, minNum, maxNum, input.InputMove.y);
            bm.SelectTextMove(button, num, maxNum);
        }
    }

    // ����
    void DecisionButton()
    {
        if(input.DecisionInput && (optionUIManager.IsOptionOpen || EndPanel) && 
            !anim.GetCurrentAnimatorStateInfo(0).IsTag("Panel"))
        {
            uiPanelController.PanelSwitching(this, anim, num);
            optionUIManager.IsOptionOpen = false;
            EndPanel = false;
            SoundManager.Instance.PlayOneShotSe("decision");
            anim.SetBool("PanelEnd", false);
        }
    }
}
