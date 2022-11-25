using UnityEngine;

/// <summary>
/// �v���C���[�̈ړ�
/// </summary>
public class PlayerNormalMove : IPlayerMover
{
    float speed = 7;
    float force = 7;
    
    Rigidbody rb;
    GameObject player;

    public PlayerNormalMove(Rigidbody _rb, GameObject _player)
    {
        rb = _rb;
        player = _player;
    }

    // �ړ�����
    public void Move(Vector2 move, GameObject camera, Animator anim)
    {
        if (move != new Vector2(0, 0))
        {
            anim.SetBool("IsWalk",true);
            anim.SetBool("IsIdle", false);
        }
        else
        {
            anim.SetBool("IsIdle",true);
            anim.SetBool("IsWalk", false);
        }
        //�J��������
        Vector3 cameraForward = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1));
        Vector3 moveForward = cameraForward * move.y + camera.transform.right * move.x;
        Vector3 moveVector = moveForward.normalized * speed;   //�ړ����x
        Vector3 vector = new Vector3(rb.velocity.x, 0, rb.velocity.z);              //velocity
        rb.AddForce((moveVector - vector) * force, ForceMode.Acceleration);
        

        // �L�����N�^�[�̌�����i�s������
        if (moveForward != Vector3.zero)
        {
            player.transform.rotation = Quaternion.RotateTowards(Quaternion.LookRotation(moveVector), player.transform.rotation,0.3f);
        }
    }
}
