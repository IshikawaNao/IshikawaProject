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
        // �`���[�g���A���X�e�[�W����Ȃ��ꍇ�X�N���v�g�𖳌�
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

    // �`���[�g���A���p�l����\������
    void TutorialPanelDisplay()
    {
        if(!stageManager.IsTimeLine && !isTutoria)
        {
            isTutoria = true;
            tutorialPanel.SetActive(true);
        }
    }

    // �`���[�g���A���p�l�����\���ɂ���
    void TutorialPanelNonDisplay()
    {
        if (isTutoria && input.EscInput)
        {
            Invoke("Delay", 0.2f);
        }
    }

    // ���͂��d�����Ȃ��悤�Ƀf�B���C
    void Delay()
    {
        isTutoria = false;
        tutorialPanel.SetActive(false);
        this.enabled = false;
    }
}
