public interface IPlayerState
{
    // ���̃N���X�̏�Ԃ��擾����
    PlayerState State { get; }

    // ��ԊJ�n���ɍŏ��Ɏ��s�����
    void Entry();

    // �t���[�����ƂɎ��s�����
    void Update();

    // ��莞�Ԃ��Ǝ��s�����
    void FixedUpdate();

    // ��ԏI�����Ɏ��s�����
    void Exit();
}

// �v���C���[�̏��
public enum PlayerState { Idle, Move, Climb, Push, Gliding, Sonar, Teleport , Pause}
