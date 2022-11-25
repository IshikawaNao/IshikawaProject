/// <summary>
/// オブジェクトを押す
/// </summary>
using UnityEngine;

public class PushObject : MonoBehaviour, IMoveObject
{
    float speed = 1.5f;
    
    Vector3 moveForward;

    RaycastHit hit;
    Ray ray;

    public void Move(Rigidbody rb, Vector2 move, GameObject _player, Animator anim) 
    {
        if(move != Vector2.zero)
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
            else if (move.x > 0)
            {
                moveForward = playerForward * move.x;
            }
            else if (move.x < 0)
            {
                moveForward = -playerForward * move.x;
            }
            anim.SetBool("IsObjectMove", true);
            rb.velocity = (moveForward * speed);
        }
    }


    public void ClimbPlayer(Rigidbody _rb, GameObject _player)
    {
        //_player.transform.Translate(0, 0.06f, 0);
        _rb.velocity = 8 * 0.5f * new Vector2(0, 1);
    }

    // playerの正面に動かせるオブジェクトがあるか
    public bool Push(GameObject _player)
    {
        Vector3 orgin = _player.transform.position;
        Vector3 direction = Vector3.Scale(_player.transform.forward, new Vector3(1, 0, 1));
        ray = new Ray(orgin, direction);

        if (Physics.Raycast(ray, out hit, 1f))
        {
            if (hit.collider.gameObject.tag.Contains("Move"))
            {
                return true;
            }
        }
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 1f);
        return false;
    }

    // playerの正面に登れるオブジェクトがあるか
    public bool Climb(GameObject _player)
    {
        Vector3 orgin = new Vector3(_player.transform.position.x, _player.transform.position .y - 0.3f, _player.transform.position.z);
        Vector3 direction = Vector3.Scale(_player.transform.forward, new Vector3(1, 0, 1));
        ray = new Ray(orgin, direction);

        if (Physics.Raycast(ray, out hit, 1f))
        {
            if (hit.collider.gameObject.layer == 6)
            {
                print("hit");
                return true;
            }
        }
       
        Debug.DrawRay(orgin, direction, Color.red, 1f);
        return false;
    }

    //　RayhitObjectのRigidbody
    public Rigidbody Box_rb()
    {
        if (Physics.Raycast(ray, out hit, 1f))
        {
            if (hit.collider.gameObject.tag.Contains("Move"))
            {
                return hit.rigidbody;
            }
        }
        return null;
    }
}
