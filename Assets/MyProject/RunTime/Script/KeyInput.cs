using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;
using Unity.VisualScripting;

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
    public Vector2 CameraPos { get { return new Vector2 (myInput.Camera.Move.ReadValue<Vector2>().x, -myInput.Camera.Move.ReadValue<Vector2>().y); } }
    // �W�����v����
    public bool InputTeleport { get { return myInput.Player.Teleport.triggered; } }
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
    // �r�[���I�u�W�F�N�g��]�{�^��
    public bool BeamRotateAction { get { return myInput.Player.BeamRotate.WasPerformedThisFrame(); } }
    // Pad��Key�̂ǂ���œ������Ă��邩
    public bool Inputdetection{ get { return Pressed();} }
    bool Pressed()
    {
        if (Gamepad.current != null) { return false;  }
        else if (myInput.Player.KeyDetection.IsPressed()) { return true; }
        return true;
    }

    // ����{�^�����͌��m
    private readonly ReactiveProperty<bool> decisionInputDetection = new ReactiveProperty<bool>();
    public IReadOnlyReactiveProperty<bool> DecisionInputDetection => decisionInputDetection;
    private readonly ReactiveProperty<bool> backInput = new ReactiveProperty<bool>();
    public IReadOnlyReactiveProperty<bool> BackInput => backInput;

    private void Update()
    {
        decisionInputDetection.Value = myInput.UI.Decision.WasPressedThisFrame();
        backInput.Value = myInput.UI.Return.WasPressedThisFrame();
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
