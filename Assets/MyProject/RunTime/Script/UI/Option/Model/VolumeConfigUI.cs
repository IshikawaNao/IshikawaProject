using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary> サウンド変更</summary>
public class VolumeConfigUI : MonoBehaviour
{
    

    [SerializeField, Header("オプションパネル")]
    GameObject optionPanel;
    [SerializeField,Header("サウンドパネル")] 
    GameObject volumePanel;
    [SerializeField, Header("システムパネル")]
    GameObject systemPanel;
    [SerializeField, Header("オプション")]
    OptionView option;
    [SerializeField,Header("masterスライダー")] 
    Slider masterSlider;
    [SerializeField, Header("BGMスライダー")] 
    Slider bgmSlider;
    [SerializeField, Header("SEスライダー")] 
    Slider seSlider;
    [SerializeField, Header("カメラ感度スライダー")]
    Slider sensitivitySlider;

    KeyInput input;

    Vector2 inputValue;                 // 操作インプットの向き
    const float VolAddition = 0.005f;
    const float DeadZone = 0.3f;

    // サウンド
    enum SoundVolue 
    {
        Master = 1,
        Bgm, 
        Se,
    }

    private void Start()
    {
        input = KeyInput.Instance;
        SaveDataManager.Instance.Load();
        masterSlider.value = SaveDataManager.Instance.MasterVol;
        bgmSlider.value = SaveDataManager.Instance.BGMVol;
        seSlider.value = SaveDataManager.Instance.SEVol;
        sensitivitySlider.value = SaveDataManager.Instance.Sensitivity;
    }

    private void Update()
    {
        inputValue = input.InputMove;
        AudioPanelControll();
        CameraSensitivity();
    }

    private void AudioPanelControll()        // サウンドの調整
    {
        if(!volumePanel.activeSelf || !optionPanel.activeSelf)
        {
            return;
        }
        // keybord入力Lスティック操作
        if (input.PressedMove)
        {
            if (inputValue.x > DeadZone) { AudioVolumeChange(option.OptionSelectButton, VolAddition); }
            else if (inputValue.x < -DeadZone) { AudioVolumeChange(option.OptionSelectButton, -VolAddition); }
        }
        else if (input.LongPressedMove)
        {
            if (inputValue.x > DeadZone) { AudioVolumeChange(option.OptionSelectButton, VolAddition); }
            else if (inputValue.x < -DeadZone) { AudioVolumeChange(option.OptionSelectButton, -VolAddition); }
        }
    }

    private void CameraSensitivity()
    {
        if (!systemPanel.activeSelf || !optionPanel.activeSelf)
        {
            return;
        }
        // keybord入力Lスティック操作
        if (input.PressedMove)
        {
            if (inputValue.x > DeadZone) { CameraSensitivityChange(option.OptionSelectButton, VolAddition); }
            else if (inputValue.x < -DeadZone) { CameraSensitivityChange(option.OptionSelectButton, -VolAddition); }
        }
        else if (input.LongPressedMove)
        {
            if (inputValue.x > DeadZone) { CameraSensitivityChange(option.OptionSelectButton, VolAddition); }
            else if (inputValue.x < -DeadZone) { CameraSensitivityChange(option.OptionSelectButton, -VolAddition); }
        }
    }

    private void AudioVolumeChange(Vector2 soundMenuNum, float volume)      // 音量の変更
    {
        if (soundMenuNum.x != 0)
        {
            return;
        }

        switch (soundMenuNum.y)
        {
            case (int)SoundVolue.Master:
                masterSlider.value += volume;
                SaveDataManager.Instance.MasterVolSave(masterSlider.value);
                break;
            case (int)SoundVolue.Bgm:
                bgmSlider.value += volume;
                SaveDataManager.Instance.BGMVolSave(bgmSlider.value);
                break;
            case (int)SoundVolue.Se:
                seSlider.value += volume;
                SaveDataManager.Instance.SEVolSave(seSlider.value);
                break;
        }
    }

    private void CameraSensitivityChange(Vector2 soundMenuNum, float volume)
    {
        if (soundMenuNum.x != 1)
        {
            return;
        }
        if(soundMenuNum.y == 2)
        {
            sensitivitySlider.value += volume;
            SaveDataManager.Instance.SensitivitySave(sensitivitySlider.value);
        }
    }

    // スライダーの位置をボリュームに合わせてセット
    public void SetMasterVolume(float masterVolume)
    {
        masterSlider.value = masterVolume;
    }
    public void SetBGMVolume(float bgmVolume)
    {
        bgmSlider.value = bgmVolume;
    }
    public void SetSeVolume(float seVolume)
    {
        seSlider.value = seVolume;
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
