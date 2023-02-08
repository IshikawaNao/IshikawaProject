using UnityEngine;
using UnityEngine.UI;

public class EndPanelManager : MonoBehaviour
{
    const int minNum = 0;
    const int maxNum = 1;
    int quitNum = 0;

    [SerializeField]
    TitleManager tm;

    [SerializeField]
    Animator anim;

    ButtonMove bm;
    UiAddition ua;

    KeyInput input;

    [SerializeField]
    Image[] button;

    [SerializeField] 
    VolumeConfigUI volumeConfigUI;

    [SerializeField]
    OptionUIManager om;

    private void Start()
    {
        input = KeyInput.Instance;
        bm = new ButtonMove();
        ua = new UiAddition();
        button[0].color = Color.white;
        button[1].color = Color.blue;
    }

    private void Update()
    {
        EndWindow(anim, input.InputMove.x, input.PressedMove, input.DecisionInput);
        ReturnButton();
    }

    void EndWindow(Animator anim, float value, bool input, bool decision)
    {
        // アニメーションが再生されている間この処理に入らないようにする
        if (input && bm.SelectDelyTime())
        {
            quitNum = ua.Addition(quitNum, minNum, maxNum, value );
            bm.SelectTextMove(button, quitNum, maxNum);
            print(quitNum);
        }

        if (decision)
        {
            anim.SetBool("PanelEnd", true);
            //次の処理までのディレイ
            Invoke("Delay", 0.5f);

            // 終了処理
            QuitDecision(quitNum, anim);
        }
    }

    // 選択ディレイ
    void Delay()
    {
        om.EndPanel = true;
    }

    // 戻る
    void ReturnButton()
    {
        if (input.EscInput)
        {
            om.EndPanel = true;
            anim.SetBool("PanelEnd", true);
            quitNum = 0;
            button[0].color = Color.white;
            button[1].color = Color.blue;
        }
    }

    // 終了選択決定処理
    void QuitDecision(int num, Animator anim)
    {
        switch (num)
        {
            case 0:
                om.EndPanel = true;
                anim.SetBool("PanelEnd",true);
                quitNum = 0;
                button[0].color = Color.white;
                button[1].color = Color.blue;
                break;
            case 1:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
                break;
        }
    }
}
