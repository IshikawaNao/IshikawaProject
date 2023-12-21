using UnityEngine;
using SoundSystem;
using UniRx;
using Cysharp.Threading.Tasks;
using System;

public class TitleManager : MonoBehaviour
{
    [SerializeField, Header("タイトルセレクト")]
    GameObject titleObj;
    [SerializeField, Header("オプションパネル")]
    VolumeConfigUI volumeConfigUI;
    [SerializeField, Header("各ステート")]
    GameObject[] stateObject;

    KeyInput input;
    TitleView title;
    IUIView titleUIView;

    bool isStartAnimation = true;

    const float FadeArrivalTime = 1.5f;

    enum State
    {
        Stage,
        Option,
        End,
    }

    private void Start()
    {
        input = KeyInput.Instance;

        // マウスカーソルを非表示
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // スライダーの数値反映
        volumeConfigUI.SetMasterVolume(SoundManager.Instance.MasterVolume);
        volumeConfigUI.SetBGMVolume(SoundManager.Instance.BGMVolume);
        volumeConfigUI.SetSeVolume(SoundManager.Instance.SEVolume);
        // ボリュームの設定
        volumeConfigUI.SetMasterSliderEvent(vol => SoundManager.Instance.MasterVolume = vol);
        volumeConfigUI.SetBGMSliderEvent(vol => SoundManager.Instance.BGMVolume = vol);
        volumeConfigUI.SetSeSliderEvent(vol => SoundManager.Instance.SEVolume = vol);

        title = titleObj.GetComponent<TitleView>();

        // 決定ボタン押下を検知
        input.DecisionInputDetection
            .Where(x => !isStartAnimation && x)
            .Subscribe(x =>
            {
                if (stateObject[(int)State.Stage].activeSelf)
                {
                    SceneChange();
                }
                else if(stateObject[(int)State.End].activeSelf)
                {
                    Back();
                }
                else
                {
                    StateChange();
                }
            }).AddTo(this);
        // 戻るボタン押下を検知
        input.BackInput
            .Where(x => !titleObj.activeSelf && !isStartAnimation && !x)
            .Subscribe(x =>
            {
                Back();
            }).AddTo(this);

        isStartAnimation = false;
        titleObj.SetActive(true);
        SoundManager.Instance.PlayBGMWithFadeIn((int)BGMList.Title);
    }

    public async void SceneChange()
    {
        FadeManager.Instance.LoadScene("Main", FadeArrivalTime);
        await UniTask.Delay(TimeSpan.FromSeconds(FadeArrivalTime));
    }

    // 選択ステート変更
    private async void StateChange()
    {
        
        // アニメーションが終わるまで処理を止める
        await title.DisableAnimation();
        var state = stateObject[title.SelectionNumbar];
        // 選択されたステートを表示する
        state.SetActive(true);
        // 選択されたObjectのアニメーションのViewを取得
        titleUIView = state.GetComponent<IUIView>();
    }
    // タイトルセレクトへ戻る
    private async void Back()
    {
        SoundManager.Instance.PlayOneShotSe((int)SEList.Cancel);
        // 選択されていたステートのアニメーション
        await titleUIView.DisableAnimation();
        // タイトルを表示する
        titleObj.SetActive(true);
    }
}
