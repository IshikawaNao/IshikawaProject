using UnityEngine;

public class FadeController : MonoBehaviour
{
    [SerializeField]
    Material mat;

    float fadeNum = 0;
    const float AddFadeNum = 0.00001f;
    const float MinFadeNum = 0;
    const float MaxFadeNum = 0.5f;

    public void FadeOpen()
    {
        if(fadeNum != MaxFadeNum)
        {
            for (float i = MinFadeNum; i < MaxFadeNum; i += AddFadeNum)
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
            for (float i = MaxFadeNum; i < MinFadeNum; i -= AddFadeNum)
            {
                mat.SetFloat("_Float", i);
            }
            fadeNum = mat.GetFloat("_Float");
        }
    }
}
