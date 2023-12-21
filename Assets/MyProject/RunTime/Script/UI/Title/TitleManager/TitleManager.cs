using UnityEngine;
using SoundSystem;
using UniRx;
using Cysharp.Threading.Tasks;
using System;

public class TitleManager : MonoBehaviour
{
    [SerializeField, Header("�^�C�g���Z���N�g")]
    GameObject titleObj;
    [SerializeField, Header("�I�v�V�����p�l��")]
    VolumeConfigUI volumeConfigUI;
    [SerializeField, Header("�e�X�e�[�g")]
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

        // �}�E�X�J�[�\�����\��
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // �X���C�_�[�̐��l���f
        volumeConfigUI.SetMasterVolume(SoundManager.Instance.MasterVolume);
        volumeConfigUI.SetBGMVolume(SoundManager.Instance.BGMVolume);
        volumeConfigUI.SetSeVolume(SoundManager.Instance.SEVolume);
        // �{�����[���̐ݒ�
        volumeConfigUI.SetMasterSliderEvent(vol => SoundManager.Instance.MasterVolume = vol);
        volumeConfigUI.SetBGMSliderEvent(vol => SoundManager.Instance.BGMVolume = vol);
        volumeConfigUI.SetSeSliderEvent(vol => SoundManager.Instance.SEVolume = vol);

        title = titleObj.GetComponent<TitleView>();

        // ����{�^�����������m
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
        // �߂�{�^�����������m
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

    // �I���X�e�[�g�ύX
    private async void StateChange()
    {
        
        // �A�j���[�V�������I���܂ŏ������~�߂�
        await title.DisableAnimation();
        var state = stateObject[title.SelectionNumbar];
        // �I�����ꂽ�X�e�[�g��\������
        state.SetActive(true);
        // �I�����ꂽObject�̃A�j���[�V������View���擾
        titleUIView = state.GetComponent<IUIView>();
    }
    // �^�C�g���Z���N�g�֖߂�
    private async void Back()
    {
        SoundManager.Instance.PlayOneShotSe((int)SEList.Cancel);
        // �I������Ă����X�e�[�g�̃A�j���[�V����
        await titleUIView.DisableAnimation();
        // �^�C�g����\������
        titleObj.SetActive(true);
    }
}
