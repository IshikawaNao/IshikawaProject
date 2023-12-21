using DG.Tweening;
using UnityEngine;

public class SonarExtension : MonoBehaviour
{
    [SerializeField, Header("�\�i�[�͈̓G�t�F�N�g")]
    GameObject sonarEffect;
    
    Material mat;
    GameObject sonarObject; 

    const string MaterialReference = "_Intensity";
    const string SonarTagName = "Sonar";
    const float MaxIntensityValue = 4f;
    const float TweenTime = 0.5f;
    const float WaiteTime = 1.5f;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains(SonarTagName))
        {
            // �\�i�[���G�ꂽ�ۂ̃A�j���[�V����
            DOTween.To
                (
                    () => mat.GetFloat(MaterialReference),
                    (x) => mat.SetFloat(MaterialReference, x),
                    MaxIntensityValue,
                    TweenTime
                );

            if(sonarObject == null ) 
            {
                // �\�i�[�𔭐�������
                sonarObject = Instantiate(sonarEffect, this.transform.position, this.transform.rotation, this.transform);
                sonarObject.transform.DOScale(new Vector3(3, -1, 3), WaiteTime);
            }
        }
    }
}
