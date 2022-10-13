/// <summary>
/// Key入力
/// </summary>
using UnityEngine;
using UnityEngine.InputSystem;
public class InputKey : MonoBehaviour
{
    // 移送入力
    Vector2 move;
    public Vector2 Move { get { return move; } }
    // マウスポジション
    Vector2 cameraPos;
    public Vector2 Camera { get { return cameraPos; } }
    // ジャンプ入力
    bool isJump;
    public bool Jump { get { return isJump; } }

    #region　InputAction
    MyInput myInput;
    void Awake() => myInput = new MyInput();
    void OnEnable() => myInput.Enable();
    void OnDisable() => myInput.Disable();
    void OnDestroy() => myInput.Dispose();
    #endregion
    
    void Update()
    {
        move = myInput.Player.Move.ReadValue<Vector2>();
        cameraPos = myInput.Camera.Move.ReadValue<Vector2>();
        isJump = myInput.Player.Jump.triggered;

    }
}
