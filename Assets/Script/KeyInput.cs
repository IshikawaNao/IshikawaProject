/// <summary>
/// Key入力
/// </summary>
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyInput : MonoBehaviour
{
    bool isJump;
    bool pushAction;
    Vector2 move;
    Vector2 pos;

    // 移送入力
    public Vector2 InputMove() { return move; }
    // マウスポジション
    public Vector2 CameraPos() { return pos; }
    // ジャンプ入力
    public bool InputJump() { return isJump; }
    // アクションボタン入力
    public bool InputAction() { return pushAction; }

    #region　InputAction
    MyInput myInput;
    void Awake() => myInput = new MyInput();
    void OnEnable() => myInput.Enable();
    void OnDisable() => myInput.Disable();
    void OnDestroy() => myInput.Dispose();
    #endregion

    private void Update()
    {
        move = myInput.Player.Move.ReadValue<Vector2>();
        pos = myInput.Camera.Move.ReadValue<Vector2>();
        isJump = myInput.Player.Jump.triggered;
        pushAction = myInput.Player.Action.triggered;
    }

   
    
}
