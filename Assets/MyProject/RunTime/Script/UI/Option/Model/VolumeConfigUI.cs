using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary> �T�E���h�ύX</summary>
public class VolumeConfigUI : MonoBehaviour
{
    

    [SerializeField, Header("�I�v�V�����p�l��")]
    GameObject optionPanel;
    [SerializeField,Header("�T�E���h�p�l��")] 
    GameObject volumePanel;
    [SerializeField, Header("�V�X�e���p�l��")]
    GameObject systemPanel;
    [SerializeField, Header("�I�v�V����")]
    OptionView option;
    [SerializeField,Header("master�X���C�_�[")] 
    Slider masterSlider;
    [SerializeField, Header("BGM�X���C�_�[")] 
    Slider bgmSlider;
    [SerializeField, Header("SE�X���C�_�[")] 
    Slider seSlider;
    [SerializeField, Header("�J�������x�X���C�_�[")]
    Slider sensitivitySlider;

    KeyInput input;

    Vector2 inputValue;                 // ����C���v�b�g�̌���
    const float VolAddition = 0.005f;
    const float DeadZone = 0.3f;

    // �T�E���h
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

    private void AudioPanelControll()        // �T�E���h�̒���
    {
        if(!volumePanel.activeSelf || !optionPanel.activeSelf)
        {
            return;
        }
        // keybord����L�X�e�B�b�N����
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
        // keybord����L�X�e�B�b�N����
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

    private void AudioVolumeChange(Vector2 soundMenuNum, float volume)      // ���ʂ̕ύX
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
