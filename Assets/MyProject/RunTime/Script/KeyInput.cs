using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Key����
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

    // �ڑ�����
    public Vector2 InputMove { get; set; }
    public bool PressedMove { get; set; }   
    public bool LongPressedMove { get; set; }   
    // �}�E�X�|�W�V����
    public Vector2 CameraPos { get; set; }
    // �W�����v����
    public bool InputJump { get; set; }
    // �A�N�V�����{�^������
    public bool PushAction { get; set; }
    // �A�N�V�����{�^������
    public bool ClimbAction { get; set; }
    // �\�i�[�M�~�b�N�{�^������
    public bool SonarAction { get; set; }
    // ����{�^������
    public bool DecisionInput { get; set; }
    // Esc�L�[����
    public bool EscInput { get; set; }
    // �J�������Z�b�g�{�^��
    public bool CameraReset { get; set; }
    // �X�N���[������
    public float Scroll { get; set; }
    // PAD����
    public bool PadCurrent { get; set; }

    #region�@InputAction
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
