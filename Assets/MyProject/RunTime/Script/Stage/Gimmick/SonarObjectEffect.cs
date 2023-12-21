using UnityEngine;
using DG.Tweening;

public class SonarObjectEffect 
{
    // 1ならソナーオン0ならオフ
    const int SonarOn = 1;
    const int SonarOff = 0;

    float intensity = 4;
    const int MaxIntensity = 30;


    // ソナーの際のオブジェクトのエフェクト切り替え
    public void ObjectEffect(bool isSonar, Material[] mat)
    {
        if (isSonar)
        {
            for (int i = 0; i < mat.Length; i++)
            {
                mat[i].SetFloat("_Boolean", SonarOn);
                mat[i].SetFloat("_intensity", intensity);
            }
        }
        else if(!isSonar)
        {
            for (int i = 0; i < mat.Length; i++)
            {
                mat[i].SetFloat("_Boolean", SonarOff);
            }
        }
    }

    // 光量加算
    public void SonarIntensity(float waiteTime)
    {
        DOTween.To
       (
           () => intensity,
           (x) => intensity = x,
           MaxIntensity,
           waiteTime
       )
       .SetLoops(2, LoopType.Yoyo);
    }
}
