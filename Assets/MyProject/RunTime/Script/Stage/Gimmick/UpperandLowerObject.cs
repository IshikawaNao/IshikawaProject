using DG.Tweening;
using UnityEngine;

public class UpperandLowerObject : MonoBehaviour
{
    [SerializeField]
    HouseRoom hr;

    [SerializeField]
    bool upflag;
    [SerializeField]
    float upMax = 10;
    const int downMin = 1;

    const float waiteTime = 5;

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
        if(hr.Placed || upflag)
        {
            transform.DOMove(
                new Vector3(pos.x,pos.y + upMax,pos.z),
                waiteTime
                );
        }
        else if(!hr.Placed || !upflag)
        {
            transform.DOMove(
                pos,
                waiteTime
                );
        }
    }
}
