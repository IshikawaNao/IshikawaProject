using UnityEngine.UI;
using TMPro;
using UnityEngine;

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
            switch (systemMenuNum)
            {
                case 0:
                    screenToggle.isOn = !screenToggle.isOn;
                    screenSizeSet.SetScreen();
                    break;
                case 1:
                    
                    break;
                case 2:
                    VolSave();
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

    public void VolSave()
    {
        cd.SensitivitySave(cameraSlider.value);
        Invoke("Dylay",0.2f);
        om.panelCover.color = new Color(.5f, 0, 0, 1);
    }

    void Dylay()
    {
        om.IsPanelSelect = true;
    }
}
