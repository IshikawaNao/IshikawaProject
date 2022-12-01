using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using SoundSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class TitleManager : MonoBehaviour
{
    // �I�𐔎�
    public int num { get; set; } = 0;
    

    // �I���f�B���C�̂��߂̃t���O
    bool isDelay = true;

    bool isFlag = true;
    // �I���C���[�W
    [SerializeField]
    GameObject[] img; 

    // �I���e�L�X�g
    [SerializeField]
    TextMeshProUGUI[] text;

    // UI�p�l��
    [SerializeField]
    GameObject[] uiPanel;


    //[SerializeField] VolumeConfigUI volumeConfigUI;

    ITitleSelect title;

    #region�@InputAction
    MyInput myInput;
    void Awake() => myInput = new MyInput();
    void OnEnable() => myInput.Enable();
    void OnDisable() => myInput.Disable();
    void OnDestroy() => myInput.Dispose();
    #endregion

    void Start()
    {
       /* // �X���C�_�[�̐��l���f
        volumeConfigUI.SetMasterVolume(SoundManager.Instance.MasterVolume);
        volumeConfigUI.SetBGMVolume(SoundManager.Instance.BGMVolume);
        volumeConfigUI.SetSeVolume(SoundManager.Instance.SEVolume);

        // �{�����[���̐ݒ�
        volumeConfigUI.SetMasterSliderEvent(vol => SoundManager.Instance.MasterVolume = vol);
        volumeConfigUI.SetBGMSliderEvent(vol => SoundManager.Instance.BGMVolume = vol);
        volumeConfigUI.SetSeSliderEvent(vol => SoundManager.Instance.SEVolume = vol);*/

        title = new TitleSelect();
    }

    void Update()
    {

        print(num);
        //���͂�vector2�ł����ė���
        Vector2 selectvalue = myInput.Player.Move.ReadValue<Vector2>();

        // �I���p�l���\����
        if (uiPanel[1].activeSelf == true)
        {
            //�@�I���p�l���\�����I��ԍ��擾
            num = title.QuitNum(selectvalue.x);

            isDelay = false;

            if (myInput.UI.Decision.WasPressedThisFrame())
            {
                // �I������
                title.QuitDecision(num, uiPanel);
                num = 2;
                //���̏����܂ł̃f�B���C
                Invoke("Delay", 0.5f);
            }

            // �߂錈�莞
            if (myInput.UI.Return.WasPressedThisFrame())
            {
                uiPanel[1].SetActive(false);
                //���̏����܂ł̃f�B���C
                Invoke("Delay", 0.5f);
                num = 2;
            }
        }
        // option�p�l���\����
        else if (uiPanel[0].activeSelf == true)
        {
            isDelay = false;
            // �߂錈�莞
            if (myInput.UI.Return.WasPressedThisFrame())
            {
                uiPanel[0].SetActive(false);
                //���̏����܂ł̃f�B���C
                Invoke("Delay", 0.5f);
            }
        }
        else
        {
            //�@���菈��
            if (myInput.UI.Decision.WasPressedThisFrame())
            {
                title.SelecDecision(num, uiPanel);
            }

            // key����orL�X�e�B�b�N���쎞
            if (myInput.Player.Move.WasPressedThisFrame())
            {
                // �I��ԍ��擾
                num = title.SelectNum(selectvalue.y,num);

                // �I������
                title.UISelect(num);
                isDelay = false;
                //���̏����܂ł̃f�B���C
                Invoke("Delay", 0.2f);
            }
            else if (myInput.Player.Move.IsPressed())
            {
                if (isDelay)
                {
                    // �I��ԍ��擾
                    num = title.SelectNum(selectvalue.y, num);

                    // �I������
                    title.UISelect(num);

                    isDelay = false;
                }
            }
        }
       
    }

    // �I���f�B���C
    void Delay()
    {
        isDelay = true;
    }
}
