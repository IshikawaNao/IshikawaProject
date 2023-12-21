using UnityEngine;
using UnityEngine.UI;

public class ScreenSizeSet : MonoBehaviour
{
    [SerializeField]
    Toggle toggle;

    // ウィンドウサイズ
    const int WindowWidth = 1280;
    const int WindowHeight = 720;
    const int FullWidth = 1920;
    const int FullHeight = 1080;

    // スクリーンサイズを切り替える
    bool isFullScreen;

    void Start()
    {
        SaveDataManager.Instance.Load();
        isFullScreen = SaveDataManager.Instance.ScreenSize;
        toggle.isOn = isFullScreen;
        SetScreenMode(isFullScreen);
    }

    public void SetScreen()
    {
        if(toggle.isOn) 
        {
            toggle.isOn = false;
        }
        else
        {
            toggle.isOn = true;
        }

        SetScreenMode(toggle.isOn);
    }

    // スクリーンサイズ切り替え
    void SetScreenMode(bool fullScreen)
    {
        if(!fullScreen)
        {
            Screen.SetResolution(WindowWidth, WindowHeight, fullScreen);
        }
        else
        {
            Screen.SetResolution(FullWidth, FullHeight, fullScreen);
        }
        SaveDataManager.Instance.ScreenSizeSave(fullScreen);
    }
}
