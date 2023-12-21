using UnityEngine;
using DG.Tweening;
using SoundSystem;
using TMPro;
using UnityEngine.Playables;
using UniRx;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// ステージマネージャー
/// </summary>
public class StageManager : MonoBehaviour
{
    [SerializeField, Header("プレイヤー")]
    PlayerController playerCon;
    [SerializeField, Header("タイムライン")]
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

    // スタート時のアニメーションフラグ
    bool isTimeLine = true;
    public bool IsTimeLine { get{ return isTimeLine; } }

    bool stageCliar = false;
    bool falling;
    public bool Fall { get { return falling; } }

    // タイム
    float stageTime = 0;
    public float StageTime { get { return stageTime; } }  
    float timer;
    bool isTimer = false;

    // ステージ情報
    int stageNum;
    public int StageNum { get { return stageNum; } }

    //　サウンドフェード
    const float SoundFadeTime = 1f;
    const float SceneLoadFadeTime = 1.5f;

    // タイムライン秒数
    const float TimeLinePlaybackTime = 6f;
    const float TimeLineTeleportPlaybackTime = 3f;

    // StagePass
    private const string Stage0InstancePas = "Assets/MyProject/RunTime/AssetData/Stage0.prefab";
    private const string Stage1InstancePas = "Assets/MyProject/RunTime/AssetData/Stage1.prefab";

    async void Awake()
    {
        var pas = "";

        // 生成するステージの選択
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
        // .Taskでインスタンス化完了までawaitできる
        await handle.Task;
    }

    async void Start()
    {
        await handle.Task;

        input = KeyInput.Instance;
        saveData = SaveDataManager.Instance;
        saveData.Load();

        // マウスカーソルを非表示
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
    
    // タイマー処理
    void TimeMeasurement()
    {
        if(isTimer)
        {
            stageTime = Time.time - timer;
            mainUIManager.TimeUpdate(stageTime);
        }
    }

    // クリアした際に呼ばれる
    void Cliar()
    {
        // ステージが生成されていないなら返す
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

    // スタートアニメーションが動いている間プレイヤーとタイマーを止める
    void StartAnimationTime()
    {
        isTimeLine = false;
        DOVirtual.DelayedCall(TimeLinePlaybackTime, () => { isTimeLine = true; timer = Time.time; isTimer = true; });
    }

    // 落下時
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
