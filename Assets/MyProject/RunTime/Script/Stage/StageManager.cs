using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using SoundSystem;
using TMPro;

/// <summary>
/// ステージマネージャー
/// </summary>
public class StageManager : MonoBehaviour
{
    // ゴールフラグ
    public bool Goal { get; set; } = false;

    float intensityVal = 0;

    float gammmaValue = 0;

    float tm = 0;
    float timer;

    Vector4 gammmaVal = Vector4.one;

    const float maxGammma = 0;
    const float minGammma = -0.4f;

    const float waiteTime = 0.02f;

    // 加数
    const float gammmaAddend = 0.1f;
    const float intensityAddend = 0.25f;

    bool gammaDown = true;
    bool gammaUp = true;

    const float soundFadeTime = 1f;

    const int minOperationNum = 0; 
    const int maxOperationNum = 3;

    int stageNum;

    bool operation;
    bool isGoal = true;

    string rank = "";

    [SerializeField, Header("player")]
    PlayerController player;

    [SerializeField,Header("Volume")]
    Volume postVol;

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

    LiftGammaGain liftGammaGain;
    ChromaticAberration chromaticAberration;

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

        // スライダーの数値反映
        volumeConfigUI.SetMasterVolume(SoundManager.Instance.MasterVolume);
        volumeConfigUI.SetBGMVolume(SoundManager.Instance.BGMVolume);
        volumeConfigUI.SetSeVolume(SoundManager.Instance.SEVolume);

        // ボリュームの設定
        volumeConfigUI.SetMasterSliderEvent(vol => SoundManager.Instance.MasterVolume = vol);
        volumeConfigUI.SetBGMSliderEvent(vol => SoundManager.Instance.BGMVolume = vol);
        volumeConfigUI.SetSeSliderEvent(vol => SoundManager.Instance.SEVolume = vol);

        postVol.profile.TryGet(out liftGammaGain);
        postVol.profile.TryGet(out chromaticAberration);

        cd = CreateData.Instance;

        timer = Time.time - 6;

        SoundManager.Instance.PlayBGMWithFadeIn("Main", soundFadeTime);
    }

    void Update()
    {
        Cliar();
        SonarEffect();
        SwithingOperation();
        TimeMeasurement();
    }

    void SwithingOperation()
    {
        if(input.PadCurrent)
        {
            for(int i = minOperationNum; i <= maxOperationNum; i++)
            {
                keyOperation[i].SetActive(false);
                padOperation[i].SetActive(true);
            }
        }
        else if(!input.PadCurrent)
        {
            for (int i = minOperationNum; i <= maxOperationNum; i++)
            {
                keyOperation[i].SetActive(true);
                padOperation[i].SetActive(false);
            }
        }
    }
    
    void TimeMeasurement()
    {
        tm = Time.time - timer;
        TaimeText.text = Mathf.Floor(tm).ToString();
    }

    void Cliar()
    {
        if(Goal && isGoal)
        {
            isGoal = false;
            player.IsMove = false;
            input.InputMove = Vector2.zero;
            input.CameraPos = player.transform.forward;
            cd.SaveClearData(Mathf.Floor(tm),rank, stageNum);
            SoundManager.Instance.StopBGMWithFadeOut("Main", soundFadeTime);
            FadeManager.Instance.LoadScene("Result", 1.0f);
        }
    }

    void SonarEffect()
    {
        if(input.SonarAction && gammaDown)
        {
            gammaDown = false;
            StartCoroutine(GammaDown());

        }
        else if(gammmaValue < maxGammma && gammaUp && !input.SonarAction)
        {
            gammaUp = false;
            StartCoroutine(GammaUp());
        }

        //gammma値とintensityを変更
        gammmaVal.w = gammmaValue;
        liftGammaGain.gamma.value = gammmaVal;
        chromaticAberration.intensity.value = intensityVal;
    }

    IEnumerator GammaDown()
    {
        if (gammmaValue >= minGammma)
        {
            yield return new WaitForSeconds(waiteTime);
            gammmaValue -= gammmaAddend;
            intensityVal += intensityAddend;
            gammaDown = true;
            gammaUp = true;
        }
    }
    IEnumerator GammaUp()
    {
        yield return new WaitForSeconds(waiteTime);
        gammmaValue += gammmaAddend;
        intensityVal -= intensityAddend;
        gammaUp = true;
        gammaDown = true;
    }

    void ClearRank()
    {
        if(tm <= 30) { rank = "S"; }
        else if(tm <= 50) { rank = "A"; }
        else if(tm <= 180) { rank = "B"; }
        else { rank = "C"; }
    }
}
