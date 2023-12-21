using UnityEngine;
using DG.Tweening;
using SoundSystem;
using TMPro;
using UnityEngine.Playables;
using UniRx;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// �X�e�[�W�}�l�[�W���[
/// </summary>
public class StageManager : MonoBehaviour
{
    [SerializeField, Header("�v���C���[")]
    PlayerController playerCon;
    [SerializeField, Header("�^�C�����C��")]
    GameObject teleportTimeLine;
    [SerializeField, Header("UIManager")]
    MainUIManager mainUIManager;

    PlayableDirector timeLine;
    GoalCheck goalCheck;
    FallingGameOver fallCheck;

    // Instance
    KeyInput input;
    SaveDataManager saveData;
    StageNumberSelect sn;

    AsyncOperationHandle<GameObject> handle;

    // �X�^�[�g���̃A�j���[�V�����t���O
    bool isTimeLine = true;
    public bool IsTimeLine { get{ return isTimeLine; } }

    bool stageCliar = false;
    bool falling;
    public bool Fall { get { return falling; } }

    // �^�C��
    float stageTime = 0;
    public float StageTime { get { return stageTime; } }  
    float timer;
    bool isTimer = false;

    // �X�e�[�W���
    int stageNum;
    public int StageNum { get { return stageNum; } }

    //�@�T�E���h�t�F�[�h
    const float SoundFadeTime = 1f;
    const float SceneLoadFadeTime = 1.5f;

    // �^�C�����C���b��
    const float TimeLinePlaybackTime = 6f;
    const float TimeLineTeleportPlaybackTime = 3f;

    // StagePass
    private const string Stage0InstancePas = "Assets/MyProject/RunTime/AssetData/Stage0.prefab";
    private const string Stage1InstancePas = "Assets/MyProject/RunTime/AssetData/Stage1.prefab";

    async void Awake()
    {
        var pas = "";

        // ��������X�e�[�W�̑I��
        switch (StageNumberSelect.Instance.StageNumber)
        {
            case 0:
                pas = Stage0InstancePas;
                break;
            case 1:
                pas = Stage1InstancePas;
                break;
        }

        handle = Addressables.InstantiateAsync(pas);
        // .Task�ŃC���X�^���X�������܂�await�ł���
        await handle.Task;
    }

    async void Start()
    {
        await handle.Task;

        input = KeyInput.Instance;
        saveData = SaveDataManager.Instance;
        saveData.Load();

        // �}�E�X�J�[�\�����\��
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        sn = StageNumberSelect.Instance;
        
        stageNum = sn.StageNumber;

        SoundManager.Instance.PlayBGMWithFadeIn((int)BGMList.Main, SoundFadeTime);

        timeLine = teleportTimeLine.GetComponent<PlayableDirector>();

        StartAnimationTime();

        goalCheck = FindObjectOfType<GoalCheck>();
        fallCheck = FindObjectOfType<FallingGameOver>();
        fallCheck.Count.Subscribe(value => ReStartAnimationTime());
    }

    void Update()
    {
        Cliar();
        TimeMeasurement();
    }
    
    // �^�C�}�[����
    void TimeMeasurement()
    {
        if(isTimer)
        {
            stageTime = Time.time - timer;
            mainUIManager.TimeUpdate(stageTime);
        }
    }

    // �N���A�����ۂɌĂ΂��
    void Cliar()
    {
        // �X�e�[�W����������Ă��Ȃ��Ȃ�Ԃ�
        if(goalCheck == null) { return; }

        if (goalCheck.Goal && !stageCliar)
        {
            stageCliar = true;
            switch (stageNum)
            {
                case 0:
                    saveData.ClearTime1Save(Mathf.Floor(stageTime));
                    teleportTimeLine.SetActive(true);
                    timeLine.Play();
                    break;
                case 1:
                    saveData.ClearTime2Save(Mathf.Floor(stageTime));
                    teleportTimeLine.SetActive(true);
                    timeLine.Play();

                    break;
            }
            mainUIManager.ResultsDisplay(stageTime);
            DOVirtual.DelayedCall(TimeLineTeleportPlaybackTime / 2, () => { playerCon.gameObject.SetActive(false); });
        }

        if (input.DecisionInput && mainUIManager.IsResultOpen)
        {
            FadeManager.Instance.LoadScene("Title", SceneLoadFadeTime);
        }
    }

    // �X�^�[�g�A�j���[�V�����������Ă���ԃv���C���[�ƃ^�C�}�[���~�߂�
    void StartAnimationTime()
    {
        isTimeLine = false;
        DOVirtual.DelayedCall(TimeLinePlaybackTime, () => { isTimeLine = true; timer = Time.time; isTimer = true; });
    }

    // ������
    void ReStartAnimationTime()
    {
        if (!fallCheck.IsFalling) { return; }
        falling = true;
        isTimeLine = false;
        teleportTimeLine.SetActive(true);
        timeLine.Play();
        DOVirtual.DelayedCall(TimeLineTeleportPlaybackTime, () => { isTimeLine = true; falling = false; });
    }
}
