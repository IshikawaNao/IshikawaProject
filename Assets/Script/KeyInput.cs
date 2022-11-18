using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Key����
/// </summary>
public class KeyInput : MonoBehaviour
{
    // �ڑ�����
    public Vector2 InputMove { get; set; }
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

    #region�@InputAction
    MyInput myInput;
    void Awake() => myInput = new MyInput();
    void OnEnable() => myInput.Enable();
    void OnDisable() => myInput.Disable();
    void OnDestroy() => myInput.Dispose();
    #endregion

    private void Update()
    {
        InputMove = myInput.Player.Move.ReadValue<Vector2>();
        CameraPos = myInput.Camera.Move.ReadValue<Vector2>();
        InputJump = myInput.Player.Jump.triggered;
        PushAction = myInput.Player.PushAction.triggered;
        ClimbAction = myInput.Player.ClimbAction.triggered;
        SonarAction = myInput.Player.SonarKey.IsPressed();
    }
}
