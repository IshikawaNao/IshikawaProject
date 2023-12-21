using UnityEngine;
using UniRx;
using DG.Tweening;

public class SonarVisibleTouch : MonoBehaviour
{
    [SerializeField]
    Material mat;
    [SerializeField, Header("当たり判定")]
    Collider col;
    [SerializeField, Header("オブジェクトの起動確認")]
    SonarActivationGimmick sonarActivation;
    // 透明か否か
    bool isInvisible = true;
    public bool IsInvisible{ get{ return isInvisible; }}
    float intensity = 4f;
    const float MaxIntensity = 30;
    const float WaiteTime = 0.5f;

    private void Start()
    {
        // 起動オブジェクトの起動を監視
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

    // アニメーション
    void VisibleAnimation()
    {
        DOTween.Sequence()
            .Append(DOTween.To(() => intensity, (x) => intensity = x, MaxIntensity, WaiteTime))
            .OnUpdate(() => mat.SetFloat("_intensity", intensity));
        
    }
}
