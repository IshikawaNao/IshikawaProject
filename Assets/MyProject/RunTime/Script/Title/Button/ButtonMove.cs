using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using SoundSystem;

public class ButtonMove
{
    const float waitTime = 0.5f;
    const int minNum = 0;
    const int distanceX = -15;

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
            SoundManager.Instance.PlayOneShotSe("select");
            selectDelyTime = false;

            img[num].DOColor(Color.white, waitTime).OnComplete(() =>
            {
                selectDelyTime = true;
            });
        }
    }

    public void SelectUIMove(GameObject obj, float moveVal, float input, Material[] mat, int num)
    {
        if(selectDelyTime)
        {
            if (input < 0)
            {
                selectDelyTime = false;
                obj.transform.DOLocalMove(new Vector3(Mathf.Clamp(obj.transform.localPosition.x - moveVal , 0, distanceX),
                    obj.transform.localPosition.y , obj.transform.localPosition.z), waitTime).OnComplete(CallbackFunction);
            }
            else if (input > 0)
            {
                selectDelyTime = false;
                obj.transform.DOLocalMove(new Vector3(Mathf.Clamp(obj.transform.localPosition.x + moveVal, 0, distanceX),
                   obj.transform.localPosition.y, obj.transform.localPosition.z), waitTime).OnComplete(CallbackFunction);
            }
            mat[num].SetFloat("_Boolean", 1);
        }
    }

    void CallbackFunction()
    {
        selectDelyTime = true;
    }
}
