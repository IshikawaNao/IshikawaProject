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

    // スタート時のアニメーションフラグ
    bool isStart = true;
    public bool IsStart { get { return isStart; } }
    
    // タイム
    float tm = 0;
    float timer;

    //　サウンドフェード
    const float soundFadeTime = 1f;

    // オプションナンバー
    const int minOperationNum = 0; 
    const int maxOperationNum = 3;

    // ステージ情報
    int stageNum;
    public int StageNum { get { return stageNum; } }

    [SerializeField, Header("プレイヤー")]
    PlayerController player;

    [SerializeField, Header("オプションパネル")] 
    VolumeConfigUI volumeConfigUI;

    [SerializeField,Header("キーボード表示")]
    GameObject[] keyOperation;
    [SerializeField, Header("Pad表示")]
    GameObject[] padOperation;

    [SerializeField,Header("タイマーテキスト")]
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
            // 選択されたステージを生成する
            GameObject obj = (GameObject)Resources.Load("Stage" + sn.StageNumber);
            Instantiate(obj, Vector3.zero, Quaternion.identity);
        }
        stageNum = sn.StageNumber;
    }


    void Start()
    {     
        input = KeyInput.Instance;
        // マウスカーソルを非表示
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        // スライダーの数値反映
        volumeConfigUI.SetMasterVolume(SoundManager.Instance.MasterVolume);
        volumeConfigUI.SetBGMVolume(SoundManager.Instance.BGMVolume);
        volumeConfigUI.SetSeVolume(SoundManager.Instance.SEVolume);

        // ボリュームの設定
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

    // 操作説明のPadとキーボードの表示を切り替える
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
    
    // タイマー処理
    void TimeMeasurement()
    {
        if(!IsStart)
        {
            tm = Time.time - timer;
            TaimeText.text = Mathf.Floor(tm).ToString();
        }
    }

    // クリアした際に呼ばれる
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

    // スタートアニメーションが動いている間プレイヤーとタイマーを止める
    void StartAnimationTime()
    {
        DOVirtual.DelayedCall(8, () => { isStart = false; timer = Time.time; });
    }
}
