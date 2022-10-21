using UnityEngine;

public class PushObject : MonoBehaviour, IMoveObject
{
    float speed = 5;
    float force = 7;

    Vector3 moveForward;

    RaycastHit hit;
    Ray ray;

    public void Move(Rigidbody rb, Vector2 move, GameObject _player) 
    {
        Vector3 playerForward = Vector3.Scale(_player.transform.forward, new Vector3(1, 0, 1));
        if (move.y > 0)
        {
            moveForward = playerForward * move.y; 
        }
        else if (move.y < 0)
        {
            moveForward = -playerForward * move.y;
        }
        else if(move.x > 0)
        {
            moveForward = playerForward  * move.x;
        }
        else if(move.x < 0)
        {
            moveForward = -playerForward * move.x;
        }
        Vector3 moveVector = moveForward * speed;
        rb.AddForce((moveForward) * speed);
    }

    // playerの正面に動かせるオブジェクトがあるか
    public bool Push(GameObject _player)
    {
        Vector3 orgin = _player.transform.position;
        Vector3 direction = Vector3.Scale(_player.transform.forward, new Vector3(1, 0, 1));
        ray = new Ray(orgin, direction);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag.Contains("Move"))
            {
                return true;
            }
        }
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        return false;
    }

    public Rigidbody Box_rb()
    {
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag.Contains("Move"))
            {
                return hit.rigidbody;
            }
        }
        return null;
    }
}
