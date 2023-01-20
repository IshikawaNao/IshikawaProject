using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class VolumeConfigUI : MonoBehaviour
{
    // 選択番号
    int soundMenuNum = 0;
    const int minNum = 0;
    const int maxNum = 3;

    const float volAddition = 0.005f;
    const float deadZone = 0.3f;

    float volMaster = 0f, volBgm = 0f, volSe = 0f;

    // 選択ディレイフラグ
    bool isDelay = true;

    ButtonMove bm;
    UiAddition ua;
    KeyInput input;
    
    [Header("セーブ")] 
    CreateData cd;
    [SerializeField] 
    Animator anim;
    [SerializeField] 
    TextMeshProUGUI masterText;
    [SerializeField] 
    TextMeshProUGUI bgmText;
    [SerializeField] 
    TextMeshProUGUI seText;
    [SerializeField] 
    Image saveButton;
    [SerializeField] 
    GameObject volumePanel;
    [SerializeField] 
    Slider masterSlider;
    [SerializeField] 
    Slider bgmSlider;
    [SerializeField] 
    Slider seSlider;
    [SerializeField] 
    OptionUIManager optionManager;
    [SerializeField]
    Image[] sliderButton;

    Vector2 inputValue;                 // 操作インプットの向き


    private void Start()
    {
        bm = new ButtonMove();
        ua = new UiAddition();
        input = KeyInput.Instance;
        cd = CreateData.Instance;
        cd.LoadVol(ref volMaster, ref volBgm, ref volSe);
        masterSlider.value = volMaster;
        bgmSlider.value = volBgm;
        seSlider.value = volSe;
        sliderButton[0].color = Color.white;
        sliderButton[1].color = Color.blue;
        sliderButton[2].color = Color.blue;
    }

    private void Update()
    {
        inputValue = input.InputMove;
        AudioPanelControll();
        SaveButton();
    }

    private void AudioPanelControll()        // サウンドの調整
    {
        // keybord入力Lスティック操作
        if (input.PressedMove && bm.SelectDelyTime() && !optionManager.IsPanelSelect && optionManager.IsAudioOpen)
        {
            soundMenuNum = ua.Addition(soundMenuNum, minNum, maxNum, input.InputMove.y);
            if (inputValue.x > deadZone) { AudioVolumeChange(soundMenuNum, volAddition); }
            else if (inputValue.x < -deadZone) { AudioVolumeChange(soundMenuNum, -volAddition); }
            bm.SelectTextMove(sliderButton, soundMenuNum, maxNum);
            isDelay = false;
        }
        else if (input.LongPressedMove && bm.SelectDelyTime() && !optionManager.IsPanelSelect && optionManager.IsAudioOpen)
        {
            if (inputValue.x > deadZone) { AudioVolumeChange(soundMenuNum, volAddition); }
            else if (inputValue.x < -deadZone) { AudioVolumeChange(soundMenuNum, -volAddition); }

            if (isDelay)
            {
                if (inputValue.y > deadZone) { soundMenuNum--; }
                else if (inputValue.y < -deadZone) { soundMenuNum++; }

                isDelay = false;
            }
        }
        else if(optionManager.IsPanelSelect)
        {
            sliderButton[0].color = Color.white;
            sliderButton[1].color = Color.blue;
            sliderButton[2].color = Color.blue;
        }
    }

    private void AudioVolumeChange(int soundMenuNum, float volume)      // 音量の変更
    {
        switch (soundMenuNum)
        {
            case 0:
                masterSlider.value += volume;
                break;
            case 1:
                bgmSlider.value += volume;
                break;
            case 2:
                seSlider.value += volume;
                break;
        }
    }

    void SaveButton()
    {
        //　決定処理
        if (input.DecisionInput && maxNum == soundMenuNum)
        {
            optionManager.Save();
        }
    }

    // スライダーの位置をボリュームに合わせてセット
    public void SetMasterVolume(float masterVolume)
    {
        masterSlider.value = masterVolume;
    }
    public void SetBGMVolume(float bgmVolume)
    {
        masterSlider.value = bgmVolume;

    }
    public void SetSeVolume(float seVolume)
    {
        masterSlider.value = seVolume;

    }

    // スライダーに変更があったら値を反映させる(イベント)
    public void SetMasterSliderEvent(UnityAction<float> sliderCallback)
    {
        SetVolumeChagedEvent(masterSlider, sliderCallback);
    }
    public void SetBGMSliderEvent(UnityAction<float> sliderCallback)
    {
        SetVolumeChagedEvent(bgmSlider, sliderCallback);
    }
    public void SetSeSliderEvent(UnityAction<float> sliderCallback)
    {
        SetVolumeChagedEvent(seSlider, sliderCallback);
    }
    void SetVolumeChagedEvent(Slider slider, UnityAction<float> sliderCallback)
    {
        if (slider.onValueChanged != null)
        {
            slider.onValueChanged.RemoveAllListeners();
        }
        slider.onValueChanged.AddListener(sliderCallback);
    }
}
