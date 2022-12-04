using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectManager : MonoBehaviour
{
    // �X�e�[�W�i���o�[
    public int sutageNum { get; set; } = 0;
    [SerializeField]
    GameObject stage;

    Animator anim;
 
    ISelectManager select;

    #region�@InputAction
    MyInput myInput;
    void Awake() => myInput = new MyInput();
    void OnEnable() => myInput.Enable();
    void OnDisable() => myInput.Disable();
    void OnDestroy() => myInput.Dispose();
    #endregion

    void Start()
    {
        select = new StageSelect();
    }

    
    void Update()
    {
        if(stage.activeSelf == true)
        {
            //����
            Vector2 selectvalue = myInput.Player.Move.ReadValue<Vector2>();

            // key����orL�X�e�B�b�N���쎞
            if (myInput.Player.Move.WasPressedThisFrame())
            {
                // �I��ԍ��擾
                sutageNum = select.SutageNum(selectvalue.y, sutageNum);
            }

            //�@���菈��
            if (myInput.UI.Decision.WasPressedThisFrame())
            {
                select.StageDecision(sutageNum, anim);
            }

            if (myInput.UI.Return.WasPressedThisFrame())
            {

            }
        }
    }
}
