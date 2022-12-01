using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class VolumeConfigUI : MonoBehaviour
{
    // �I��ԍ�
    int soundMenuNum = 0;
    // �I���f�B���C�t���O
    bool isDelay = true;
    [SerializeField] TextMeshProUGUI masterText;
    [SerializeField] TextMeshProUGUI bgmText;
    [SerializeField] TextMeshProUGUI seText;
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

    }

    private void Update()
    {
        inputValue = myInput.Player.Move.ReadValue<Vector2>();
        AudioPanelControll();
    }

    private void AudioPanelControll()        // �T�E���h�̒���
    {
        // keybord����L�X�e�B�b�N����
        if (myInput.Player.Move.WasPressedThisFrame())
        {
            if (inputValue.y > 0.3f && soundMenuNum > 0) { soundMenuNum--; }
            else if (inputValue.y < -0.3f && soundMenuNum < 2) { soundMenuNum++; }
            else if (inputValue.x > 0.3f) { AudioVolumeChange(soundMenuNum, 0.005f); }
            else if (inputValue.x < -0.3f) { AudioVolumeChange(soundMenuNum, -0.005f); }

            isDelay = false;
            //���̏����܂ł̃f�B���C
            Invoke("Delay", 0.2f);
        }
        else if (myInput.Player.Move.IsPressed())
        {
            if (inputValue.x > 0.3f) { AudioVolumeChange(soundMenuNum, 0.005f); }
            else if (inputValue.x < -0.3f) { AudioVolumeChange(soundMenuNum, -0.005f); }

            if (isDelay)
            {
                if (inputValue.y > 0.3f && soundMenuNum > 0) { soundMenuNum--; }
                else if (inputValue.y < -0.3f && soundMenuNum < 2) { soundMenuNum++; }

                isDelay = false;
            }
        }

        switch (soundMenuNum)
        {
            case 0:
                masterText.color = new Color(255, 255, 0, 255);
                bgmText.color = new Color(255, 255, 255, 255);
                seText.color = new Color(255, 255, 255, 255);
                break;
            case 1:
                masterText.color = new Color(255, 255, 255, 255);
                bgmText.color = new Color(255, 255, 0, 255);
                seText.color = new Color(255, 255, 255, 255);
                break;
            case 2:
                masterText.color = new Color(255, 255, 255, 255);
                bgmText.color = new Color(255, 255, 255, 255);
                seText.color = new Color(255, 255, 0, 255);
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

    // �I���f�B���C
    void Delay()
    {
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
