using UnityEngine.UI;
using TMPro;
using UnityEngine;
using SoundSystem;

public class SystemUIManager : MonoBehaviour
{
    // 選択番号
    int systemMenuNum = 0;
    const int minNum = 0;
    const int maxNum = 2;

    // sliderの加減数
    const float volAddition = 0.005f;
    // デッドゾーン
    const float deadZone = 0.3f;

    float sen = 0f;

    ButtonMove bm;
    UiAddition ua;
    KeyInput input;
    CreateData cd;

    [SerializeField]
    OptionUIManager om;
    [SerializeField]
    ScreenSizeSet screenSizeSet;
    [SerializeField]
    Toggle screenToggle;
    [SerializeField]
    Image[] button;
    [SerializeField]
    Slider cameraSlider;

    void Start()
    {
        bm = new ButtonMove();
        ua = new UiAddition();
        input = KeyInput.Instance;
        cd = CreateData.Instance;
        cd.LoadSensitivity(ref sen);
        cameraSlider.value = sen;

        button[0].color = Color.white;
        button[1].color = Color.blue;
        button[2].color = Color.blue;
    }

    void Update()
    {
        SystemPanelControll();
        ButtonDecision();
    }

    void SystemPanelControll()
    {
        if(input.PressedMove && bm.SelectDelyTime() && !om.IsPanelSelect && om.IsSystemOpen)
        {
            systemMenuNum = ua.Addition(systemMenuNum, minNum, maxNum, input.InputMove.y);
            bm.SelectTextMove(button, systemMenuNum, maxNum);
            CameraMoveVol();
        }
        else if (input.LongPressedMove && bm.SelectDelyTime() && !om.IsPanelSelect && om.IsSystemOpen)
        {
            systemMenuNum = ua.Addition(systemMenuNum, minNum, maxNum, input.InputMove.y);
            bm.SelectTextMove(button, systemMenuNum, maxNum);
            CameraMoveVol();
        }
        else if (om.IsPanelSelect)
        {
            button[0].color = Color.white;
            button[1].color = Color.blue;
            button[2].color = Color.blue;
        }
    }

    // 決定
    void ButtonDecision()
    {
        if (input.DecisionInput && !om.IsPanelSelect && om.IsSystemOpen)
        {
            SoundManager.Instance.PlayOneShotSe("decision");
            switch (systemMenuNum)
            {
                case 0:
                    screenToggle.isOn = !screenToggle.isOn;
                    screenSizeSet.SetScreen();
                    break;
                case 1:
                    
                    break;
                case 2:
                    om.Save();
                    break;
            }

        }
    }

    void CameraMoveVol()
    {
        if(systemMenuNum == 1 && input.InputMove.x > deadZone)
        {
            cameraSlider.value += volAddition;
        }
        else if (systemMenuNum == 1 && input.InputMove.x < -deadZone) 
        {
            cameraSlider.value -= volAddition;
        }
    }
}
