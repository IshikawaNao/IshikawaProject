using UnityEngine;

public class FadeController : MonoBehaviour
{
    [SerializeField]
    Material mat;

    float fadeNum = 0;
    const float addFadeNum = 0.00001f;
    const float minFadeNum = 0;
    const float maxFadeNum = 0.5f;

    public void FadeOpen()
    {
        if(fadeNum != maxFadeNum)
        {
            for (float i = minFadeNum; i < maxFadeNum; i += addFadeNum)
            {
                mat.SetFloat("_Float", i);
            }
            fadeNum = mat.GetFloat("_Float");
        }
       
    }

    public void FadeClose()
    {
        if (fadeNum != 0)
        {
            for (float i = maxFadeNum; i < minFadeNum; i -= addFadeNum)
            {
                mat.SetFloat("_Float", i);
            }
            fadeNum = mat.GetFloat("_Float");
        }
    }
}
