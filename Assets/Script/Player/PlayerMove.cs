using UnityEngine;

public class PlayerMove : MonoBehaviour
{
   public void Move(Rigidbody rb, Vector2 move, float speed, float force)
    {
        //カメラ方向
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1));
        Vector3 moveForward = cameraForward * move.y + Camera.main.transform.right * move.x;
        Vector3 moveVector = moveForward.normalized * speed;   //移動速度
        Vector3 vector = new Vector3(rb.velocity.x, 0, rb.velocity.z);              //velocity
        rb.AddForce((moveVector - vector) * force, ForceMode.Acceleration);

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }
}
