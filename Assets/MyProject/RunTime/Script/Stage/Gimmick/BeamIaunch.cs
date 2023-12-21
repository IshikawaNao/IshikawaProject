using DG.Tweening;
using SoundSystem;
using UnityEngine;

public class BeamIaunch : MonoBehaviour
{
    [SerializeField, Header("ビームエフェクト")]
    GameObject beam;
    [SerializeField, Header("発射部分")]
    GameObject head;

    float rotateValue = 0f;
    bool isRotate = true;

    const string SonarTagName = "Sonar";
    const int AddRotateValue = 90;
    const float ArrivalTime = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains(SonarTagName))
        {
            beam.SetActive(true);
            //SoundManager.Instance.PlayOneShotSe((int)SEList.Beam);
        }
    }

    /// <summary> 発射台回転 </summary>
    public void BeamHeadRotate()
    {
        if(isRotate) 
        {
            isRotate = false;
            rotateValue += AddRotateValue;
            head.transform.DOLocalRotate(new Vector3(0, rotateValue, 0), ArrivalTime, RotateMode.Fast)
                .OnComplete(() => isRotate = true);
            
        }
        
    }
}
