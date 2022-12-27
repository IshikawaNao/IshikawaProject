using UnityEngine;

/// <summary>
/// プレイヤーの通常移動
/// </summary>
public class PlayerNormalMove : IPlayerMover
{
    const float speed = 7;
    const float force = 7;
    const float rotationSpeed = 0.3f;

    Rigidbody rb;
    GameObject player;

    public PlayerNormalMove(Rigidbody _rb, GameObject _player)
    {
        rb = _rb;
        player = _player;
    }

    // 移動処理
    public void Move(Vector2 move, Animator anim)
    {
        //  アニメーション切り替え
        AnimatorSwitching(move, anim);

        //カメラ方向　
        Camera mc = Camera.main;
        Vector3 cameraForward = Vector3.Scale(mc.transform.forward, new Vector3(1, 0, 1));
        Vector3 moveForward = cameraForward * move.y + mc.transform.right * move.x;
        Vector3 moveVector = moveForward.normalized * speed;   //移動速度
        Vector3 vector = new Vector3(rb.velocity.x, 0, rb.velocity.z);              //velocity
        rb.AddForce((moveVector - vector) * force, ForceMode.Acceleration);
        

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            player.transform.rotation = Quaternion.RotateTowards(Quaternion.LookRotation(moveVector), player.transform.rotation, rotationSpeed);
        }
    }

    // アニメーション
    void AnimatorSwitching(Vector2 move, Animator anim)
    {
        if (move != Vector2.zero)
        {
            anim.SetBool("IsWalk", true);
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsRan", false);
            anim.SetBool("IsObjectMove", false);
        }
        else
        {
            anim.SetBool("IsIdle", true);
            anim.SetBool("IsWalk", false);
            anim.SetBool("IsRan", false);
            anim.SetBool("IsObjectMove", false);
        }
    }
}
