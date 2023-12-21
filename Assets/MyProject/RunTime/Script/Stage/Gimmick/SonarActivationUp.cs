using DG.Tweening;
using UnityEngine;
using UniRx;
using SoundSystem;

public class UpperandLowerObject1 : MonoBehaviour
{
    [SerializeField,Header("�N���I�u�W�F�N�g")]
    SonarActivationGimmick sonarActivation;
    [SerializeField, Header("���B�ʒu")]
    float upMax = 10;

    Tween tween;
    Vector3 pos;
    // ���B����
    const float ArrivalTime = 8;
    // �N������܂ł̎���
    const float DelayTime = 2;

    const string PlayerTag = "Player";
    const string MoveTag = "Move";


    private void Start()
    {
        pos = this.transform.position;
        // �N���I�u�W�F�N�g�̋N�����Ď�
        sonarActivation.Activated
            .Subscribe(x =>
            {
                StartUpDown(x);
            }).AddTo(this);
    }

    void StartUpDown(bool x)
    {
        if(x)
        {
            tween = transform.DOMove(
               new Vector3(pos.x, pos.y + upMax, pos.z),
               ArrivalTime
               )
               .SetDelay(DelayTime);
            SoundManager.Instance.PlayOneShotSe((int)SEList.Up);
        }
        else
        {
            tween = transform.DOMove(
                pos,
                ArrivalTime
                )
                .SetDelay(DelayTime);
        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == PlayerTag || collision.gameObject.tag == MoveTag)
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.transform.SetParent(null);
    }
}
