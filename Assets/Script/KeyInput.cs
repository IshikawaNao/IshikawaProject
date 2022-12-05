using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Key入力
/// </summary>
public class KeyInput : MonoBehaviour
{
    public static KeyInput Instance { get; private set; }

    // 移送入力
    public Vector2 InputMove { get; set; }
    public bool PressedMove { get; set; }   
    // マウスポジション
    public Vector2 CameraPos { get; set; }
    // ジャンプ入力
    public bool InputJump { get; set; }
    // アクションボタン入力
    public bool PushAction { get; set; }
    // アクションボタン入力
    public bool ClimbAction { get; set; }
    // ソナーギミックボタン入力
    public bool SonarAction { get; set; }
    // 決定ボタン入力
    public bool DecisionInput { get; set; }
    // Escキー入力
    public bool EscInput { get; set; }

    #region　InputAction
    MyInput myInput;
    void Awake()
    {
        /*if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }*/

        myInput = new MyInput();
    }
    void OnEnable() => myInput.Enable();
    void OnDisable() => myInput.Disable();
    void OnDestroy() => myInput.Dispose();
    #endregion

    private void Update()
    {
        InputMove = myInput.Player.Move.ReadValue<Vector2>();
        CameraPos = myInput.Camera.Move.ReadValue<Vector2>();
        PressedMove = myInput.Player.Move.WasPressedThisFrame();
        InputJump = myInput.Player.Jump.triggered;
        PushAction = myInput.Player.PushAction.triggered;
        ClimbAction = myInput.Player.ClimbAction.triggered;
        SonarAction = myInput.Player.SonarKey.IsPressed();
        DecisionInput = myInput.UI.Decision.WasPressedThisFrame();
        EscInput = myInput.UI.Return.WasPressedThisFrame();
    }
}
