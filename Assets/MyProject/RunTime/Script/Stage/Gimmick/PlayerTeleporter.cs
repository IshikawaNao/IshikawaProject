using UnityEngine;
using DG.Tweening;
using UniRx;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField, Header("テレポートエフェクト")]
    GameObject effect;
    [SerializeField, Header("テレポート先のテレポーター")] 
    GameObject teleporter;
    [SerializeField, Header("オブジェクトの起動確認")]
    SonarActivationGimmick sonarActivation;

    const float DelayTime = 1.5f;

    // テレポート出来るか否か
    bool activated = false;
    public bool Activated { get { return activated; } }

    private void Start()
    {
        // 起動オブジェクトの起動を監視
        sonarActivation.Activated
            .Where(x => x)
            .Subscribe(x =>
            {
                activated = x;
                effect.SetActive(true);
            }).AddTo(this);
    }

    public void Teleport(GameObject obj)
    {
        DOVirtual.DelayedCall(DelayTime, () => obj.transform.position = teleporter.transform.position);
    }
}
