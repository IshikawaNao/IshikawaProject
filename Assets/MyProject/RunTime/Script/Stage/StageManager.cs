using UnityEngine;
using DG.Tweening;
using SoundSystem;
using TMPro;
using UnityEngine.Playables;
using UniRx;

/// <summary>
/// �X�e�[�W�}�l�[�W���[
/// </summary>
public class StageManager : MonoBehaviour
{
    // �X�^�[�g���̃A�j���[�V�����t���O
    //bool isTimeLine = true;
    public bool IsTimeLine { get; set; }

    public bool Goal { get { return isGoal.Goal; } }
    bool stageCliar = false;

    public bool Fall { get { return isFall.IsFalling; } }
    
    // �^�C��
    float tm = 0;
    float timer;
    bool isTimer = false;

    //�@�T�E���h�t�F�[�h
    const float soundFadeTime = 1f;

    // �I�v�V�����i���o�[
    const int minOperationNum = 0; 
    const int maxOperationNum = 3;

    // �X�e�[�W���
    int stageNum;
    public int StageNum { get { return stageNum; } }

    [SerializeField, Header("�v���C���[")]
    PlayerController player;

    [SerializeField, Header("�I�v�V�����p�l��")] 
    VolumeConfigUI volumeConfigUI;
    [SerializeField,Header("�L�[�{�[�h�\��")]
    GameObject[] keyOperation;
    [SerializeField, Header("Pad�\��")]
    GameObject[] padOperation;
    [SerializeField,Header("�^�C�}�[�e�L�X�g")]
    TextMeshProUGUI  TaimeText;
    [SerializeField, Header("�^�C�����C��")]
    GameObject StartTimeLine;
    [SerializeField, Header("�^�C�����C���A�j���[�V����")]
    PlayableDirector playableDirector;

    GoalCheck isGoal;
    FallingGameOver isFall;

    // Instance
    KeyInput input;
    SaveDataManager saveData;
    StageNumberSelect sn;

    private void Awake()
    {
        sn = StageNumberSelect.Instance;

        if (sn == null)
        {
            GameObject obj = (GameObject)Resources.Load("Stage" + 1);
            Instantiate(obj, Vector3.zero, Quaternion.identity);
        }
        else
        {
            // �I�����ꂽ�X�e�[�W�𐶐�����
            GameObject obj = (GameObject)Resources.Load("Stage" + sn.StageNumber);
            Instantiate(obj, Vector3.zero, Quaternion.identity);
        }
        stageNum = sn.StageNumber;
    }


    void Start()
    {     
        input = KeyInput.Instance;
        saveData = SaveDataManager.Instance;
        saveData.Load();

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

        timer = 0;
        TaimeText.text = Mathf.Floor(timer).ToString();

        SoundManager.Instance.PlayBGMWithFadeIn("Main", soundFadeTime);

        isGoal = GameObject.Find("MagicCircuitGoal").GetComponent<GoalCheck>();
        isFall = GameObject.Find("FallingGameOver").GetComponent<FallingGameOver>();

        isFall.Count.Subscribe(value => ReStartAnimationTime());
        StartAnimationTime();
    }

    void Update()
    {
        Cliar();
        SwithingOperation();
        TimeMeasurement();
        print(IsTimeLine);
    }

    // ���������Pad�ƃL�[�{�[�h�̕\����؂�ւ���
    void SwithingOperation()
    {
        if(input.Inputdetection)
        {
            for(int i = minOperationNum; i <= maxOperationNum; i++)
            {
                keyOperation[i].SetActive(true);
                padOperation[i].SetActive(false);
            }
        }
        else if(!input.Inputdetection)
        {
            for (int i = minOperationNum; i <= maxOperationNum; i++)
            {
                keyOperation[i].SetActive(false);
                padOperation[i].SetActive(true);
            }
        }
    }
    
    // �^�C�}�[����
    void TimeMeasurement()
    {
        if(isTimer)
        {
            tm = Time.time - timer;
            TaimeText.text = Mathf.Floor(tm).ToString();
        }
    }

    // �N���A�����ۂɌĂ΂��
    void Cliar()
    {
        if(isGoal.Goal && !stageCliar)
        {
            stageCliar = true;
            switch (stageNum)
            {
                case 0:
                    saveData.ClearTime1Save(Mathf.Floor(tm));
                    break;
                case 1:
                    saveData.ClearTime2Save(Mathf.Floor(tm));
                    break;
            }
            saveData.Save();
            FadeManager.Instance.LoadScene("Result", 1.5f);
        }
    }

    // �X�^�[�g�A�j���[�V�����������Ă���ԃv���C���[�ƃ^�C�}�[���~�߂�
    void StartAnimationTime()
    {
        StartTimeLine.transform.position = new Vector3(player.transform.position.x, player.transform.position.y+0.12f, player.transform.position.z);
        DOVirtual.DelayedCall(6f, () => {  timer = Time.time; isTimer = true; });
    }

    // �X�^�[�g�A�j���[�V�����������Ă���ԃv���C���[���~�߂�
    void ReStartAnimationTime()
    {
        //isStart = true;
        StartTimeLine.transform.position = new Vector3(isFall.ResetPosition.x, isFall.ResetPosition.y + 0.15f, isFall.ResetPosition.z);
        playableDirector.Play();
        //DOVirtual.DelayedCall(7.5f, () => { isStart = false;});
    }
}
