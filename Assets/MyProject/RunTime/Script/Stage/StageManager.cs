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
    string rank = "";

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
    CreateData cd;
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
        cd = CreateData.Instance;


        // �X���C�_�[�̐��l���f
        volumeConfigUI.SetMasterVolume(SoundManager.Instance.MasterVolume);
        volumeConfigUI.SetBGMVolume(SoundManager.Instance.BGMVolume);
        volumeConfigUI.SetSeVolume(SoundManager.Instance.SEVolume);

        // �{�����[���̐ݒ�
        volumeConfigUI.SetMasterSliderEvent(vol => SoundManager.Instance.MasterVolume = vol);
        volumeConfigUI.SetBGMSliderEvent(vol => SoundManager.Instance.BGMVolume = vol);
        volumeConfigUI.SetSeSliderEvent(vol => SoundManager.Instance.SEVolume = vol);

        // �Z�[�u�f�[�^�Ăяo��
        cd.VolSet();
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
            ClearRank();
            cd.SaveClearData(Mathf.Floor(tm), rank, stageNum);
            FadeManager.Instance.LoadScene("Result", 1.5f);
        }
    }

    // �N���A�����N��ݒ肷��
    void ClearRank()
    {
        if(tm <= 30) { rank = "S"; }
        else if(tm <= 50) { rank = "A"; }
        else if(tm <= 180) { rank = "B"; }
        else { rank = "C"; }
    }

    // �X�^�[�g�A�j���[�V�����������Ă���ԃv���C���[�ƃ^�C�}�[���~�߂�
    void StartAnimationTime()
    {
        DOVirtual.DelayedCall(8, () => { isStart = false; timer = Time.time; });
    }
}
