using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IPlayerMover
{
    float speed = 7;
    float force = 7;
    float jumpPower = 5;
    
    Rigidbody rb;

    public PlayerMove(Rigidbody _rb)
    {
        rb = _rb;    
    }
    public void Move(Vector2 move, Camera camera,  GameObject player)
    {
        //カメラ方向
        Vector3 cameraForward = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1));
        Vector3 moveForward = cameraForward * move.y + camera.transform.right * move.x;
        Vector3 moveVector = moveForward.normalized * speed;   //移動速度
        Vector3 vector = new Vector3(rb.velocity.x, 0, rb.velocity.z);              //velocity
        rb.AddForce((moveVector - vector) * force, ForceMode.Acceleration);

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            player.transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }
    public void Jump()
    {
        Vector3 jumpVector = new Vector3(0, jumpPower, 0);
        rb.AddForce(jumpVector, ForceMode.Impulse);
    }
}
