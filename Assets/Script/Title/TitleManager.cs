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
    bool isDelay = true;

    // 選択イメージ
    [SerializeField]
    Animator anim; 


    // UIパネル
    [SerializeField]
    GameObject[] uiPanel;


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
        num = 0;
    }

    void Update()
    {
        print(num);
        //入力をvector2でもって来る
        Vector2 selectvalue = myInput.Player.Move.ReadValue<Vector2>();

        // 終了パネル表示時
        if (uiPanel[1].activeSelf == true)
        {
            if (num == 2) { num = 3; }

            // アニメーションが再生されている間この処理に入らないようにする
            if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "TitleStart" && myInput.Player.Move.WasPressedThisFrame())
            {
                //　終了パネル表示時選択番号取得
                num = title.QuitNum(selectvalue.x);
            }
            
            isDelay = false;

            if (myInput.UI.Decision.WasPressedThisFrame())
            {
                // 終了処理
                title.QuitDecision(num, anim);
                num = 2;
                //次の処理までのディレイ
                Invoke("Delay", 0.5f);
            }

            // 戻る決定時
            if (myInput.UI.Return.WasPressedThisFrame())
            {
                anim.SetTrigger("PanelEnd");
 
                //次の処理までのディレイ
                Invoke("Delay", 0.5f);
                num = 2;
            }
        }
        // optionパネル表示時
        else if (uiPanel[0].activeSelf == true)
        {
            isDelay = false;
            // 戻る決定時
            if (myInput.UI.Return.WasPressedThisFrame())
            {
                anim.SetTrigger("PanelEnd");
                //次の処理までのディレイ
                Invoke("Delay", 0.5f);
            }
        }
        else
        {
            //　決定処理
            if (myInput.UI.Decision.WasPressedThisFrame())
            {
                title.SelecDecision(num, anim);
            }
    
            // key入力orLスティック操作時
            if (myInput.Player.Move.WasPressedThisFrame())
            {
                // 選択番号取得
                num = title.SelectNum(selectvalue.y,num);

                // 選択処理
                title.UISelect(num);
                isDelay = false;
                //次の処理までのディレイ
                Invoke("Delay", 0.5f);
            }
            else if (myInput.Player.Move.IsPressed())
            {
                if (isDelay)
                {
                    // 選択番号取得
                    num = title.SelectNum(selectvalue.y, num);

                    // 選択処理
                    title.UISelect(num);

                    isDelay = false;
                }
            }

            if (num < 0) { num = 0; }
            else if (num > 2) { num = 2; }
        }
       
    }
    // 選択ディレイ
    void Delay()
    {
        isDelay = true;
    }
}
