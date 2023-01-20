using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Key入力
/// </summary>
public class KeyInput : MonoBehaviour
{
    private static KeyInput instance;
    public static KeyInput Instance {
        get {
            if (instance == null)
            {
                instance = (KeyInput)FindObjectOfType(typeof(KeyInput));

                if (instance == null)
                {
                    Debug.LogError(typeof(KeyInput) + "is nothing");
                }
            }
            return instance;
        }
        set { }
    }

    // 移送入力
    public Vector2 InputMove { get; set; }
    public bool PressedMove { get; set; }   
    public bool LongPressedMove { get; set; }   
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
    // カメラリセットボタン
    public bool CameraReset { get; set; }
    // スクロール入力
    public float Scroll { get; set; }
    // PAD入力
    public bool PadCurrent { get; set; }

    #region　InputAction
    MyInput myInput;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

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
        LongPressedMove = myInput.Player.Move.IsPressed();
        InputJump = myInput.Player.Jump.triggered;
        PushAction = myInput.Player.PushAction.IsPressed();
        ClimbAction = myInput.Player.ClimbAction.triggered;
        SonarAction = myInput.Player.SonarKey.IsPressed();
        DecisionInput = myInput.UI.Decision.WasPressedThisFrame();
        EscInput = myInput.UI.Return.WasPressedThisFrame();
        CameraReset = myInput.Camera.CameraReset.WasPerformedThisFrame();
        Scroll = myInput.Camera.Scroll.ReadValue<Vector2>().y;
        if (Gamepad.current == null) PadCurrent = false;
        else PadCurrent = true;
    }
}
