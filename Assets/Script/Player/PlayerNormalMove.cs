using UnityEngine;

/// <summary>
/// プレイヤーの移動
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

    // 移動処理
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
        //カメラ方向
        Vector3 cameraForward = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1));
        Vector3 moveForward = cameraForward * move.y + camera.transform.right * move.x;
        Vector3 moveVector = moveForward.normalized * speed;   //移動速度
        Vector3 vector = new Vector3(rb.velocity.x, 0, rb.velocity.z);              //velocity
        rb.AddForce((moveVector - vector) * force, ForceMode.Acceleration);
        

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            player.transform.rotation = Quaternion.RotateTowards(Quaternion.LookRotation(moveVector), player.transform.rotation,0.3f);
        }
    }
}
