using UnityEngine;
using SoundSystem;
using UniRx;

public class TitleManager : MonoBehaviour
{
    KeyInput input;

    [SerializeField]
    TitleUIPresenter title;
    [SerializeField, Header("�I�v�V�����p�l��")]
    VolumeConfigUI volumeConfigUI;
    [SerializeField, Header("�e�X�e�[�g")]
    GameObject[] StateObj;
    int num = 0;

    TitleStateView titleStateView = new TitleStateView();

    public int ModelNum { get { return title.ModelNum; } }
    int stateNum = 0;
    public int StateNum { get { return stateNum; } }
    public bool IsTitleUI { get { return title.IsTitleUI; } }

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


        title.StateObservable.Subscribe(state => titleStateView.ChangeState(StateObj, state));
        // �^�C�g��UI�؂�ւ�
        titleStateView.ChangeState(StateObj, stateNum);

        SoundManager.Instance.PlayBGMWithFadeIn("Title");
    }

    private void Update()
    {
        StateNumChange();
        Back();
    }

    void StateNumChange()
    {
        if(stateNum�@!= num)
        {
            num = stateNum;
            
        }
    }

    private void Back()
    {
        if (!input.EscInput)
        {
            return;
        }
        titleStateView.ChangeState(StateObj, 0);
    }
}
