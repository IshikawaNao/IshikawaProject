using UnityEngine;
using UnityEngine.UI;

public class EndPanelManager : MonoBehaviour
{
    const int minQuitNum = 0;
    const int maxQuitNum = 1;
    int quitNum = 0;

    public bool EndPanel { get; set; } = true;

    [SerializeField]
    TitleManager tm;

    [SerializeField]
    Animator anim;

    ButtonMove bm;

    [SerializeField]
    KeyInput input;

    [SerializeField]
    Image[] button;

    [SerializeField] 
    VolumeConfigUI volumeConfigUI;

    private void Start()
    {
        bm = new ButtonMove();
    }

    private void Update()
    {
        EndWindow(tm, anim, bm, input.InputMove.x, input.PressedMove, input.DecisionInput);
    }

    public void EndWindow(TitleManager tm , Animator anim, ButtonMove bm, float value, bool input, bool decision)
    {
        // �A�j���[�V�������Đ�����Ă���Ԃ��̏����ɓ���Ȃ��悤�ɂ���
        if (input && bm.SelectDelyTime())
        {
            QuitNum(value);
            bm.SelectTextMove(button, quitNum, maxQuitNum);
        }

        if (decision)
        {
            anim.SetBool("PanelEnd", true);
            //���̏����܂ł̃f�B���C
            Invoke("Delay", 0.5f);

            // �I������
            QuitDecision(quitNum, anim);
        }
    }

    // �I���f�B���C
    void Delay()
    {
        EndPanel = true;
    }

    //�@�I���p�l���\�����I��ԍ��擾
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

    // �I���I�����菈��
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
