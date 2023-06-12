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
    string rank = "";

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
            // 選択されたステージを生成する
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

        // セーブデータ呼び出し
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
            ClearRank();
            cd.SaveClearData(Mathf.Floor(tm), rank, stageNum);
            FadeManager.Instance.LoadScene("Result", 1.5f);
        }
    }

    // クリアランクを設定する
    void ClearRank()
    {
        if(tm <= 30) { rank = "S"; }
        else if(tm <= 50) { rank = "A"; }
        else if(tm <= 180) { rank = "B"; }
        else { rank = "C"; }
    }

    // スタートアニメーションが動いている間プレイヤーとタイマーを止める
    void StartAnimationTime()
    {
        DOVirtual.DelayedCall(8, () => { isStart = false; timer = Time.time; });
    }
}
