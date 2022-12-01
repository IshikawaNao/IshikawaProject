using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SoundSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class TitleManager : MonoBehaviour
{
    // �I�𐔎�
    public int num { get; set; } = 0;
    

    // �I���f�B���C�̂��߂̃t���O
    bool isDelay = true;

    // �I���C���[�W
    [SerializeField]
    Animator anim; 


    // UI�p�l��
    [SerializeField]
    GameObject[] uiPanel;


    [SerializeField] VolumeConfigUI volumeConfigUI;

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
        // �X���C�_�[�̐��l���f
        volumeConfigUI.SetMasterVolume(SoundManager.Instance.MasterVolume);
        volumeConfigUI.SetBGMVolume(SoundManager.Instance.BGMVolume);
        volumeConfigUI.SetSeVolume(SoundManager.Instance.SEVolume);

        // �{�����[���̐ݒ�
        volumeConfigUI.SetMasterSliderEvent(vol => SoundManager.Instance.MasterVolume = vol);
        volumeConfigUI.SetBGMSliderEvent(vol => SoundManager.Instance.BGMVolume = vol);
        volumeConfigUI.SetSeSliderEvent(vol => SoundManager.Instance.SEVolume = vol);

        title = new TitleSelect();
        num = 0;
    }

    void Update()
    {
        print(num);
        //���͂�vector2�ł����ė���
        Vector2 selectvalue = myInput.Player.Move.ReadValue<Vector2>();

        // �I���p�l���\����
        if (uiPanel[1].activeSelf == true)
        {
            if (num == 2) { num = 3; }

            // �A�j���[�V�������Đ�����Ă���Ԃ��̏����ɓ���Ȃ��悤�ɂ���
            if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "TitleStart" && myInput.Player.Move.WasPressedThisFrame())
            {
                //�@�I���p�l���\�����I��ԍ��擾
                num = title.QuitNum(selectvalue.x);
            }
            
            isDelay = false;

            if (myInput.UI.Decision.WasPressedThisFrame())
            {
                // �I������
                title.QuitDecision(num, anim);
                num = 2;
                //���̏����܂ł̃f�B���C
                Invoke("Delay", 0.5f);
            }

            // �߂錈�莞
            if (myInput.UI.Return.WasPressedThisFrame())
            {
                anim.SetTrigger("PanelEnd");
 
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
                anim.SetTrigger("PanelEnd");
                //���̏����܂ł̃f�B���C
                Invoke("Delay", 0.5f);
            }
        }
        else
        {
            //�@���菈��
            if (myInput.UI.Decision.WasPressedThisFrame())
            {
                title.SelecDecision(num, anim);
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
                Invoke("Delay", 0.5f);
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

            if (num < 0) { num = 0; }
            else if (num > 2) { num = 2; }
        }
       
    }
    // �I���f�B���C
    void Delay()
    {
        isDelay = true;
    }
}
