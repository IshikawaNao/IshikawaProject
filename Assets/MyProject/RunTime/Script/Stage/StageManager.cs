using UnityEngine;
using DG.Tweening;
using SoundSystem;
using TMPro;

/// <summary>
/// �X�e�[�W�}�l�[�W���[
/// </summary>
public class StageManager : MonoBehaviour
{
    // �S�[���t���O
    bool isGoal = true;
    public bool Goal { get; set; } = false;

    // �X�^�[�g���̃A�j���[�V�����t���O
    bool isStart = true;
    public bool IsStart { get { return isStart; } }
    
    // �^�C��
    float tm = 0;
    float timer;

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

    // Instance
    KeyInput input;
    StageNumberSelect sn;

    private void Awake()
    {
        sn = StageNumberSelect.Instance;

        if (sn == null)
        {
            GameObject obj = (GameObject)Resources.Load("Stage" + 0);
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
        // �}�E�X�J�[�\�����\��
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

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

        StartAnimationTime();
        SoundManager.Instance.PlayBGMWithFadeIn("Main", soundFadeTime);
    }

    void Update()
    {
        Cliar();
        SwithingOperation();
        TimeMeasurement();
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
        if(!IsStart)
        {
            tm = Time.time - timer;
            TaimeText.text = Mathf.Floor(tm).ToString();
        }
    }

    // �N���A�����ۂɌĂ΂��
    void Cliar()
    {
        if(Goal && isGoal)
        {
            isGoal = false;
            SaveDataManager.Instance.Load();
            switch(stageNum)
            {
                case 0:
                    SaveDataManager.Instance.ClearTime1Save(Mathf.Floor(tm));
                    break;
                case 1:
                    SaveDataManager.Instance.ClearTime2Save(Mathf.Floor(tm));
                    break;


            }
            
            SaveDataManager.Instance.Save();
            FadeManager.Instance.LoadScene("Result", 1.5f);
        }
    }

    // �X�^�[�g�A�j���[�V�����������Ă���ԃv���C���[�ƃ^�C�}�[���~�߂�
    void StartAnimationTime()
    {
        DOVirtual.DelayedCall(8, () => { isStart = false; timer = Time.time; });
    }
}
