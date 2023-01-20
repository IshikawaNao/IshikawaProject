using SoundSystem;
using UnityEngine;

public class PlayerRan : IPlayerMover
{
    float speed = 15;
    float force = 7;

    Rigidbody rb;
    GameObject player;

    public PlayerRan(Rigidbody _rb, GameObject _player)
    {
        rb = _rb;
        player = _player;
    }

    public void Move(Vector2 move, Animator anim)
    {
        anim.SetInteger("IsPush", 0);
        anim.SetBool("IsObjectMove", false);
        anim.SetBool("IsIdle", false);
        anim.SetBool("IsWalk", false);
        anim.SetBool("IsRan", true);


        //�J���������@
        Camera mc = Camera.main;
        Vector3 cameraForward = Vector3.Scale(mc.transform.forward, new Vector3(1, 0, 1));
        Vector3 moveForward = cameraForward * move.y + mc.transform.right * move.x;
        Vector3 moveVector = moveForward.normalized * speed;   //�ړ����x
        Vector3 vector = new Vector3(rb.velocity.x, 10, rb.velocity.z);              //velocity
        rb.AddForce((moveVector - vector) * force, ForceMode.Acceleration);

        SoundManager.Instance.PlayShotSe("Ran");

        // �L�����N�^�[�̌�����i�s������
        if (moveForward != Vector3.zero)
        {
            player.transform.rotation = Quaternion.RotateTowards(Quaternion.LookRotation(moveVector), player.transform.rotation, 0.3f);
        }
    }
}