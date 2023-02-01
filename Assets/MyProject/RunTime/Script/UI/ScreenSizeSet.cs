using UnityEngine;
using UnityEngine.UI;

public class ScreenSizeSet : MonoBehaviour
{
    [SerializeField]
    Toggle toggle;

    int width = 1280;
    int height = 720;

    bool isFullScreen;
    void Start()
    {
        CreateData.Instance.LoadScreenSize(ref isFullScreen);
        toggle.isOn = isFullScreen;
        SetScreenMode(isFullScreen);
    }

    public void SetScreen()
    {
        SetScreenMode(toggle.isOn);
    }


    void SetScreenMode(bool fullScreen)
    {
        if(!fullScreen)
        {
            Screen.SetResolution(width, height, fullScreen);
        }
        else
        {
            Screen.SetResolution(1920, 1080, fullScreen);
        }
        
    }

}
