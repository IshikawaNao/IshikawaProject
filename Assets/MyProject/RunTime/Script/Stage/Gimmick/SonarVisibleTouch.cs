using UnityEngine;
using UniRx;
using DG.Tweening;

public class SonarVisibleTouch : MonoBehaviour
{
    [SerializeField]
    Material mat;
    [SerializeField, Header("�����蔻��")]
    Collider col;
    [SerializeField, Header("�I�u�W�F�N�g�̋N���m�F")]
    SonarActivationGimmick sonarActivation;
    // �������ۂ�
    bool isInvisible = true;
    public bool IsInvisible{ get{ return isInvisible; }}
    float intensity = 4f;
    const float MaxIntensity = 30;
    const float WaiteTime = 0.5f;

    private void Start()
    {
        // �N���I�u�W�F�N�g�̋N�����Ď�
        sonarActivation.Activated
            .Where(x => x)
            .Subscribe(x =>
            {
                VisibleAnimation();
                mat.SetFloat("_Boolean", 1);
                col.isTrigger = false;
                isInvisible = false;
            }).AddTo(this);

        mat.SetFloat("_Boolean", 0);
        col.isTrigger = true;
    }

    // �A�j���[�V����
    void VisibleAnimation()
    {
        DOTween.Sequence()
            .Append(DOTween.To(() => intensity, (x) => intensity = x, MaxIntensity, WaiteTime))
            .OnUpdate(() => mat.SetFloat("_intensity", intensity));
        
    }
}
