using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using DG.Tweening;

public class SonarEffect : MonoBehaviour
{
    // ソナー使用中
    bool isOnSonar = false;
    public bool IsOnSonar { get { return isOnSonar; } }

    // ガンマ値
    float gammmaValue = 0;
    const float minGammma = -0.4f;

    float intensityVal = 0.5f;

    const float waiteTime = 1f;

    float sonarTime;

    Vector4 gammmaVal = Vector4.one;

    [SerializeField, Header("Volume")]
    Volume postVol;

    [SerializeField]
    SonarObjectEffect sonarObject;

    LiftGammaGain liftGammaGain;
    ChromaticAberration chromaticAberration;

    [SerializeField]
    Material mat;
    GameObject sonarObj;

    void Start()
    {
        postVol.profile.TryGet(out liftGammaGain);
        postVol.profile.TryGet(out chromaticAberration);
        
    }

    public void Sonar()
    {
        // gammmaの加算減算
        DOTween.To
        (
            () => gammmaValue,
            (x) => gammmaValue = x,
            minGammma,
            waiteTime
        )
        .SetLoops(2, LoopType.Yoyo)
        .OnUpdate(() =>
        {
            //gammma値変更
            gammmaVal.w = gammmaValue;
            liftGammaGain.gamma.value = gammmaVal;
                 
        })
        .OnStart(() =>{ isOnSonar = true;})
        .OnComplete(() =>{ isOnSonar = false;  });

        // ソナーウェーブの発生時間と半径の加算
        DOTween.To(
            () => sonarTime,
            (x) => sonarTime = x,
            1,
            1.5f
           ).OnUpdate(() => { mat.SetFloat("_SonarTime", sonarTime); })
           .OnComplete(() =>
           {
               sonarTime = 0;
               mat.SetFloat("_SonarTime", sonarTime);
               Destroy(sonarObj);
           });

           // オブジェクトのソナー切り替え
           sonarObject.SonarIntensity();
    }

    public void SonarWaveGeneration(Vector3 pos)
    {    
        GameObject obj = (GameObject)Resources.Load("SonarWave");
        sonarObj = Instantiate(obj, pos, Quaternion.identity);
    }
}
