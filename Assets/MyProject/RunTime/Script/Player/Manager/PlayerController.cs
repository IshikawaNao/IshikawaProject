using SoundSystem;
using UnityEngine;

/// <summary> �v���C���[���� </summary>
public class PlayerController : MonoBehaviour
{
    // ���݂̃X�e�[�g
    public IPlayerState CurrentState { get { return state.CurrentState; } }

    [SerializeField,Header("�X�e�[�W�}�l�[�W���[")]
    StageManager sm;
    [SerializeField,Header("�\�i�[�G�t�F�N�g")]
    SonarEffect sonar;       
    [SerializeField,Header("Rigidbody")]
    Rigidbody rb;
    [SerializeField,Header("�R���C�_�[")]
    CapsuleCollider col;
    [SerializeField, Header("animator")]
    Animator anim;
    [SerializeField, Header("�e���|�[�g�A�j���[�V����")]
    GameObject teleport;
    [SerializeField,Header("UIManager")] 
    MainUIManager mainUIManager;
    [SerializeField, Header("RayHit���m")]
    RayHitDetection rayHitDetection;
    // �X�e�[�g�ύX
    PlayerStatecontroller state;
    // ���͎󂯎��
    KeyInput input;         
    SoundManager soundManager;

    void Start()
    {
        input = KeyInput.Instance;
        soundManager = SoundManager.Instance;
        state = new PlayerStatecontroller(input, soundManager, this.gameObject, rb, col, 
            anim, rayHitDetection, sm, teleport, sonar, mainUIManager);
        state.Init(PlayerState.Idle);
    }

    void Update()
    {
        state.Update();
    }

    private void FixedUpdate()
    {
        state.FixedUpdate();
    }
}
