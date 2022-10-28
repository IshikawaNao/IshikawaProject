/// <summary
/// �v���C���[�̈ړ�
/// </summary>
using UnityEngine;

public class PlayerMove : MonoBehaviour, IPlayerMover
{
    float speed = 7;
    float force = 7;
    float jumpPower = 3;
    
    Rigidbody rb;
    GameObject player;

    public PlayerMove(Rigidbody _rb, GameObject _player)
    {
        rb = _rb;
        player = _player;
    }

    // �ړ�����
    public void Move(Vector2 move, GameObject camera)
    {
        //�J��������
        Vector3 cameraForward = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1));
        Vector3 moveForward = cameraForward * move.y + camera.transform.right * move.x;
        Vector3 moveVector = moveForward.normalized * speed;   //�ړ����x
        Vector3 vector = new Vector3(rb.velocity.x, 0, rb.velocity.z);              //velocity
        rb.AddForce((moveVector - vector) * force, ForceMode.Acceleration);

        // �L�����N�^�[�̌�����i�s������
        if (moveForward != Vector3.zero)
        {
            player.transform.rotation = Quaternion.LookRotation(moveVector);
        }
    }

    // �W�����v����
    public void Jump()
    {
        Vector3 jumpVector = new Vector3(0, jumpPower, 0);
        rb.AddForce(jumpVector, ForceMode.Impulse);
    }
}
