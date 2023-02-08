using UnityEngine;
using UnityEngine.UI;

public class ScreenSizeSet : MonoBehaviour
{
    [SerializeField]
    Toggle toggle;

    // ウィンドウサイズ
    const int windowWidth = 1280;
    const int windowHeight = 720;
    const int fullWidth = 1920;
    const int fullHeight = 1080;

    // スクリーンサイズを切り替える
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

    // スクリーンサイズ切り替え
    void SetScreenMode(bool fullScreen)
    {
        if(!fullScreen)
        {
            Screen.SetResolution(windowWidth, windowHeight, fullScreen);
        }
        else
        {
            Screen.SetResolution(fullWidth, fullHeight, fullScreen);
        }
        
    }

}
