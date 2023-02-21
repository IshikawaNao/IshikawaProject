using UnityEngine;
using DG.Tweening;

public class SonarObjectEffect : MonoBehaviour
{
    [SerializeField]
    SonarEffect sonarEffect;

    [SerializeField]
    Material[] mat;

    // 1�Ȃ�\�i�[�I��0�Ȃ�I�t
    const int sonarOn = 1;
    const int sonarOff = 0;

    const float waiteTime = 1f;

    float intensity = 4;
    const int maxIntensity = 30;

    private void Update()
    {
        ObjectEffect(sonarEffect.IsOnSonar);
    }

    // �\�i�[�̍ۂ̃I�u�W�F�N�g�̃G�t�F�N�g�؂�ւ�
    public void ObjectEffect(bool isSonar)
    {
        if (isSonar)
        {
            
            for (int i = 0; i < mat.Length; i++)
            {
                mat[i].SetFloat("_Boolean", sonarOn);
                mat[i].SetFloat("_intensity", intensity);
            }
        }
        else if(!isSonar)
        {
            for (int i = 0; i < mat.Length; i++)
            {
                mat[i].SetFloat("_Boolean", sonarOff);
            }
        }
    }

    // ���ʉ��Z
    public void SonarIntensity()
    {
        DOTween.To
       (
           () => intensity,
           (x) => intensity = x,
           maxIntensity,
           waiteTime
       )
       .SetLoops(2, LoopType.Yoyo);
    }
}
