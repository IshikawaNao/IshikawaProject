using DG.Tweening;
using SoundSystem;
using TMPro;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    [SerializeField, Header("�|�[�Y�p�l��")]
    GameObject pausePanel;
    [SerializeField, Header("���U���g�p�l��")]
    GameObject resultPanel;
    [SerializeField, Header("�^�C�}�[�e�L�X�g")]
    TextMeshProUGUI taimeText;
    [SerializeField, Header("Volume�R���t�B�O")]
    VolumeConfigUI volumeConfigUI;
    [SerializeField, Header("�`���[�g���A���p�l��")]
    GameObject tutorialPanel;


    // �|�[�Y���\������Ă��邩
    public bool IsPauseOpen { get { return pausePanel.activeSelf; } }
    // ���U���g���\������Ă��邩
    public bool IsResultOpen { get { return resultPanel.activeSelf; } }

    // ���U���g�p�l���\������
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

        // �X���C�_�[�̐��l���f
        volumeConfigUI.SetMasterVolume(SoundManager.Instance.MasterVolume);
        volumeConfigUI.SetBGMVolume(SoundManager.Instance.BGMVolume);
        volumeConfigUI.SetSeVolume(SoundManager.Instance.SEVolume);
        // �{�����[���̐ݒ�
        volumeConfigUI.SetMasterSliderEvent(vol => SoundManager.Instance.MasterVolume = vol);
        volumeConfigUI.SetBGMSliderEvent(vol => SoundManager.Instance.BGMVolume = vol);
        volumeConfigUI.SetSeSliderEvent(vol => SoundManager.Instance.SEVolume = vol);
    }

    // �X�e�[�W�^�C���X�V
    public void TimeUpdate(float time)
    {
        taimeText.text = Mathf.Floor(time).ToString();
    }

    // ���U���g�\��
    public void ResultsDisplay(float time)
    {
        resultPanel.SetActive(true);
        var canvasGroup =  resultPanel.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(fadeValue, fadeTime);
        var result = resultPanel.GetComponent<ResultPanel>();
        result.ClearResult(time);
    }
}
