using UnityEngine;

public class PlayerMove : MonoBehaviour
{
   public void Move(Rigidbody rb, Vector2 move, float speed ,float force)
    {
        Vector3 moveVector = (new Vector3(move.x, 0, move.y).normalized) * speed;   //ˆÚ“®‘¬“x
        Vector3 vector = new Vector3(rb.velocity.x, 0, rb.velocity.z);              //velocity
        rb.AddForce((moveVector - vector) * force, ForceMode.Acceleration);
    }
}
