using UnityEngine;

public class PlayerPushMove : IPlayerMover
{
    // 要調整
    const float speed = 2.7f;
    const float force = 3f;

    int num = 0;
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
        
        // 移動アニメーション切り替え
        if(move == Vector2.zero){ num = 0; }
        else if(move.y > 0){ num = 4; }
        else if(move.y < 0){ num = 2; }
        else if(move.x > 0){ num = 1; }
        else if (move.x < 0){ num = 3; }

        anim.SetInteger("IsPush", num);
    }

    void PushMove(Vector2 move)
    {
        //Camera cm = Camera.main;

        Vector3 playerForward = Vector3.Scale(player.transform.forward, new Vector3(1, 0, 1));
        Vector3 moveForward = playerForward * move.y + player.transform.right * move.x;
        Vector3 moveVector = moveForward.normalized * speed;   //移動速度
        rb.velocity = moveVector;
    }
}
