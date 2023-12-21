using DG.Tweening;
using SoundSystem;
using UnityEngine;

public class UpperandLowerObject : MonoBehaviour
{
    [SerializeField,Header("起動オブジェクト")]
    DetectionObject hr;
    [SerializeField, Header("到達位置")]
    float upMax = 10;

    Tween tween;
    bool isAbove;
    // 到達時間
    const float ArrivalTime = 8;
    // 起動するまでの時間
    const float DelayTime = 2;

    Vector3 pos;

    private void Start()
    {
        pos = this.transform.position;   
    }

    void Update()
    {
        StartUpDown();
    }

    void StartUpDown()
    {
        if(hr.Placed && !isAbove)
        {
            isAbove = true;
            tween =  transform.DOMove(
                new Vector3(pos.x,pos.y + upMax,pos.z),
                ArrivalTime
                )
                .SetDelay(DelayTime)
                .OnComplete(() => isAbove = false);
            SoundManager.Instance.PlayOneShotSe((int)SEList.Up);
        }
        else if(!hr.Placed)
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
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.transform.SetParent(null);
    }
}
