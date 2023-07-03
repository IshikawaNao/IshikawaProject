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
        SaveDataManager.Instance.ScreenSize2Save(fullScreen);
    }

}
