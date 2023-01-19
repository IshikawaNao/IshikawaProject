using UnityEngine;
using UnityEngine.UI;

public class EndPanelManager : MonoBehaviour
{
    const int minQuitNum = 0;
    const int maxQuitNum = 1;
    int quitNum = 0;

    [SerializeField]
    TitleManager tm;

    [SerializeField]
    Animator anim;

    ButtonMove bm;

    KeyInput input;

    [SerializeField]
    Image[] button;

    [SerializeField] 
    VolumeConfigUI volumeConfigUI;

    private void Start()
    {
        input = KeyInput.Instance;
        bm = new ButtonMove();
    }

    private void Update()
    {
        EndWindow(tm, anim, bm, input.InputMove.x, input.PressedMove, input.DecisionInput);
        ReturnButton();
    }

    public void EndWindow(TitleManager tm , Animator anim, ButtonMove bm, float value, bool input, bool decision)
    {
        // アニメーションが再生されている間この処理に入らないようにする
        if (input && bm.SelectDelyTime())
        {
            QuitNum(value);
            bm.SelectTextMove(button, quitNum, maxQuitNum);
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
        tm.EndPanel = true;
    }

    // 戻る
    void ReturnButton()
    {
        if (input.EscInput)
        {
            tm.EndPanel = true;
            anim.SetBool("PanelEnd", true);
        }
    }

    //　終了パネル表示時選択番号取得
    public void QuitNum(float input)
    {
        if (input > 0)
        {
            quitNum--;
        }
        else if (input < 0)
        {
            quitNum++;
        }
        quitNum = Mathf.Clamp(quitNum, minQuitNum,maxQuitNum);
    }

    // 終了選択決定処理
    public void QuitDecision(int num, Animator anim)
    {
        switch (num)
        {
            case 0:
                anim.SetBool("PanelEnd",true);
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
