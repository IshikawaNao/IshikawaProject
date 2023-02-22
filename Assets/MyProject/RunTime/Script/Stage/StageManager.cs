using UnityEngine;
using DG.Tweening;
using SoundSystem;
using TMPro;

/// <summary>
/// ステージマネージャー
/// </summary>
public class StageManager : MonoBehaviour
{
    // ゴールフラグ
    bool isGoal = true;
    public bool Goal { get; set; } = false;

    float tm = 0;
    float timer;

    bool isStart = true;
    public bool IsStart { get { return isStart; } }

    const float soundFadeTime = 1f;

    const int minOperationNum = 0; 
    const int maxOperationNum = 3;

    int stageNum;

    string rank = "";

    [SerializeField, Header("player")]
    PlayerController player;

    [SerializeField, Header("OptionPanel")] 
    VolumeConfigUI volumeConfigUI;

    [SerializeField]
    GameObject[] keyOperation;
    [SerializeField]
    GameObject[] padOperation;

    KeyInput input;

    CreateData cd;

    [SerializeField]
    TextMeshProUGUI  TaimeText;

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
            GameObject obj = (GameObject)Resources.Load("Stage" + sn.StageNumber);
            Instantiate(obj, Vector3.zero, Quaternion.identity);
        }
        stageNum = sn.StageNumber;
    }


    void Start()
    {     
        input = KeyInput.Instance;
        cd = CreateData.Instance;


        // スライダーの数値反映
        volumeConfigUI.SetMasterVolume(SoundManager.Instance.MasterVolume);
        volumeConfigUI.SetBGMVolume(SoundManager.Instance.BGMVolume);
        volumeConfigUI.SetSeVolume(SoundManager.Instance.SEVolume);

        // ボリュームの設定
        volumeConfigUI.SetMasterSliderEvent(vol => SoundManager.Instance.MasterVolume = vol);
        volumeConfigUI.SetBGMSliderEvent(vol => SoundManager.Instance.BGMVolume = vol);
        volumeConfigUI.SetSeSliderEvent(vol => SoundManager.Instance.SEVolume = vol);

        cd.VolSet();
        timer = 0;
        TaimeText.text = Mathf.Floor(timer).ToString();
        StartTime();
        SoundManager.Instance.PlayBGMWithFadeIn("Main", soundFadeTime);
    }

    void Update()
    {
        Cliar();
        SwithingOperation();
        TimeMeasurement();
    }

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
    
    void TimeMeasurement()
    {
        if(!IsStart)
        {
            tm = Time.time - timer;
            TaimeText.text = Mathf.Floor(tm).ToString();
        }
    }

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

    void ClearRank()
    {
        if(tm <= 30) { rank = "S"; }
        else if(tm <= 50) { rank = "A"; }
        else if(tm <= 180) { rank = "B"; }
        else { rank = "C"; }
    }

    void StartTime()
    {
        DOVirtual.DelayedCall(6, () => { isStart = false; timer = Time.time; });
    }
}
