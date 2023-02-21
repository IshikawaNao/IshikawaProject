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
    public Vector2 InputMove { get { return myInput.Player.Move.ReadValue<Vector2>(); } }
    public bool PressedMove { get { return myInput.Player.Move.WasPressedThisFrame(); } }   
    public bool LongPressedMove { get { return myInput.Player.Move.IsPressed(); } }   
    // �}�E�X�|�W�V����
    public Vector2 CameraPos { get { return myInput.Camera.Move.ReadValue<Vector2>(); } }
    // �W�����v����
    public bool InputJump { get { return myInput.Player.Jump.triggered; } }
    // �A�N�V�����{�^������
    public bool PushAction { get { return myInput.Player.PushAction.IsPressed(); } }
    // �A�N�V�����{�^������
    public bool ClimbAction { get { return myInput.Player.ClimbAction.triggered; } }
    // �\�i�[�M�~�b�N�{�^������
    public bool SonarAction { get { return myInput.Player.SonarKey.WasPressedThisFrame(); } }
    // ����{�^������
    public bool DecisionInput { get { return myInput.UI.Decision.WasPressedThisFrame(); } }
    // Esc�L�[����
    public bool EscInput { get { return myInput.UI.Return.WasPressedThisFrame(); } }
    // �J�������Z�b�g�{�^��
    public bool CameraReset { get { return myInput.Camera.CameraReset.WasPerformedThisFrame(); } }
    // �X�N���[������
    public float Scroll { get { return myInput.Camera.Scroll.ReadValue<Vector2>().y; } }
    // ���͂��ꂽ
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
    #region�@InputAction
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
