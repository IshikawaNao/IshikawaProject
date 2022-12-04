using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectManager : MonoBehaviour
{
    // ステージナンバー
    public int sutageNum { get; set; } = 0;
    [SerializeField]
    GameObject stage;

    Animator anim;
 
    ISelectManager select;

    #region　InputAction
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
            //入力
            Vector2 selectvalue = myInput.Player.Move.ReadValue<Vector2>();

            // key入力orLスティック操作時
            if (myInput.Player.Move.WasPressedThisFrame())
            {
                // 選択番号取得
                sutageNum = select.SutageNum(selectvalue.y, sutageNum);
            }

            //　決定処理
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
