using UnityEngine;

/// <summary>
/// �I�u�W�F�N�g������
/// </summary>
public class PushObject : MonoBehaviour, IMoveObject
{
    // �v����
    const float speed = 2.95f;

    const float rayLength = 1f;
    const float rayHeight = 0.3f;

    const float layerNum = 6;
    const float clmbSpeed = 4;
    Vector2 climbVelocity = Vector2.up;

    Vector3 checkVec = new(1, 0, 1);
    
    RaycastHit hit;
    Ray ray;

    public void Move(Rigidbody rb, Vector2 move, GameObject _player, Animator anim) 
    {
        if(move != Vector2.zero)
        {
            Vector3 playerForward = Vector3.Scale(_player.transform.forward, checkVec);
            Vector3 moveForward = playerForward * move.y + _player.transform.right * move.x;
            Vector3 moveVector = moveForward.normalized * speed;   //�ړ����x
            rb.velocity = moveVector;
        }
    }


    public void ClimbPlayer(Rigidbody _rb, GameObject _player)
    {
        _rb.velocity = clmbSpeed * climbVelocity;
    }

    // player�̐��ʂɓ�������I�u�W�F�N�g�����邩
    public bool Push(GameObject _player)
    {
        Vector3 orgin = _player.transform.position;
        Vector3 direction = Vector3.Scale(_player.transform.forward, checkVec);
        ray = new Ray(orgin, direction);

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if (hit.collider.gameObject.tag.Contains("Move"))
            {
                return true;
            }
        }
        return false;
    }

    // player�̐��ʂɓo���I�u�W�F�N�g�����邩
    public bool Climb(GameObject _player)
    {
        Vector3 orgin = new Vector3(_player.transform.position.x, _player.transform.position .y - rayHeight, _player.transform.position.z);
        Vector3 direction = Vector3.Scale(_player.transform.forward, checkVec);
        ray = new Ray(orgin, direction);

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if (hit.collider.gameObject.layer == layerNum)
            {
                return true;
            }
        }
       
        return false;
    }

    //�@RayhitObject��Rigidbody
    public Rigidbody Box_rb()
    {
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if (hit.collider.gameObject.tag.Contains("Move"))
            {
                return hit.rigidbody;
            }
        }
        return null;
    }
}
