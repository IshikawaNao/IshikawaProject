using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary> �T�E���h�ύX</summary>
public class VolumeConfigUI : MonoBehaviour
{
    const float volAddition = 0.005f;
    const float deadZone = 0.3f;

    KeyInput input;

    [SerializeField, Header("�I�v�V�����p�l��")]
    GameObject optionPanel;
    [SerializeField,Header("�T�E���h�p�l��")] 
    GameObject volumePanel;
    [SerializeField, Header("�I�v�V����")]
    OptionPresenter option;
    [SerializeField,Header("master�X���C�_�[")] 
    Slider masterSlider;
    [SerializeField, Header("BGM�X���C�_�[")] 
    Slider bgmSlider;
    [SerializeField, Header("SE�X���C�_�[")] 
    Slider seSlider;

    Vector2 inputValue;                 // ����C���v�b�g�̌���


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

    private void AudioPanelControll()        // �T�E���h�̒���
    {
        if(!volumePanel.activeSelf || !optionPanel.activeSelf)
        {
            return;
        }
        // keybord����L�X�e�B�b�N����
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

    private void AudioVolumeChange(Vector2 soundMenuNum, float volume)      // ���ʂ̕ύX
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

    // �X���C�_�[�̈ʒu���{�����[���ɍ��킹�ăZ�b�g
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

    // �X���C�_�[�ɕύX����������l�𔽉f������(�C�x���g)
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
