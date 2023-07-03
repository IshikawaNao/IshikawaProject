using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSensitivityConfigUI : MonoBehaviour
{
    [SerializeField, Header("オプションパネル")]
    GameObject optionPanel;
    [SerializeField, Header("システムパネル")]
    GameObject systemPanel;
    [SerializeField, Header("オプション")]
    OptionPresenter option;
    [SerializeField,Header("カメラ感度スライダー")]
    Slider sensitivitySlider;

    KeyInput input;

    const float addition = 0.005f;
    const float deadZone = 0.3f;


    private void Start()
    {
        input = KeyInput.Instance;
        sensitivitySlider.value = SaveDataManager.Instance.Sensitivity;
    }
}
