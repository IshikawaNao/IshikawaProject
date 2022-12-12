using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SoundSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class TitleManager : MonoBehaviour
{
    // �I�𐔎�
    public int num { get; set; } = 0;
    
    // �I���f�B���C�̂��߂̃t���O
    public bool isDelay { get; set; }

    // �I���C���[�W
    [SerializeField]
    Animator anim; 

    // UI�p�l��
    [SerializeField]
    GameObject[] uiPanel;

    TitleButton titleButton;
    OptionManager optionManager;
    EndPanelManager endPanelManager;

    [SerializeField] VolumeConfigUI volumeConfigUI;

    ITitleSelect title;

    #region�@InputAction
    MyInput myInput;
    void Awake() => myInput = new MyInput();
    void OnEnable() => myInput.Enable();
    void OnDisable() => myInput.Disable();
    void OnDestroy() => myInput.Dispose();
    #endregion

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

        title = new TitleSelect();
        titleButton = new TitleButton();    
        optionManager = new OptionManager();
        endPanelManager = new EndPanelManager();
        num = 0;

        SoundManager.Instance.PlayBGMWithFadeIn("Title", 1f);
    }

    void Update()
    {
         print(myInput.Camera.Scroll.ReadValue<Vector2>());
        //����
        Vector2 selectvalue = myInput.Player.Move.ReadValue<Vector2>();

        // �I���p�l���\����
        if (uiPanel[1].activeSelf == true)
        {
            endPanelManager.EndWindow(this, title, anim, selectvalue.x, 
                myInput.Player.Move.WasPressedThisFrame(), myInput.UI.Decision.WasPressedThisFrame(), myInput.UI.Return.WasPressedThisFrame());
        }
        // option�p�l���\����
        else if (uiPanel[0].activeSelf == true)
        {
            optionManager.OptionWindow(myInput.UI.Return.WasPressedThisFrame(), anim, this);
        }
        else
        {
            titleButton.ButtonSelect(this, title, anim, selectvalue.y, 
                myInput.Player.Move.WasPressedThisFrame(), myInput.Player.Move.IsPressed(), myInput.UI.Decision.WasPressedThisFrame());
        }     
    }
}
