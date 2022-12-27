using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class VolumeConfigUI : MonoBehaviour
{
    // �I��ԍ�
    int soundMenuNum = 0;
    const int minNum = 0;
    const int maxNum = 3;

    const float volAddition = 0.005f;
    const float deadZone = 0.3f;

    public bool IsSelect { get; set; } = true;

    // �I���f�B���C�t���O
    bool isDelay = true;
    SaveData save;
    [SerializeField ,Header("�Z�[�u")] CreateData cd;
    [SerializeField] KeyInput input;
    [SerializeField] Animator anim;
    [SerializeField] TextMeshProUGUI masterText;
    [SerializeField] TextMeshProUGUI bgmText;
    [SerializeField] TextMeshProUGUI seText;
    [SerializeField] Image saveButton;
    [SerializeField] GameObject volumePanel;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;

    Vector2 inputValue;                 // ����C���v�b�g�̌���

    #region�@InputAction
    MyInput myInput;
    void Awake() => myInput = new MyInput();
    void OnEnable()
    {
        myInput.Enable();
        soundMenuNum = 0;
    }
    void OnDisable() => myInput.Disable();
    void OnDestroy() => myInput.Dispose();
    #endregion
    private void Start()
    {
        save = new SaveData();
        save = cd.loadData();
        masterSlider.value = save.masterVol;
        bgmSlider.value = save.BGMVol;
        seSlider.value = save.SEVol;
    }

    private void Update()
    {
        inputValue = input.InputMove;
        AudioPanelControll();
        Decision();
    }

    private void AudioPanelControll()        // �T�E���h�̒���
    {
        // keybord����L�X�e�B�b�N����
        if (input.PressedMove)
        {
            if (inputValue.y > deadZone) { soundMenuNum--; }
            else if (inputValue.y < -deadZone) { soundMenuNum++; }
            else if (inputValue.x > deadZone) { AudioVolumeChange(soundMenuNum, volAddition); }
            else if (inputValue.x < -deadZone) { AudioVolumeChange(soundMenuNum, -volAddition); }

            isDelay = false;
        }
        else if (input.LongPressedMove)
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

        switch (soundMenuNum)
        {
            case 0:
                masterText.color = Color.yellow;
                bgmText.color = Color.white;
                seText.color = Color.white;
                saveButton.color = Color.white;
                break;
            case 1:
                masterText.color = Color.white;
                bgmText.color = Color.yellow;
                seText.color = Color.white;
                saveButton.color = Color.white;
                break;
            case 2:
                masterText.color = Color.white;
                bgmText.color = Color.white;
                seText.color = Color.yellow;
                saveButton.color = Color.white;
                break;
            case 3:
                masterText.color = Color.white;
                bgmText.color = Color.white;
                seText.color = Color.white;
                saveButton.color = Color.yellow;
                break;
        }
    }

    private void AudioVolumeChange(int soundMenuNum, float volume)      // ���ʂ̕ύX
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

    void Decision()
    {
        //�@���菈��
        if (input.DecisionInput && maxNum == soundMenuNum)
        {
            save.masterVol = masterSlider.value;
            save.BGMVol = bgmSlider.value;
            save.SEVol = seSlider.value;
            cd.SaveData(save);
            anim.SetBool("PanelEnd", true);
            //���̏����܂ł̃f�B���C
            Invoke("Delay", 0.5f);
        }
    }

    // �I���f�B���C
    void Delay()
    {
        IsSelect = true;
        isDelay = true;
    }

    // �X���C�_�[�̈ʒu���{�����[���ɍ��킹�ăZ�b�g
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
