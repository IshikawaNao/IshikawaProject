using UnityEngine;

public class PlayerPushMove : IPlayerMover
{
    // 要調整
    float speed = 2.7f;
    float force = 3f;

    Rigidbody rb;
    GameObject player;

    public PlayerPushMove(Rigidbody _rb, GameObject _player)
    {
        rb = _rb;
        player = _player;
    }

    public void Move(Vector2 move, Animator anim)
    {
        // Animator
        PushMoveAnimator(move, anim);

        // Move
        PushMove(move);
    }

    // アニメーションの管理
    void PushMoveAnimator(Vector2 move, Animator anim)
    {
        anim.SetBool("IsIdle", true);
        anim.SetBool("IsWalk", false);
        anim.SetBool("IsRan", false);
        anim.SetBool("IsObjectMove", true);


        if(move == Vector2.zero)
        {
            anim.SetInteger("IsPush", 0);
        }
         else if(move.y > 0)
        {
            anim.SetInteger("IsPush", 0);
        }
        else if(move.y < 0)
        {
            anim.SetInteger("IsPush", 2);
        }
        else if(move.x > 0)
        {
            anim.SetInteger("IsPush", 1);
        }
        else if (move.x < 0)
        {
            anim.SetInteger("IsPush", 3);
        }
    }

    void PushMove(Vector2 move)
    {
        //Camera cm = Camera.main;

        Vector3 playerForward = Vector3.Scale(player.transform.forward, new Vector3(1, 0, 1));
        Vector3 moveForward = playerForward * move.y + player.transform.right * move.x;
        //カメラ方向
        //Vector3 cameraForward = Vector3.Scale(cm.transform.forward, new Vector3(1, 0, 1));
        //Vector3 moveForward = cameraForward * move.y + cm.transform.right * move.x;
        Vector3 moveVector = moveForward.normalized * speed;   //移動速度
        Vector3 vector = new Vector3(rb.velocity.x, 0, rb.velocity.z);              //velocity
       // rb.AddForce((moveVector - vector) * force, ForceMode.Acceleration);
        rb.velocity = moveVector;
    }
}
