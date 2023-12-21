using DG.Tweening;
using SoundSystem;
using TMPro;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    [SerializeField, Header("ポーズパネル")]
    GameObject pausePanel;
    [SerializeField, Header("リザルトパネル")]
    GameObject resultPanel;
    [SerializeField, Header("タイマーテキスト")]
    TextMeshProUGUI taimeText;
    [SerializeField, Header("Volumeコンフィグ")]
    VolumeConfigUI volumeConfigUI;
    [SerializeField, Header("チュートリアルパネル")]
    GameObject tutorialPanel;


    // ポーズが表示されているか
    public bool IsPauseOpen { get { return pausePanel.activeSelf; } }
    // リザルトが表示されているか
    public bool IsResultOpen { get { return resultPanel.activeSelf; } }

    // リザルトパネル表示時間
    const float fadeTime = 0.5f;
    const float fadeValue = 1f;

    private void Start()
    {
        switch(StageNumberSelect.Instance.StageNumber)
        {
            case 0:
                tutorialPanel.SetActive(true); 
                break;
            case 1:
                tutorialPanel.SetActive(false);
                break;
        }

        // スライダーの数値反映
        volumeConfigUI.SetMasterVolume(SoundManager.Instance.MasterVolume);
        volumeConfigUI.SetBGMVolume(SoundManager.Instance.BGMVolume);
        volumeConfigUI.SetSeVolume(SoundManager.Instance.SEVolume);
        // ボリュームの設定
        volumeConfigUI.SetMasterSliderEvent(vol => SoundManager.Instance.MasterVolume = vol);
        volumeConfigUI.SetBGMSliderEvent(vol => SoundManager.Instance.BGMVolume = vol);
        volumeConfigUI.SetSeSliderEvent(vol => SoundManager.Instance.SEVolume = vol);
    }

    // ステージタイム更新
    public void TimeUpdate(float time)
    {
        taimeText.text = Mathf.Floor(time).ToString();
    }

    // リザルト表示
    public void ResultsDisplay(float time)
    {
        resultPanel.SetActive(true);
        var canvasGroup =  resultPanel.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(fadeValue, fadeTime);
        var result = resultPanel.GetComponent<ResultPanel>();
        result.ClearResult(time);
    }
}
