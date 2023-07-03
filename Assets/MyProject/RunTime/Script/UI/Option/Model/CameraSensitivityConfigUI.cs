using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSensitivityConfigUI : MonoBehaviour
{
    [SerializeField, Header("�I�v�V�����p�l��")]
    GameObject optionPanel;
    [SerializeField, Header("�V�X�e���p�l��")]
    GameObject systemPanel;
    [SerializeField, Header("�I�v�V����")]
    OptionPresenter option;
    [SerializeField,Header("�J�������x�X���C�_�[")]
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
