/// <summary>
/// �I�u�W�F�N�g��o��
/// </summary>
using UnityEngine;

public class PlayerClimb : MonoBehaviour, IClimb
{
    RaycastHit hit;
    Ray ray;

    // �o�鏈��
    public void ClimbPlayer(Rigidbody _rb)
    {
        _rb.velocity = 8 * 0.5f * new Vector2(0, 1);
    }

    // player�̐��ʂɓo���I�u�W�F�N�g�����邩
    public bool Climb(GameObject _player)
    {
        Vector3 orgin = new Vector3(_player.transform.position.x, _player.transform.position.y - 0.3f, _player.transform.position.z);
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
}
