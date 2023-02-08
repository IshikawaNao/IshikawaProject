using UnityEngine;
using UnityEngine.UI;

public class ScreenSizeSet : MonoBehaviour
{
    [SerializeField]
    Toggle toggle;

    // �E�B���h�E�T�C�Y
    const int windowWidth = 1280;
    const int windowHeight = 720;
    const int fullWidth = 1920;
    const int fullHeight = 1080;

    // �X�N���[���T�C�Y��؂�ւ���
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

    // �X�N���[���T�C�Y�؂�ւ�
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
