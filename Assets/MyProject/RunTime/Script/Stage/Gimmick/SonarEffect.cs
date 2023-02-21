using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using DG.Tweening;

public class SonarEffect : MonoBehaviour
{
    // �\�i�[�g�p��
    bool isOnSonar = false;
    public bool IsOnSonar { get { return isOnSonar; } }

    // �K���}�l
    float gammmaValue = 0;
    const float minGammma = -0.4f;

    float intensityVal = 0;

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
            //gammma�l��intensity��ύX
            gammmaVal.w = gammmaValue;
            liftGammaGain.gamma.value = gammmaVal;
            chromaticAberration.intensity.value = intensityVal;
            sonarTime = Time.time;
            mat.SetFloat("_SonarTime", sonarTime);
        })
        .OnStart(() =>{ isOnSonar = true;})
        .OnComplete(() =>{ 
            isOnSonar = false; 
            Destroy(sonarObj);
            sonarTime = 0;
            mat.SetFloat("_SonarTime", 0);
        });
        
        // �I�u�W�F�N�g�̃\�i�[�؂�ւ�
        //sonarObject.ObjectEffect(isOnSonar);
        sonarObject.SonarIntensity();
    }

    public void SonarWaveGeneration(Vector3 pos)
    {    
        GameObject obj = (GameObject)Resources.Load("SonarWave");
        sonarObj = Instantiate(obj, pos, Quaternion.identity);
    }
}
