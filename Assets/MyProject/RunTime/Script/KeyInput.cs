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
    public Vector2 InputMove { get { return myInput.Player.Move.ReadValue<Vector2>(); } }
    public bool PressedMove { get { return myInput.Player.Move.WasPressedThisFrame(); } }   
    public bool LongPressedMove { get { return myInput.Player.Move.IsPressed(); } }   
    // マウスポジション
    public Vector2 CameraPos { get { return myInput.Camera.Move.ReadValue<Vector2>(); } }
    // ジャンプ入力
    public bool InputJump { get { return myInput.Player.Jump.triggered; } }
    // アクションボタン入力
    public bool PushAction { get { return myInput.Player.PushAction.IsPressed(); } }
    // アクションボタン入力
    public bool ClimbAction { get { return myInput.Player.ClimbAction.triggered; } }
    // ソナーギミックボタン入力
    public bool SonarAction { get { return myInput.Player.SonarKey.WasPressedThisFrame(); } }
    // 決定ボタン入力
    public bool DecisionInput { get { return myInput.UI.Decision.WasPressedThisFrame(); } }
    // Escキー入力
    public bool EscInput { get { return myInput.UI.Return.WasPressedThisFrame(); } }
    // カメラリセットボタン
    public bool CameraReset { get { return myInput.Camera.CameraReset.WasPerformedThisFrame(); } }
    // スクロール入力
    public float Scroll { get { return myInput.Camera.Scroll.ReadValue<Vector2>().y; } }
    // 入力された
    public bool Inputdetection{ get { return Pressed();} }
    bool pressed = true;
    bool Pressed()
    {
        if (myInput.Player.KeyDetection.IsPressed()) { pressed = true; }
        else if (Gamepad.current != null)
        { 
            if(Gamepad.current.IsPressed()){ pressed = false; }
        }
        return pressed;
    }
    #region　InputAction
    MyInput myInput;
    void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        myInput = new MyInput();
    }
    void OnEnable() => myInput.Enable();
    //void OnDisable() => myInput.Disable();
    #endregion

}
