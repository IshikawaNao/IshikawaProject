using UnityEngine;
using UniRx;

public class SonarActivationGimmick : MonoBehaviour
{
    [SerializeField, Header("作動部分のオブジェクト")]
    GameObject targetObj;
    Material mat;

    GameObject sonar;

    private readonly ReactiveProperty<bool> activated = new ReactiveProperty<bool>();
    public IReadOnlyReactiveProperty<bool> Activated => activated;

    const string MaterialReference = "_Intensity";
    const string SonarTagName = "Sonar";
    const float MaxIntensityValue = 4f;
    const float DefaultIntensityValue = 1f;

    private void Start()
    {
        mat = targetObj.GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if(sonar == null && activated.Value)
        {
            activated.Value = false;
            // materialの光量を変更
            mat.SetFloat(MaterialReference, DefaultIntensityValue);
        }
    }

    private void CheckActivation(GameObject colliderObject)
    {
        sonar = colliderObject;
        activated.Value = true;
        mat.SetFloat(MaterialReference, MaxIntensityValue);
    }

    // ソナーオブジェクトにぶつかったとき
    private void OnTriggerEnter(Collider other)
    {
        if (!activated.Value && other.gameObject.CompareTag(SonarTagName))
        {
            CheckActivation(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        activated.Value = false;
        mat.SetFloat(MaterialReference, MaxIntensityValue);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!activated.Value)
        {
            CheckActivation(other.gameObject);
        }
    }
}
