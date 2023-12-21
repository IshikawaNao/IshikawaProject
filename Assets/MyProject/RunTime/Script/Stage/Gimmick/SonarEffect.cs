using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using DG.Tweening;

public class SonarEffect : MonoBehaviour
{
    [SerializeField, Header("Volume")]
    Volume postVol;
    [SerializeField, Header("ソナー範囲エフェクト")]
    GameObject sonarEffect;
    [SerializeField, Header("各くギミックのMaterial")]
    Material[] gimmickMaterial;

    LiftGammaGain liftGammaGain;
    ChromaticAberration chromaticAberration;
    SonarObjectEffect objEffect;

    // ソナー使用中
    bool isOnSonar = false;
    public bool IsOnSonar { get { return isOnSonar; } }

    // ガンマ値
    float gammmaValue = 0;
    const float MinGammma = -0.4f;
    const float WaiteTime = 1.5f;

    // ソナー
    // 幅
    float thicknessValue = 0;
    const string Thickness = "_Thickness";
    const float MinThickness = 0f;
    const float MaxThickness = 0.5f;
    // 光量
    const string Intensity = "_Intensity";
    const float MaxIntensity = 5f;

    Vector4 gammmaVal = Vector4.one;

    void Start()
    {
        postVol.profile.TryGet(out liftGammaGain);
        postVol.profile.TryGet(out chromaticAberration);
        objEffect = new SonarObjectEffect();
    }

    void Update()
    {
        objEffect.ObjectEffect(isOnSonar, gimmickMaterial);
    }

    public void Sonar()
    {
        if (isOnSonar) { return; }

        // オブジェクトのソナー切り替え
        objEffect.SonarIntensity(WaiteTime);

        // ソナーを発生させる
        var sonar = Instantiate(sonarEffect,this.transform.position,this.transform.rotation,this.transform);
        var mat = sonar.GetComponent<Renderer>().material;
        mat.SetFloat(Intensity, MaxIntensity);
        thicknessValue = MaxThickness;
        // ソナーアニメーション
        DOTween.Sequence()
            .Append(sonar.transform.DOScale(new Vector3(1, 1, 1), WaiteTime))
            .Append(DOTween.To(() => thicknessValue, (x) => thicknessValue = x, MinThickness,WaiteTime))
            .OnUpdate(() => mat.SetFloat(Thickness, thicknessValue))
            .OnComplete(() => Destroy(sonar));

        // gammmaの加算減算
        DOTween.To
            (
                () => gammmaValue,
                (x) => gammmaValue = x,
                MinGammma,
                WaiteTime
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
    }
}
