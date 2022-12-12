using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SoundSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class TitleManager : MonoBehaviour
{
    // 選択数字
    public int num { get; set; } = 0;
    
    // 選択ディレイのためのフラグ
    public bool isDelay { get; set; }

    // 選択イメージ
    [SerializeField]
    Animator anim; 

    // UIパネル
    [SerializeField]
    GameObject[] uiPanel;

    TitleButton titleButton;
    OptionManager optionManager;
    EndPanelManager endPanelManager;

    [SerializeField] VolumeConfigUI volumeConfigUI;

    ITitleSelect title;

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
        //入力
        Vector2 selectvalue = myInput.Player.Move.ReadValue<Vector2>();

        // 終了パネル表示時
        if (uiPanel[1].activeSelf == true)
        {
            endPanelManager.EndWindow(this, title, anim, selectvalue.x, 
                myInput.Player.Move.WasPressedThisFrame(), myInput.UI.Decision.WasPressedThisFrame(), myInput.UI.Return.WasPressedThisFrame());
        }
        // optionパネル表示時
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
