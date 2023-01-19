using UnityEngine;
using UnityEngine.UI;

public class ScreenSizeSet : MonoBehaviour
{
    [SerializeField]
    Toggle toggle;

    int width = 1280;
    int height = 720;

    void Start()
    {
        SetToggle(); 
    }

    public void SetScreen()
    {
        SetScreenMode(toggle.isOn);
    }

    void SetToggle()
    {
        toggle.isOn = Screen.fullScreen;
        toggle.onValueChanged.AddListener((x) => SetScreenMode(x));
    }
    void SetScreenMode(bool isFullScreen)
    {
        Screen.SetResolution(width, height,isFullScreen);
    }

}
