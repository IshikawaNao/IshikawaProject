using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary> サウンド変更</summary>
public class VolumeConfigUI : MonoBehaviour
{
    const float volAddition = 0.005f;
    const float deadZone = 0.3f;

    KeyInput input;

    [SerializeField, Header("オプションパネル")]
    GameObject optionPanel;
    [SerializeField,Header("サウンドパネル")] 
    GameObject volumePanel;
    [SerializeField, Header("オプション")]
    OptionPresenter option;
    [SerializeField,Header("masterスライダー")] 
    Slider masterSlider;
    [SerializeField, Header("BGMスライダー")] 
    Slider bgmSlider;
    [SerializeField, Header("SEスライダー")] 
    Slider seSlider;

    Vector2 inputValue;                 // 操作インプットの向き


    private void Start()
    {
        input = KeyInput.Instance;
        SaveDataManager.Instance.Load();
        masterSlider.value = SaveDataManager.Instance.MasterVol;
        bgmSlider.value = SaveDataManager.Instance.BGMVol;
        seSlider.value = SaveDataManager.Instance.SEVol;
    }

    private void Update()
    {
        inputValue = input.InputMove;
        AudioPanelControll();
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
            if (inputValue.x > deadZone) { AudioVolumeChange(option.SelectNum, volAddition); }
            else if (inputValue.x < -deadZone) { AudioVolumeChange(option.SelectNum, -volAddition); }
        }
        else if (input.LongPressedMove)
        {
            if (inputValue.x > deadZone) { AudioVolumeChange(option.SelectNum, volAddition); }
            else if (inputValue.x < -deadZone) { AudioVolumeChange(option.SelectNum, -volAddition); }
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
            case 1:
                masterSlider.value += volume;
                SaveDataManager.Instance.MasterVolSave(masterSlider.value);
                break;
            case 2:
                bgmSlider.value += volume;
                SaveDataManager.Instance.BGMVolSave(bgmSlider.value);
                break;
            case 3:
                seSlider.value += volume;
                SaveDataManager.Instance.SEVolSave(seSlider.value);
                break;
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
