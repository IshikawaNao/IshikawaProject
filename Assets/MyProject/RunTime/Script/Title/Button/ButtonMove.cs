using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonMove
{
    const float waitTime = 0.5f;
    const int minNum = 0;

    bool selectDelyTime = true;
    public bool SelectDelyTime() { return selectDelyTime; }

    public void SelectTextMove(Image[] img, int num, int maxNum)
    {
        if (img[num].color == Color.blue)
        {
            for (int i = minNum; i <= maxNum; i++)
            {
                img[i].color = Color.blue;
            }

            selectDelyTime = false;

            img[num].DOColor(Color.white, waitTime).OnComplete(() =>
            {
                selectDelyTime = true;
            });
        }
    }

    public void SelectUIMove(GameObject obj, float moveVal, float input)
    {
        if(selectDelyTime)
        {
            if (input > 0)
            {
                selectDelyTime = false;
                obj.transform.DOLocalMove(new Vector3(obj.transform.localPosition.x,
                    Mathf.Clamp(obj.transform.localPosition.y - moveVal, 0, 700), obj.transform.localPosition.z), waitTime).OnComplete(CallbackFunction);
            }
            else if (input < 0)
            {
                selectDelyTime = false;
                obj.transform.DOLocalMove(new Vector3(obj.transform.localPosition.x,
                    Mathf.Clamp(obj.transform.localPosition.y + moveVal, 0, 700), obj.transform.localPosition.z), waitTime).OnComplete(CallbackFunction);
            }
        }
    }

    void CallbackFunction()
    {
        selectDelyTime = true;
    }
}
