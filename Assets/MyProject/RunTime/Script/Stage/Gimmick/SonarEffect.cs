using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using DG.Tweening;

public class SonarEffect : MonoBehaviour
{
    [SerializeField, Header("Volume")]
    Volume postVol;
    [SerializeField, Header("�\�i�[�͈̓G�t�F�N�g")]
    GameObject sonarEffect;
    [SerializeField, Header("�e���M�~�b�N��Material")]
    Material[] gimmickMaterial;

    LiftGammaGain liftGammaGain;
    ChromaticAberration chromaticAberration;
    SonarObjectEffect objEffect;

    // �\�i�[�g�p��
    bool isOnSonar = false;
    public bool IsOnSonar { get { return isOnSonar; } }

    // �K���}�l
    float gammmaValue = 0;
    const float MinGammma = -0.4f;
    const float WaiteTime = 1.5f;

    // �\�i�[
    // ��
    float thicknessValue = 0;
    const string Thickness = "_Thickness";
    const float MinThickness = 0f;
    const float MaxThickness = 0.5f;
    // ����
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

        // �I�u�W�F�N�g�̃\�i�[�؂�ւ�
        objEffect.SonarIntensity(WaiteTime);

        // �\�i�[�𔭐�������
        var sonar = Instantiate(sonarEffect,this.transform.position,this.transform.rotation,this.transform);
        var mat = sonar.GetComponent<Renderer>().material;
        mat.SetFloat(Intensity, MaxIntensity);
        thicknessValue = MaxThickness;
        // �\�i�[�A�j���[�V����
        DOTween.Sequence()
            .Append(sonar.transform.DOScale(new Vector3(1, 1, 1), WaiteTime))
            .Append(DOTween.To(() => thicknessValue, (x) => thicknessValue = x, MinThickness,WaiteTime))
            .OnUpdate(() => mat.SetFloat(Thickness, thicknessValue))
            .OnComplete(() => Destroy(sonar));

        // gammma�̉��Z���Z
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
                //gammma�l�ύX
                gammmaVal.w = gammmaValue;
                liftGammaGain.gamma.value = gammmaVal;
            })
            .OnStart(() =>{ isOnSonar = true;})
            .OnComplete(() =>{ isOnSonar = false;  });
    }
}
