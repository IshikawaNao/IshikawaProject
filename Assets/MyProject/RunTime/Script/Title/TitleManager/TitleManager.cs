using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SoundSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class TitleManager : MonoBehaviour
{
    // 選択数字
    int num = 0;
    const int maxNum = 2;
    const int minNum = 0;

    const int fadeTime = 1;

    // 選択ディレイのためのフラグ
    public bool isDelay { get; set; }

    [SerializeField, Header("入力")]
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

    [SerializeField] VolumeConfigUI volumeConfigUI;


    #region　InputAction
    MyInput myInput;
    void Awake() => myInput = new MyInput();
    void OnEnable() => myInput.Enable();
    void OnDisable() => myInput.Disable();
    void OnDestroy() => myInput.Dispose();
    #endregion

    void Start()
    {
        // スライダーの数値反映
        volumeConfigUI.SetMasterVolume(SoundManager.Instance.MasterVolume);
        volumeConfigUI.SetBGMVolume(SoundManager.Instance.BGMVolume);
        volumeConfigUI.SetSeVolume(SoundManager.Instance.SEVolume);

        // ボリュームの設定
        volumeConfigUI.SetMasterSliderEvent(vol => SoundManager.Instance.MasterVolume = vol);
        volumeConfigUI.SetBGMSliderEvent(vol => SoundManager.Instance.BGMVolume = vol);
        volumeConfigUI.SetSeSliderEvent(vol => SoundManager.Instance.SEVolume = vol);


        bm = new ButtonMove();
        ua = new UiAddition();
        uiPanelController = new UiPanelController();

        button[0].color = Color.white;

        SoundManager.Instance.PlayBGMWithFadeIn("Title", fadeTime);
    }

    void Update()
    {
        SelectNum();
        Decision();
        Return();
    }

    void SelectNum()
    {
        if(input.PressedMove && (volumeConfigUI.IsSelect || endPanelManager.EndPanel) && bm.SelectDelyTime())
        {
            num = ua.Addition(num, minNum, maxNum, input.InputMove.y);
            bm.SelectTextMove(button, num, maxNum);
        }
        else if(input.LongPressedMove && (volumeConfigUI.IsSelect || endPanelManager.EndPanel) && bm.SelectDelyTime())
        {
            num = ua.Addition(num, minNum, maxNum, input.InputMove.y);
            bm.SelectTextMove(button, num, maxNum);
        }
    }

    void Decision()
    {
        if(input.DecisionInput && (volumeConfigUI.IsSelect || endPanelManager.EndPanel))
        {
            uiPanelController.PanelSwitching(this, anim, num);
            volumeConfigUI.IsSelect = false;
            endPanelManager.EndPanel = false;
        }
    }

    void Return()
    {
        if(input.EscInput)
        {
            volumeConfigUI.IsSelect = true;
            endPanelManager.EndPanel = true;
            anim.SetBool("PanelEnd",true);
        }
        else
        {
            anim.SetBool("PanelEnd", false);
        }
    }
}
