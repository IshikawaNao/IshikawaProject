using UnityEngine;

public class TutorialStageDisplay : MonoBehaviour
{
    [SerializeField]
    StageManager stageManager;
    [SerializeField]
    GameObject tutorialPanel;

    KeyInput input;

    bool isTutoria = false;
    public bool IsTutoria { get { return isTutoria; } }

    private void Start()
    {
        // チュートリアルステージじゃない場合スクリプトを無効
        if (stageManager.StageNum != 0)
        {
            this.enabled = false;
        }
        input = KeyInput.Instance;
    }

    private void Update()
    {
        TutorialPanelDisplay();
        TutorialPanelNonDisplay();
    }

    // チュートリアルパネルを表示する
    void TutorialPanelDisplay()
    {
        if(!stageManager.IsTimeLine && !isTutoria)
        {
            isTutoria = true;
            tutorialPanel.SetActive(true);
        }
    }

    // チュートリアルパネルを非表示にする
    void TutorialPanelNonDisplay()
    {
        if (isTutoria && input.EscInput)
        {
            Invoke("Delay", 0.2f);
        }
    }

    // 入力が重複しないようにディレイ
    void Delay()
    {
        isTutoria = false;
        tutorialPanel.SetActive(false);
        this.enabled = false;
    }
}
