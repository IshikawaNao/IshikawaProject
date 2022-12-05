using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Key����
/// </summary>
public class KeyInput : MonoBehaviour
{
    public static KeyInput Instance { get; private set; }

    // �ڑ�����
    public Vector2 InputMove { get; set; }
    public bool PressedMove { get; set; }   
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

    #region�@InputAction
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
