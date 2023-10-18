using UnityEngine;
using DG.Tweening;
using SoundSystem;
using TMPro;
using UnityEngine.Playables;
using UniRx;

/// <summary>
/// ステージマネージャー
/// </summary>
public class StageManager : MonoBehaviour
{
    // スタート時のアニメーションフラグ
    //bool isTimeLine = true;
    public bool IsTimeLine { get; set; }

    public bool Goal { get { return isGoal.Goal; } }
    bool stageCliar = false;

    public bool Fall { get { return isFall.IsFalling; } }
    
    // タイム
    float tm = 0;
    float timer;
    bool isTimer = false;

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
    [SerializeField, Header("タイムライン")]
    GameObject StartTimeLine;
    [SerializeField, Header("タイムラインアニメーション")]
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
            // 選択されたステージを生成する
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

        // マウスカーソルを非表示
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

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
        if(isTimer)
        {
            tm = Time.time - timer;
            TaimeText.text = Mathf.Floor(tm).ToString();
        }
    }

    // クリアした際に呼ばれる
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

    // スタートアニメーションが動いている間プレイヤーとタイマーを止める
    void StartAnimationTime()
    {
        StartTimeLine.transform.position = new Vector3(player.transform.position.x, player.transform.position.y+0.12f, player.transform.position.z);
        DOVirtual.DelayedCall(6f, () => {  timer = Time.time; isTimer = true; });
    }

    // スタートアニメーションが動いている間プレイヤーを止める
    void ReStartAnimationTime()
    {
        //isStart = true;
        StartTimeLine.transform.position = new Vector3(isFall.ResetPosition.x, isFall.ResetPosition.y + 0.15f, isFall.ResetPosition.z);
        playableDirector.Play();
        //DOVirtual.DelayedCall(7.5f, () => { isStart = false;});
    }
}
