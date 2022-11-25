using UnityEngine;

public class PlayerPushMove : IPlayerMover
{
    float speed = 4;
    float force = 3;

    Rigidbody rb;
    GameObject player;

    public PlayerPushMove(Rigidbody _rb, GameObject _player)
    {
        rb = _rb;
        player = _player;
    }

    public void Move(Vector2 move, GameObject camera, Animator anim)
    {
        anim.SetBool("IsIdle", false);
        anim.SetBool("IsWalk", false);
        anim.SetBool("IsObjectMove", true);
        //ÉJÉÅÉâï˚å¸
        Vector3 cameraForward = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1));
        Vector3 moveForward = cameraForward * move.y + camera.transform.right * move.x;
        Vector3 moveVector = moveForward.normalized * speed;   //à⁄ìÆë¨ìx
        Vector3 vector = new Vector3(rb.velocity.x, 0, rb.velocity.z);              //velocity
        rb.AddForce((moveVector - vector) * force, ForceMode.Acceleration);
    }
}
